using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafProjectile : MonoBehaviour {
    [SerializeField] Vector3 direction;
    [SerializeField] float _speed;
    [SerializeField] int _damage;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] DamageHealth dh;

    public LeafProjectile SetDirection(Vector3 direction) {
        this.direction = direction;
        return this;
    }

    public LeafProjectile SetSpeed(float speed) {
        this._speed = speed;
        return this;
    }

    public LeafProjectile SetDamage(int damage) {
        _damage = damage;
        return this;
    }

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * _speed;
        dh = GetComponent<DamageHealth>();
        dh.Damage = _damage;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Structure") {
            Destroy(gameObject);
        }
    }
}
