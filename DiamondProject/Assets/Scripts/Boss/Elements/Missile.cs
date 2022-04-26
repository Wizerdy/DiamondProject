using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;
using UnityEngine.Events;

public class Missile : BossEntities {
    [SerializeField] UnityEvent OnDeath;
    [SerializeField] UnityEvent OnDestroy;
    [SerializeField] protected PlayerControllerReference _player;
    [SerializeField] protected Vector3 directionTarget = Vector3.zero;
    [SerializeField] protected Vector3 direction = Vector3.zero;
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected float rotationSpeed = 100f;
    [SerializeField] protected float lifetime = 1f;
    [SerializeField] protected float lifetimeTimer = 1f;
    [SerializeField] protected float explosionRadius = 1f;
    [SerializeField] protected int explosionDamage = 1;
    [SerializeField] ExploBush explobush;
    protected Rigidbody2D rb = null;
    public FireMissile firemissile;
    bool isDead = false;

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

    public virtual void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            Die();
        }
    }

    public virtual void Start() {
        rb = GetComponent<Rigidbody2D>();
        lifetimeTimer = lifetime;
        explobush.AddExploBush();
    }

    public virtual void FixedUpdate() {
        if (!isDead) {
            lifetimeTimer -= Time.fixedDeltaTime;
            if (lifetimeTimer <= 0) {
                Die();
            }
            directionTarget = _player.Instance.gameObject.transform.position - transform.position;
            direction = Vector3.RotateTowards(direction, directionTarget, Mathf.Deg2Rad * Time.fixedDeltaTime * rotationSpeed, 1);
            rb.velocity = direction.normalized * speed;
        }
    }

    public void Die() {
        explobush.RemoveExploBush();
        isDead = true;
        rb.velocity = Vector2.zero;
        OnDeath?.Invoke();
        StartCoroutine(Tools.Delay(() => Explode(), 1f));
    }

    void Explode() {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        Debug.DrawLine(transform.position - new Vector3(explosionRadius / 2, 0), transform.position + new Vector3(explosionRadius / 2, 0));
        Debug.DrawLine(transform.position - new Vector3(0, explosionRadius / 2), transform.position + new Vector3(0, explosionRadius / 2));
        for (int i = 0; i < collider.Length; i++) {
            if(collider[i].tag == "Player") {
                _player.Instance.Health.TakeDamage(explosionDamage);
            }
        }
        OnDestroy?.Invoke();
        Destroy(gameObject);
    }

    protected float ModuloAngle(float angle) {
        angle %= 360;
        angle += 360;
        angle %= 360;
        return angle;
    }

    protected float ModuloAngle(Vector3 vector) {
        return ModuloAngle(Vector3.Angle(Vector3.up, vector));
    }
}
