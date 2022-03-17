using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    [SerializeField] private PlayerControllerReference _player;
    [SerializeField] Vector3 directionTarget = Vector3.zero;
    [SerializeField] Vector3 direction = Vector3.zero;
    [SerializeField] float speed = 1f;
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] float lifetime = 1f;
    [SerializeField] float lifetimeTimer = 1f;
    Rigidbody2D rb = null;

    public Missile SetSpeed(float speed) {
        this.speed = speed;
        return this;
    }

    public Missile SetRotationSpeed(float rotationSpeed) {
        this.rotationSpeed = rotationSpeed;
        return this;
    }

    public Missile SetLifeTime(float lifetime) {
        this.lifetime = lifetime;
        return this;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            _player.Instance.Health.TakeDamage(10);
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
        directionTarget = _player.Instance.gameObject.transform.position - transform.position;
        float directionµSense =  180 <= Vector3.Angle(direction, directionTarget) ? -1 : 1;
        direction = Vector3.RotateTowards(direction, directionTarget, Mathf.Deg2Rad * Time.fixedDeltaTime * rotationSpeed, 1);
        rb.velocity = direction.normalized * speed;
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, direction) * Quaternion.Euler(0, 0, 90);
    }

    public void Die() {
        Destroy(gameObject);
    }

    float ModuloAngle(float angle) {
        angle %= 360;
        angle += 360;
        angle %= 360;
        return angle;
    }

    float ModuloAngle(Vector3 vector) {
        return ModuloAngle(Vector3.Angle(Vector3.up, vector));
    }
}
