using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boomerang : MonoBehaviour {
    [SerializeField] float _firstSpeed;
    [SerializeField] float _secondSpeed;
    [SerializeField] int _damage;
    [SerializeField] int _damageBack;
    [SerializeField] Vector3 _direction;
    [SerializeField] Vector3 _destination;
    [SerializeField] Vector3 _depart;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] DamageHealth dh;
    [SerializeField] bool comeback;
    [SerializeField] UnityEvent _onComeback;
    [SerializeField] UnityEvent<Boomerang> _onDeath;

    public event UnityAction OnComeback { add => _onComeback.AddListener(value); remove => _onComeback.RemoveListener(value); }
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

    public Boomerang SetDamageBack(int damage) {
        _damageBack = damage;
        return this;
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = _direction * _firstSpeed;
        dh = GetComponent<DamageHealth>();
        dh.Damage = _damage;
        _depart = transform.position;
    }

    private void Update() {
        float manatthanMax = Mathf.Abs(_destination.x - _depart.x) + Mathf.Abs(_destination.y - _depart.y);
        float manatthanCurrent = Mathf.Abs(_destination.x - transform.position.x) + Mathf.Abs(_destination.y - transform.position.y);
        float percentage = Mathf.InverseLerp(0, manatthanMax, manatthanCurrent);
        AkSoundEngine.SetRTPCValue("RTPC_LeafBoomerang_Position", percentage);

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
        dh.Damage = _damageBack;
        _onComeback?.Invoke();
    }

    private void Die() {
        _onDeath?.Invoke(this);
        Destroy(gameObject);
    }
}
