using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boomerang : MonoBehaviour {
    [SerializeField] float _firstSpeed;
    [SerializeField] float _secondSpeed;
    [SerializeField] int _damage;
    [SerializeField] Vector3 _direction;
    [SerializeField] Vector3 _destination;
    [SerializeField] Vector3 _depart;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] DamageHealth dh;
    [SerializeField] bool comeback;
    [SerializeField] UnityEvent<Boomerang> _onDeath;
    public event UnityAction<Boomerang> OnDeath { add => _onDeath.AddListener(value); remove => _onDeath.RemoveListener(value); }

    public Boomerang SetDirection(Vector3 direction) {
        this._direction = direction;
        return this;
    }

    public Boomerang SetDestination(Vector3 destination) {
        this._destination = destination;
        return this;
    }

    public Boomerang SetFirstSpeed(float speed) {
        this._firstSpeed = speed;
        return this;
    }
    public Boomerang SetSecondSpeed(float speed) {
        this._secondSpeed = speed;
        return this;
    }
    public Boomerang SetDamage(int damage) {
        _damage = damage;
        return this;
    }



    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = _direction * _firstSpeed;
        dh = GetComponent<DamageHealth>();
        dh.Damage = _damage;
        _depart = transform.position;
    }
    private void Update() {
        if (!comeback) {
            if (_direction == Vector3.left && transform.position.x <= _destination.x) {
                Comeback();
            } else if (_direction == Vector3.right && transform.position.x >= _destination.x) {
                Comeback();
            } else if (_direction == Vector3.up && transform.position.y >= _destination.y) {
                Comeback();
            } else if (_direction == Vector3.down && transform.position.y <= _destination.y) {
                Comeback();
            }
        } else {
            if (_direction == Vector3.left && transform.position.x <= _depart.x) {
                Die();
            } else if (_direction == Vector3.right && transform.position.x >= _depart.x) {
                Die();
            } else if (_direction == Vector3.up && transform.position.y >= _depart.y) {
                Die();
            } else if (_direction == Vector3.down && transform.position.y <= _depart.y) {
                Die();
            }
        }
    }

    private void Comeback() {
        comeback = true;
        _direction *= -1;
        rb.velocity = _direction * _secondSpeed;
        transform.localScale *= -1;
    }

    private void Die() {
        _onDeath?.Invoke(this);
        Destroy(gameObject);
    }
}
