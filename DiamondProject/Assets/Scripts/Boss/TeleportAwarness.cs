using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAwarness : MonoBehaviour {
    [SerializeField] Reference<TempPlayerController> _player;
    [SerializeField] Reference<Boss> _boss;
    [SerializeField] CircleCollider2D fleeDetection = null;
    [SerializeField] public float FleeDetectionRadius = 2f;

    private void Start() {
        fleeDetection = new CircleCollider2D();
        fleeDetection.radius = FleeDetectionRadius;
    }
    void UpdateFlee() {
        Vector3 playerPosition = _player.Instance.gameObject.transform.position;
        if ((playerPosition - transform.position).sqrMagnitude < FleeDetectionRadius) {
            Shield shield = _boss.Instance.gameObject.GetComponentInChildren<Shield>();
            if (shield == null) {
                _boss.Instance.Teleport();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        UpdateFlee();
    }

    private void OnCollisionStay2D(Collision2D collision) {
        UpdateFlee();
    }
}
