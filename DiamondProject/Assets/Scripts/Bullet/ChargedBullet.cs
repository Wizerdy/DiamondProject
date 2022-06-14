using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class ChargedBullet : MonoBehaviour {
    [Header("Reference")]
    [SerializeField] Rigidbody2D _rigidbody = null;
    [SerializeField] DamageHealth _damageHealth = null;

    [Header("Values")]

    [SerializeField] int _damage = 10;
    [SerializeField] int _fullChargedDamageBonus = 10;
    [SerializeField] MultipleTagSelector _damageables = new MultipleTagSelector(MultipleTagSelector.State.EVERYTHING);
    [SerializeField, Range(0f, 1f)] float _deflection = 0f;
    [SerializeField, Tooltip("0f = infinite")] float _range = 0f;
    [SerializeField] float _speed = 30f;
    [SerializeField] float _cooldownTime = 1f;
    [SerializeField] float _recoil = 3f;
    [SerializeField] float _recoilTime = 1f;
    [SerializeField] float _chargingTime = 3f;
    [SerializeField] AnimationCurve _damagesOverTime = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] AnimationCurve _recoilOverTime = AnimationCurve.Linear(0, 0, 1, 1);

    float _lifetime = 0f;
    float _timer = 0f;
    Vector2 _direction = Vector2.up;

    #region Properties

    public float Cooldown => _cooldownTime;
    public float ChargingTime => _chargingTime;
    public float RecoilTime => _recoilTime;

    #endregion

    void Reset() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    #region Builder

    public ChargedBullet SetDamage(int damage) {
        _damage = damage;
        return this;
    }

    public ChargedBullet SetDeflection(float deflection) {
        _deflection = deflection;
        return this;
    }

    public ChargedBullet SetRange(float range) {
        _range = range;
        return this;
    }

    public ChargedBullet SetSpeed(float speed) {
        _speed = speed;
        return this;
    }

    #endregion

    public Vector2 ComputeDirection(Vector2 direction) {
        float angleMax = _deflection * 360f;
        float angle = Random.Range(0f, angleMax) - (angleMax / 2f);
        direction = Quaternion.Euler(0f, 0f, angle) * direction;

        _direction = direction;
        return direction;
    }

    public float Recoil(float charge) {
        return _recoilOverTime.Evaluate(charge) * _recoil;
    }

    public void Launch(Vector2 direction, float charge = 1f) {
        if (_rigidbody == null) { return; }
        if (_speed == 0f) { Debug.LogError("Can't divide by 0 btw (speed)"); return; }

        Vector2 velocity = direction * _speed;
        _rigidbody.velocity = velocity;

        int damage = Mathf.RoundToInt(_damage * _damagesOverTime.Evaluate(charge)) + (charge >= 1f ? _fullChargedDamageBonus : 0);
        _damageHealth.Damage = damage;
        _damageHealth.Damageables = _damageables;
        _timer = 0f;

        _lifetime = _range / _speed;

        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, -direction.To3D()) * Quaternion.Euler(0f, 0f, 90f);
        transform.rotation = rotation;
    }

    //void OnEnable() {
    //    Launch();
    //}

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
