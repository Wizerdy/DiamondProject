using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactCollision : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            PlayerController touchais = collision.gameObject.GetComponent<PlayerController>();
            touchais.Health.TakeDamage(100);
        }
    }
}
