using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            PlayerController touchais = collision.gameObject.GetComponent<PlayerController>();
            touchais.TakeDamage(-1);
        }
    }
}
