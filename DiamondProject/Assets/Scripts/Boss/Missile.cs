using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    Vector3 direction = Vector3.zero;
    float speed = 1f;
    float lifetime = 1f;
    Rigidbody2D rb = null;
    PlayerController player = null;

    public Missile SetSpeed(float speed) {
        this.speed = speed;
        return this;
    }

    public Missile SetLifeTime(float lifetime) {
        this.lifetime = lifetime;
        return this;
    }

    private void Start() {
        rb = GetComponent<Rigidbody2D>(); 
        player = Gino.instance.player.GetComponent<PlayerController>();
    }

    private void Update() {
        direction = player.gameObject.transform.position - transform.position;
        rb.velocity = direction.normalized * speed;
    }
}
