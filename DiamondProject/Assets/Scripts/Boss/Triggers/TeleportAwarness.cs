using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAwarness : MonoBehaviour {
    [SerializeField] PlayerControllerReference _player;
    [SerializeField] Reference<Boss> _boss;
    [SerializeField] CircleCollider2D fleeDetection = null;
    [SerializeField] public float FleeDetectionRadius = 2f;

    private void Start() {
        fleeDetection = gameObject.AddComponent<CircleCollider2D>();
        fleeDetection.isTrigger = true;
        fleeDetection.radius = FleeDetectionRadius;
    }
    void UpdateFlee() {
        Vector3 playerPosition = _player.Instance.gameObject.transform.position;
            Shield shield = _boss.Instance.gameObject.GetComponentInChildren<Shield>();
        if (_boss.Instance.CurrentState != Boss.State.TELEPORT 
            && _boss.Instance.CurrentState != Boss.State.FIREBALL 
            && _boss.Instance.CurrentState != Boss.State.FIREMISSILE 
            && _boss.Instance.CurrentState != Boss.State.FIREBOTH            
            || shield != null) {
            _boss.Instance.StopActions();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            UpdateFlee();
            
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            UpdateFlee();
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            UpdateFlee();
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            UpdateFlee();
        }
    }
}
