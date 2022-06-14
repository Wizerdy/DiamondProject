using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ToolsBoxEngine;

public class EntityChargeRanged : MonoBehaviour {
    [Header("Static")]
    //[SerializeField] GameObject _bullet;
    [SerializeField] Transform _entity = null;
    [SerializeField] Transform _attackParent = null;
    [SerializeField] Animator _attackAnimator = null;
    [SerializeField] SpriteRenderer _spriteRenderer = null;
    [SerializeField] LayerMask _walls;
    [Header("Default values")]
    [SerializeField] float _bulletSpeed = 30f;
    [SerializeField] float _recoilDistance = 3f;
    [SerializeField] int _damage = 10;
    [SerializeField] int _fullChargedDamageBonus = 10;
    [SerializeField] float _cooldownTime = 1f;
    [SerializeField] float _chargingTime = 3f;
    [SerializeField] float _recoilTime = 1f;
    [SerializeField] MultipleTagSelector _damageables = new MultipleTagSelector(MultipleTagSelector.State.EVERYTHING);
    [SerializeField] AnimationCurve _damagesOverTime = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] AnimationCurve _recoilOverTime = AnimationCurve.Linear(0, 0, 1, 1);

    // Events
    [SerializeField] UnityEvent<Vector2> _onCharging;
    [SerializeField] UnityEvent<Vector2> _onAttack;
    [SerializeField] UnityEvent<GameObject> _onHit;
    [SerializeField] UnityEvent<GameObject> _onTrigger;

    [HideInInspector, SerializeField] UnityEvent<Vector2> _onOverCharge;
    [HideInInspector, SerializeField] UnityEvent _onAttackEnd;

    GameObject _bullet = null;
    Vector2 _direction = Vector2.zero;
    float _chargingTimer = 0f;

    bool _canRangeAttack = true;
    bool _isCharging = false;
    bool _isAttacking = false;

    bool _thunderArrow = false;

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

    private void Update() {
        AkSoundEngine.SetRTPCValue("RTPC_CrossbowCharge", _chargingTimer / _chargingTime);
        if (_isCharging) {
            _chargingTimer += Time.deltaTime;
            if (_chargingTimer >= _chargingTime) {
                StopCharging(_direction);
                _onOverCharge?.Invoke(_direction);
            }
        }
    }

    public void SetBullet(GameObject bullet) {
        _bullet = bullet;
        ChargedBullet chargedBullet = _bullet.GetComponent<ChargedBullet>();
        if (chargedBullet == null) { Debug.Log("No Charged Bullet Assigned"); return; }
        _chargingTime = chargedBullet.ChargingTime;
        _cooldownTime = chargedBullet.Cooldown;
        _recoilTime = chargedBullet.RecoilTime;
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
        _chargingTimer = 0f;
        _onAttack?.Invoke(direction);
        _attackAnimator.SetBool("Charge Range", false);
    }

    public void Attack(Vector2 direction, float timer) {
        if (!_canRangeAttack) { return; }
        _onAttack?.Invoke(direction);
        _isAttacking = true;

        ChargedBullet chargedBullet = _bullet.GetComponent<ChargedBullet>();
        direction = chargedBullet?.ComputeDirection(direction) ?? direction;
        UpdateDirection(direction);
        float percentage = timer / _chargingTime;

        // Damage
        int damage = Mathf.CeilToInt(_damagesOverTime.Evaluate(percentage) * _damage);
        if (percentage >= 1f) { damage += _fullChargedDamageBonus; }

        // Recoil
        float attackTime;
        float distance;

        if (chargedBullet == null) {
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, -direction.To3D()) * Quaternion.Euler(0f, 0f, 90f);
            GameObject bull = Instantiate(_bullet, transform.position, rotation);
            DamageHealth damageHealth = bull.GetComponent<DamageHealth>();
            damageHealth?.SetValues(_damageables, damage);
            if (damageHealth != null) { damageHealth.OnCollide += _InvokeOnHit; damageHealth.OnTrigger += _InvokeOnTrigger; }
            bull.GetComponent<Rigidbody2D>().velocity = direction * _bulletSpeed;
            attackTime = _recoilOverTime.Evaluate(percentage) * _recoilTime;
            distance = _recoilOverTime.Evaluate(percentage) * _recoilDistance;
        } else {
            ChargedBullet lastBullet = Instantiate(_bullet, transform.position, Quaternion.identity).GetComponent<ChargedBullet>();
            if (_thunderArrow) { lastBullet.ThunderStruck(); }
            lastBullet.Launch(direction, percentage);
            attackTime = lastBullet.RecoilTime;
            distance = lastBullet.Recoil(percentage);

            DamageHealth damageHealth = lastBullet.GetComponent<DamageHealth>();
            if (damageHealth != null) { damageHealth.OnCollide += _InvokeOnHit; damageHealth.OnTrigger += _InvokeOnTrigger; }
        }

        NoThunderArrow();

        if (_routine_DashAttack != null) { StopCoroutine(_routine_DashAttack); } 
        _routine_DashAttack = StartCoroutine(IDashAttack(-direction, distance, attackTime));

        _attackAnimator.SetTrigger("Range Attack");
        _canRangeAttack = false;
        StartCoroutine(Tools.Delay(() => _canRangeAttack = true, _cooldownTime));
    }

    IEnumerator IDashAttack(Vector2 direction, float distance, float time) {
        if (time <= 0f) { yield break; }
        direction = direction.normalized;
        Vector2 position = _entity.Position2D();
        Vector2 target = direction * distance + position;
        float timePassed = 0f;
        while (timePassed < time) {
            yield return new WaitForEndOfFrame();
            timePassed += Time.deltaTime;
            Vector2 nextPos = Vector2.Lerp(position, target, timePassed / time);
            RaycastHit2D hit = Physics2D.Linecast(_entity.Position2D(), nextPos, _walls);
            if (hit.collider != null) {
                nextPos = hit.point;
            }
            _entity.position = nextPos.To3D(_entity.position.z, Axis.Z);
        }

        _isAttacking = false;
        _onAttackEnd?.Invoke();
    }

    public void UpdateDirection(Vector2 direction) {
        _direction = direction;
        _spriteRenderer.flipY = direction.x > 0;
        _attackParent.rotation = Quaternion.LookRotation(Vector3.forward, direction.To3D());
    }

    void _InvokeOnHit(GameObject obj) {
        _onHit?.Invoke(obj);
    }

    void _InvokeOnTrigger(GameObject obj) {
        _onTrigger?.Invoke(obj);
    }

    public void ThunderArrow() {
        if (!_isCharging) { return; }
        _thunderArrow = true;
        _spriteRenderer?.material.SetInteger("_Active", 1);
    }

    public void NoThunderArrow() {
        _thunderArrow = false;
        _spriteRenderer?.material.SetInteger("_Active", 0);
    }

    private void OnDrawGizmosSelected() {
        if (!IsAttacking) { return; }
        Gizmos.color = Color.red;
        float percentage = _chargingTimer / _chargingTime;
        float distance = _recoilOverTime.Evaluate(percentage) * _recoilDistance;
        Gizmos.DrawLine(_entity.position.Override(5f, Axis.Z), _entity.position + (_direction * distance).To3D(5f));
    }
}
