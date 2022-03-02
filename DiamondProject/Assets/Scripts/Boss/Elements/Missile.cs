using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    [SerializeField] private Reference<TempPlayerController> _player;
    Vector3 direction = Vector3.zero;
    float speed = 1f;
    float lifetime = 1f;
    float lifetimeTimer = 1f;
    Rigidbody2D rb = null;

    public Missile SetSpeed(float speed) {
        this.speed = speed;
        return this;
    }

    public Missile SetLifeTime(float lifetime) {
        this.lifetime = lifetime;
        return this;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            _player.Instance.TakeDamage();
            Die();
        }
    }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        lifetimeTimer = lifetime;
    }

    private void FixedUpdate() {
        lifetimeTimer -= Time.fixedDeltaTime;
        if (lifetimeTimer <= 0) {
            Die();
        }
        direction = _player.Instance.gameObject.transform.position - transform.position;
        rb.velocity = direction.normalized * speed;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction) * Quaternion.Euler(0, 0, 90);
    }

    public void Die() {
        Destroy(gameObject);
    }
}
