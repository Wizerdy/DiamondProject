using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class Bullet : MonoBehaviour {
    [Header("Reference")]
    [SerializeField] Rigidbody2D _rigidbody = null;
    [SerializeField] DamageHealth _damageHealth = null;

    [Header("Values")]
    [SerializeField] int _damage = 10;
    [SerializeField] MultipleTagSelector _damageables = new MultipleTagSelector(MultipleTagSelector.State.NONE);
    [SerializeField, Range(0f, 1f)] float _deflection = 0f;
    [Tooltip("0f = infinite")]
    [SerializeField] float _range = 0f;
    [SerializeField] float _cooldown = 0.2f;
    [SerializeField] float _speed = 30f;
    [SerializeField] int _overheat = 5;

    float _lifetime = 0f;
    float _timer = 0f;
    Vector2 _straightDirection = Vector2.up;
    Vector2 _direction = Vector2.up;

    void Reset() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    #region Properties

    public int Overheat => _overheat;
    public float Cooldown => _cooldown;

    #endregion

    #region Builder

    public Bullet SetDamage(int damage) {
        _damage = damage;
        return this;
    }

    public Bullet SetDeflection(float deflection) {
        _deflection = deflection;
        return this;
    }

    public Bullet SetRange(float range) {
        _range = range;
        return this;
    }

    public Bullet SetCooldown(float cooldown) {
        _cooldown = cooldown;
        return this;
    }

    public Bullet SetSpeed(float speed) {
        _speed = speed;
        return this;
    }

    public Bullet SetOverheat(int overheat) {
        _overheat = overheat;
        return this;
    }

    #endregion

    public Vector2 ComputeDirection(Vector2 direction) {
        _straightDirection = direction;

        float angleMax = _deflection * 360f;
        float angle = Random.Range(0f, angleMax) - (angleMax / 2f);
        direction = Quaternion.Euler(0f, 0f, angle) * direction;

        _direction = direction;
        return direction;
    }

    public void Launch() {
        if (_rigidbody == null) { return; }
        if (_speed == 0f) { Debug.LogError("Can't divide by 0 btw (speed)"); return; }

        //_direction = direction;

        //float angleMax = _deflection * 360f;
        //float angle = Random.Range(0f, angleMax) - (angleMax / 2f);
        //direction = Quaternion.Euler(0f, 0f, angle) * direction;
        Vector2 velocity = _direction * _speed;
        _rigidbody.velocity = velocity;

        _damageHealth.Damage = _damage;
        _damageHealth.Damageables = _damageables;
        _timer = 0f;

        _lifetime = _range / _speed;

        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, -_direction.To3D()) * Quaternion.Euler(0f, 0f, 90f);
        transform.rotation = rotation;
    }

    void OnEnable() {
        Launch();
    }

    void Update() {
        UpdateRange();
    }

    void UpdateRange() {
        if (_lifetime <= 0f) { return; }
        if (_timer < _lifetime) {
            _timer += Time.deltaTime;
        } else {
            _rigidbody.gameObject.SetActive(false);
            Destroy(_rigidbody.gameObject);
        }
    }
}
