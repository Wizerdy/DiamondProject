using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderInLayerManager : MonoBehaviour {
    [SerializeField] bool Offset = false;
    [SerializeField] Transform OffsetTransform;
    SpriteRenderer[] sr;
    MeshRenderer[] sk;
    ParticleSystemRenderer[] ps;
    private void Start() {
        sr = GetComponents<SpriteRenderer>();
        sk = GetComponents<MeshRenderer>();
        ps = GetComponents<ParticleSystemRenderer>();

    }

    private void Update() {
        if (!Offset) {
            for (int i = 0; i < sr.Length; i++) {
                sr[i].sortingOrder = (int)(transform.position.y * -100);
            }
            for (int i = 0; i < sk.Length; i++) {
                sk[i].sortingOrder = (int)(transform.position.y * -100);
            }
            for (int i = 0; i < ps.Length; i++) {
                ps[i].sortingOrder = (int)(ps[i].gameObject.transform.position.y * -100);
            }
        } else {
            for (int i = 0; i < sr.Length; i++) {
                sr[i].sortingOrder = (int)(OffsetTransform.position.y * -100);
                Debug.Log(OffsetTransform.position.y);
                Debug.Log(OffsetTransform.position.y * -100);
            }
            for (int i = 0; i < sk.Length; i++) {
                sk[i].sortingOrder = (int)(OffsetTransform.position.y * -100);
            }
            for (int i = 0; i < ps.Length; i++) {
                ps[i].sortingOrder = (int)(OffsetTransform.position.y * -100);
            }
        }
    }
}
