using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityInteract : MonoBehaviour {
    [SerializeField] float radius = 5f;

    [Header("Debug")]
    [SerializeField] Color radiusColor = Color.green;

    List<NPC> npcs = new List<NPC>();

    private void Update() {
        NpcInRange(radius);
    }

    public void Interact(NPC npc) {
        if (npcs.Count > 0) {
            npc.StartTalking(transform.gameObject);
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

    public void OnDrawGizmosSelected() {
        Gizmos.color = radiusColor;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
