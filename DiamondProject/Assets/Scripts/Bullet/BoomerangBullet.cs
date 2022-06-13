using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class BoomerangBullet : MonoBehaviour {
    [SerializeField] Rigidbody2D _rigidbody = null;
    [SerializeField] DamageHealth _damageHealth = null;
    [Header("Values")]
    [SerializeField] float _backTime = 2f;
    [SerializeField] float _backSpeed = 10f;
    [SerializeField] int _backDamage = 5;

    private void Reset() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _damageHealth = GetComponent<DamageHealth>();
    }

    void OnEnable() {
        if (_backTime <= 0f) { return; }
        StartCoroutine(Tools.Delay(GoBack, _backTime));
    }

    void GoBack() {
        if (_rigidbody == null) { return; }
        _damageHealth?.SetValues(null, _backDamage);
        //_damageHealth?.ResetHitted();
        _rigidbody.velocity = -_rigidbody.velocity.normalized * _backSpeed;
    }
}
