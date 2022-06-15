using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderInLayerManager : MonoBehaviour {
    [SerializeField] bool Offset = false;
    [SerializeField] Transform OffsetTransform;
    SpriteRenderer[] sr;
    MeshRenderer[] sk;
    private void Start() {
        sr = GetComponents<SpriteRenderer>();
        sk = GetComponents<MeshRenderer>();
        if (Offset) {
            for (int i = 0; i < sr.Length; i++) {
                sr[i].sortingOrder = (int)(OffsetTransform.position.y * -1000);
            }
            for (int i = 0; i < sk.Length; i++) {
                sk[i].sortingOrder = (int)(OffsetTransform.position.y * -1000);
            }
        }
    }

    private void Update() {
        if (!Offset) {
            for (int i = 0; i < sr.Length; i++) {
                sr[i].sortingOrder = (int)(transform.position.y * -1000);
            }
            for (int i = 0; i < sk.Length; i++) {
                sk[i].sortingOrder = (int)(transform.position.y * -1000);
            }
        }
    }
}
