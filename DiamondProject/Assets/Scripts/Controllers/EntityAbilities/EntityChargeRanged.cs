using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ToolsBoxEngine;

public class EntityChargeRanged : MonoBehaviour {
    [Header("Static")]
    [SerializeField] Transform _entity = null;
    [SerializeField] Transform _attackParent = null;
    [SerializeField] Animator _attackAnimator = null;
    [SerializeField] DamageHealth _attackHitbox = null;
    [Header("Values")]
    [SerializeField] float _dashDistance = 3f;
    [SerializeField] int _damage = 10;
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

    #region Events

    public event UnityAction<Vector2> OnCharging { add => _onCharging.AddListener(value); remove => _onCharging.RemoveListener(value); }
    public event UnityAction<Vector2> OnAttack { add => _onAttack.AddListener(value); remove => _onAttack.RemoveListener(value); }
    public event UnityAction<Vector2> OnOverCharge { add => _onOverCharge.AddListener(value); remove => _onOverCharge.RemoveListener(value); }
    public event UnityAction<GameObject> OnHit { add => _onHit.AddListener(value); remove => _onHit.RemoveListener(value); }
    public event UnityAction OnAttackEnd { add => _onAttackEnd.AddListener(value); remove => _onAttackEnd.RemoveListener(value); }

    #endregion

    #endregion

    private void Awake() {
        _attackHitbox.OnCollide += _InvokeOnHit;
    }

    private void Update() {
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
    }

    public void StartCharging(Vector2 direction) {
        if (_isCharging) { return; }
        _isCharging = true;
        UpdateDirection(direction);

        _chargingTimer = 0f;

        _onCharging?.Invoke(direction);
        _attackAnimator.SetBool("Charge Range", true);
    }

    public void StopCharging(Vector2 direction) {
        if (!_isCharging) { return; }
        _isCharging = false;
        Attack(direction, _chargingTimer);
        _onAttack?.Invoke(direction);
        _attackAnimator.SetBool("Charge Range", false);
    }

    public void Attack(Vector2 direction, float timer) {
        UpdateDirection(direction);

        float percentage = timer / _chargingTime;
        int damage = Mathf.CeilToInt(_damagesOverTime.Evaluate(percentage) * _damage);
        _attackHitbox.SetValues(_damageables, damage);

        //float attackTime = _attackTimeOverTime.Evaluate(percentage) * _attackTime;
        float attackTime = _distanceOverTime.Evaluate(percentage) * _attackTime;
        float distance = _distanceOverTime.Evaluate(percentage) * _dashDistance;
        if (_routine_DashAttack != null) { StopCoroutine(_routine_DashAttack); }
        if (attackTime >= 0f) {
            _isAttacking = true;
            _attackAnimator.SetBool("Dash Attack", true);
            _routine_DashAttack = StartCoroutine(IDashAttack(direction, distance, attackTime));
        } else {
            _onAttackEnd?.Invoke();
        }
    }

    IEnumerator IDashAttack(Vector2 direction, float distance, float time) {
        direction = direction.normalized;
        Vector2 position = _entity.Position2D();
        Vector2 target = direction * distance + position;
        float timePassed = 0f;
        while (timePassed < time) {
            yield return new WaitForEndOfFrame();
            timePassed += Time.deltaTime;

            _entity.position = Vector2.Lerp(position, target, timePassed / time).To3D(_entity.position.z, Axis.Z);
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

    private void OnDrawGizmos() {
        if (!IsAttacking) { return; }
        Gizmos.color = Color.red;
        float percentage = _chargingTimer / _chargingTime;
        float distance = _distanceOverTime.Evaluate(percentage) * _dashDistance;
        Gizmos.DrawLine(_entity.position.Override(5f, Axis.Z), _entity.position + (_direction * distance).To3D(5f));
    }
}
