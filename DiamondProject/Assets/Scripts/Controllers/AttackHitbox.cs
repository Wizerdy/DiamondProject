using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour {
    private Vector3 startPos;

    private void Start() {
        startPos = transform.localPosition;
    }

    private void Update() {
        transform.localPosition = startPos;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Boss boss = collision.gameObject.GetComponent<Boss>();
        if (boss != null) {
            boss.LoseLife(1);
            return;
        }

        Rock rock = collision.gameObject.GetComponent<Rock>();
        if (rock != null) {
            rock.LoseLife(1);
            return;
        }

        Missile missile = collision.gameObject.GetComponent<Missile>();
        if (missile != null) {
            missile.Die();
            return;
        }
    }
}
