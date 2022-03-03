using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class EntityInteract : MonoBehaviour {
    [SerializeField] float radius = 5f;

    [Header("Debug")]
    [SerializeField] bool _showDebug = false;
    [SerializeField] Color radiusColor = Color.green;

    List<NPC> npcs = new List<NPC>();
    NPC _interractingNPC = null;

    public Tools.BasicDelegate<NPC> OnInteract;
    public Tools.BasicDelegate<NPC> OnStopInteract;

    private void Awake() {
        OnStopInteract += StopInteract;
    }

    private void Update() {
        NpcInRange(radius);
    }

    private void OnDestroy() {
        OnStopInteract -= StopInteract;
        if (_interractingNPC != null) {
            UnsunscribeInteraction(_interractingNPC);
        }
    }

    public void Interact(NPC npc) {
        if (npcs.Count > 0) {
            _interractingNPC = npc;
            npc.StartTalking(transform.gameObject);
            OnInteract?.Invoke(npc);
            npc.OnStopInteract += OnStopInteract;
            npc.OnStopInteract += UnsunscribeInteraction;
        }
    }

    public void NpcInRange(float radius) {
        npcs.Clear();
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius, 1 << LayerMask.NameToLayer("NPC"));
        for (int i = 0; i < hitColliders.Length; i++) {
            if (!npcs.Contains(hitColliders[i].GetComponent<NPC>())) {
                npcs.Add(hitColliders[i].GetComponent<NPC>());
            }
        }
    }

    public NPC GetNearestNpc() {
        if (npcs.Count <= 0) { return null; }

        NPC tempEntity = null;
        float minDistance = Mathf.Infinity;

        for (int i = 0; i < npcs.Count; i++) {
            float distance = Vector3.Distance(transform.position, npcs[i].transform.position);
            if (distance > radius) {
                npcs.Remove(npcs[i]);
                continue;
            }
            if (minDistance > distance) {
                minDistance = distance;
                tempEntity = npcs[i];
            }
        }
        return tempEntity;
    }

    private void StopInteract(NPC npc) {
        _interractingNPC = null;
    }

    private void UnsunscribeInteraction(NPC npc) {
        npc.OnStopInteract -= OnStopInteract;
        npc.OnStopInteract -= UnsunscribeInteraction;
    }

    public void OnDrawGizmosSelected() {
        if (!_showDebug) { return; }
        Gizmos.color = radiusColor;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
