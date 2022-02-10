using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Entity
{
    PlayerControls controls;

    [Header("NPC Settings")]
    [SerializeField] private bool isInteracting;
    public float npcInteractRadius;
    public List<NPC> npcList;

    void Update()
    {
        Move();
        NpcInRange(npcInteractRadius);
        GetNearestNpc();
    }

    private void Awake()
    {
        controls = new PlayerControls();

        controls.GamePlay.Move.performed += cc => direction = cc.ReadValue<Vector2>();
        controls.GamePlay.Move.canceled += cc => direction = Vector2.zero;

        controls.GamePlay.Interact.performed += cc => isInteracting = cc.ReadValue<bool>();
        controls.GamePlay.Interact.canceled += cc => isInteracting = false;
    }

    public override void Move()
    {
        // Direction value updated ?
        //Debug.Log(direction);
        base.Move();
    }

    public void NpcInRange(float radius)
    {
        // Tableau des npc dans la range d'interaction du joueur
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, npcInteractRadius, 1 << LayerMask.NameToLayer("NPC"));
        // Boucle sur tous les colliders dans la range
        for (int i = 0; i < hitColliders.Length; i++)
        {
            // Vérification de la taille de la liste pour ne pas faire tourner dans le vide quand la liste est vide
            if (!npcList.Contains(hitColliders[i].GetComponent<NPC>()))
            {
                npcList.Add(hitColliders[i].GetComponent<NPC>());
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, npcInteractRadius);
    }

    public void Interact()
    {
        if (isInteracting)
        {
           // Open dialog
        }
    }

    public Entity GetNearestNpc()
    {
        if (npcList.Count <= 0)
        {
            return null;
        }

        Entity tempEntity = null;
        float dist = Mathf.Infinity;
        for (int i = 0; i < npcList.Count; i++)
        {
            if (Vector3.Distance(transform.position, npcList[i].transform.position) > npcInteractRadius)
            {
                npcList.Remove(npcList[i]);
                continue;
            }   
            if (dist > Vector3.Distance(transform.position, npcList[i].transform.position))
            {
                // A CHANGER PROTO
                dist = Vector3.Distance(transform.position, npcList[i].transform.position);
                tempEntity = npcList[i];
            }
        }
        return tempEntity;
    }

    private void OnEnable()
    {
        controls.GamePlay.Enable();
    }

    private void OnDisable()
    {
        controls.GamePlay.Disable();
    }
}
