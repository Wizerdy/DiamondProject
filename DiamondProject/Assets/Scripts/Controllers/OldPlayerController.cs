using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OldPlayerController : Entity {
    PlayerControls controls;

    [Header("NPC Settings")]
    [SerializeField] private float interactingValue;
    public float npcInteractRadius;
    public bool isInteracting;
    public bool isInteracting2;
    public List<NPC> npcList;
    [SerializeField] private TextInteraction textInteraction;

    private bool canMove = true;

    void Update() {
        if (canMove)
            Move();

        NpcInRange(npcInteractRadius);
        GetNearestNpc();
        Interact();
        if (interactingValue != 0 && isInteracting2 == true) {
            isInteracting = !isInteracting;
            isInteracting2 = false;
        }
        if (interactingValue == 0) {
            isInteracting2 = true;
        }
    }

    private void Awake() {
        controls = new PlayerControls();

        controls.GamePlay.Move.performed += cc => direction = cc.ReadValue<Vector2>();
        controls.GamePlay.Move.canceled += cc => direction = Vector2.zero;

        controls.GamePlay.Interact.performed += cc => interactingValue = cc.ReadValue<float>();
        controls.GamePlay.Interact.canceled += cc => interactingValue = 0;

        //controls.GamePlay.DialogueInteraction.started += cc => textInteraction.OnClickEvent();
    }

    private void OnDestroy() {
        //controls.GamePlay.DialogueInteraction.started -= cc => textInteraction.OnClickEvent();
    }

    public override void Move() {
        // Direction value updated ?
        //Debug.Log(direction);
        base.Move();
    }

    public void NpcInRange(float radius) {
        // Tableau des npc dans la range d'interaction du joueur
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, npcInteractRadius, 1 << LayerMask.NameToLayer("NPC"));
        // Boucle sur tous les colliders dans la range
        for (int i = 0; i < hitColliders.Length; i++) {
            // Vérification de la taille de la liste pour ne pas faire tourner dans le vide quand la liste est vide
            if (!npcList.Contains(hitColliders[i].GetComponent<NPC>())) {
                npcList.Add(hitColliders[i].GetComponent<NPC>());
            }
        }
    }

    public void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, npcInteractRadius);
    }

    public void Interact() {
        if (npcList.Count > 0) {
            if (isInteracting) {
                GetNearestNpc().StartTalking(transform.gameObject);
            }
        }
        isInteracting = false;
    }

    public NPC GetNearestNpc() {
        if (npcList.Count <= 0) {
            return null;
        }

        NPC tempEntity = null;
        float dist = Mathf.Infinity;
        for (int i = 0; i < npcList.Count; i++) {
            if (Vector3.Distance(transform.position, npcList[i].transform.position) > npcInteractRadius) {
                npcList.Remove(npcList[i]);
                continue;
            }
            if (dist > Vector3.Distance(transform.position, npcList[i].transform.position)) {
                // A CHANGER PROTO
                dist = Vector3.Distance(transform.position, npcList[i].transform.position);
                tempEntity = npcList[i];
            }
        }
        return tempEntity;
    }

    public void disableMovement() {
        canMove = false;
    }

    public void enableMovement() {
        canMove = true;
    }

    private void OnEnable() {
        controls.GamePlay.Enable();
    }

    private void OnDisable() {
        controls.GamePlay.Disable();
    }
}
