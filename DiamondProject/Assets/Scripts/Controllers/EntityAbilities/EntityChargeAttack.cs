using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ToolsBoxEngine;
using System;

public class EntityChargeAttack : MonoBehaviour {
    [Header("Static")]
    [SerializeField] Transform _entity = null;
    [SerializeField] Transform _attackParent = null;
    [SerializeField] Animator _attackAnimator = null;
    [SerializeField] DamageHealth _attackHitbox = null;
    [SerializeField] LayerMask _walls;
    [Header("Values")]
    [SerializeField] float _dashDistance = 3f;
    [SerializeField] int _damage = 10;
    [SerializeField] int _fullChargedDamageBonus = 10;
    [SerializeField] float _cooldownTime = 1f;
    [SerializeField] float _chargingTime = 3f;
    [SerializeField] float _attackTime = 1f;
    [SerializeField] MultipleTagSelector _damageables = new MultipleTagSelector(MultipleTagSelector.State.EVERYTHING);
    [SerializeField] AnimationCurve _damagesOverTime = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] AnimationCurve _distanceOverTime = AnimationCurve.Linear(0, 0, 1, 1);
    //[SerializeField] AnimationCurve _attackTimeOverTime = AnimationCurve.Linear(0, 0, 1, 1);

    // Events
    [SerializeField] UnityEvent<Vector2> _onCharging;
    [SerializeField] UnityEvent<Vector2> _onAttack;
    [SerializeField] UnityEvent<GameObject> _onHit;
    [SerializeField] UnityEvent<GameObject> _onTrigger;

    [HideInInspector, SerializeField] UnityEvent<Vector2> _onOverCharge;
    [HideInInspector, SerializeField] UnityEvent _onAttackEnd;

    Vector2 _direction = Vector2.zero;
    float _chargingTimer = 0f;

    bool _isCharging = false;
    bool _isAttacking = false;

    Coroutine _routine_DashAttack;

    #region Properties

    public bool CanAttack => !IsAttacking;
    public bool IsAttacking => _isAttacking || _isCharging;
    public float MaxChargeTime => _chargingTime;

    #region Events

    public event UnityAction<Vector2> OnCharging { add => _onCharging.AddListener(value); remove => _onCharging.RemoveListener(value); }
    public event UnityAction<Vector2> OnAttack { add => _onAttack.AddListener(value); remove => _onAttack.RemoveListener(value); }
    public event UnityAction<Vector2> OnOverCharge { add => _onOverCharge.AddListener(value); remove => _onOverCharge.RemoveListener(value); }
    public event UnityAction<GameObject> OnHit { add => _onHit.AddListener(value); remove => _onHit.RemoveListener(value); }
    public event UnityAction<GameObject> OnTrigger { add => _onTrigger.AddListener(value); remove => _onTrigger.RemoveListener(value); }
    public event UnityAction OnAttackEnd { add => _onAttackEnd.AddListener(value); remove => _onAttackEnd.RemoveListener(value); }

    #endregion

    #endregion

    private void Awake() {
        _attackHitbox.OnCollide += _InvokeOnHit;
        _attackHitbox.OnTrigger += _InvokeOnTrigger;
    }

    private void Update() {
        AkSoundEngine.SetRTPCValue("RTPC_SwordCharge", _chargingTimer / _chargingTime);
        if (_isCharging) {
            _chargingTimer += Time.deltaTime;
            if (_chargingTimer >= _chargingTime) {
                StopCharging(_direction);
                _onOverCharge?.Invoke(_direction);
            }
        }
    }

    private void OnDestroy() {
        _attackHitbox.OnCollide -= _InvokeOnHit;
        _attackHitbox.OnTrigger -= _InvokeOnTrigger;
    }

    public void StartCharging(Vector2 direction) {
        if (_isCharging) { return; }
        _isCharging = true;
        UpdateDirection(direction);

        _chargingTimer = 0f;

        _onCharging?.Invoke(direction);
        _attackAnimator.SetBool("Charge Attack", true);
    }

    public void StopCharging(Vector2 direction) {
        if (!_isCharging) { return; }
        _isCharging = false;


        Attack(direction, _chargingTimer);
        _chargingTimer = 0f;
        _onAttack?.Invoke(direction);
        _attackAnimator.SetBool("Charge Attack", false);
    }

    public void Attack(Vector2 direction, float timer) {
        UpdateDirection(direction);

        float percentage = timer / _chargingTime;
        int damage = Mathf.CeilToInt(_damagesOverTime.Evaluate(percentage) * _damage);
        if (percentage >= 1f) { damage += _fullChargedDamageBonus; }
        _attackHitbox.SetValues(_damageables, damage);

        //float attackTime = _attackTimeOverTime.Evaluate(percentage) * _attackTime;
        float attackTime = _distanceOverTime.Evaluate(percentage) * _attackTime;
        float distance = _distanceOverTime.Evaluate(percentage) * _dashDistance;
        if (_routine_DashAttack != null) { StopCoroutine(_routine_DashAttack); }
        if (attackTime >= 0f) {
            _isAttacking = true;
            _attackAnimator.SetBool("Dash Attack", true);
            _routine_DashAttack = StartCoroutine(IDashAttack(direction, distance, attackTime, damage));
        } else {
            _onAttackEnd?.Invoke();
        }
    }

    IEnumerator IDashAttack(Vector2 direction, float distance, float time, int damage) {
        direction = direction.normalized;
        Vector2 position = _entity.Position2D();
        Vector2 target = direction * distance + position;
        float timePassed = 0f;
        _attackHitbox.SetValues(_damageables, damage);
        while (timePassed < time) {
            yield return new WaitForEndOfFrame();
            try {
                timePassed += Time.deltaTime;
                Vector2 nextPos = Vector2.Lerp(position, target, timePassed / time);
                RaycastHit2D hit = Physics2D.Linecast(_entity.Position2D(), nextPos);
                if (hit.collider != null) {
                    IHealth hitHealth = hit.collider?.gameObject?.GetComponent<IHealth>();
                    if (hitHealth != null) {
                        _attackHitbox.Hit(hit.collider);
                    }
                    if (_walls.Contains(hit.collider.gameObject.layer)) {
                        nextPos = hit.point;
                    }
                }
                _entity.position = nextPos.To3D(_entity.position.z, Axis.Z);
            } catch (Exception e) {
                Debug.LogException(e);
                break;
            }
        }

        _isAttacking = false;
        _attackAnimator.SetBool("Dash Attack", false);
        _onAttackEnd?.Invoke();
    }

    public void UpdateDirection(Vector2 direction) {
        _direction = direction;
        _attackParent.rotation = Quaternion.LookRotation(Vector3.forward, direction.To3D());
    }

    void _InvokeOnHit(GameObject obj) {
        _onHit?.Invoke(obj);
    }

    void _InvokeOnTrigger(GameObject obj) {
        _onTrigger?.Invoke(obj);
    }

    private void OnDrawGizmosSelected() {
        if (!IsAttacking) { return; }
        Gizmos.color = Color.red;
        float percentage = _chargingTimer / _chargingTime;
        float distance = _distanceOverTime.Evaluate(percentage) * _dashDistance;
        Gizmos.DrawLine(_entity.position.Override(5f, Axis.Z), _entity.position + (_direction * distance).To3D(5f));
    }
}
