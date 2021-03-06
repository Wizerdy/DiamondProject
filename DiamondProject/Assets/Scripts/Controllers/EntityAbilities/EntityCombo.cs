using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ToolsBoxEngine;

public class EntityCombo : MonoBehaviour {
    [Header("Static")]
    [SerializeField] Transform _attackParent = null;
    [SerializeField] Animator _attackAnimator = null;
    [SerializeField] DamageHealth _attackHitbox = null;
    [Header("Values")]
    [SerializeField] int[] _damage = new int[3];
    [SerializeField] float _cooldownTime = 1f;
    [SerializeField] MultipleTagSelector _damageables = new MultipleTagSelector(MultipleTagSelector.State.EVERYTHING);
    [SerializeField] UnityEvent<Vector2> _onAttack;
    [SerializeField] UnityEvent<GameObject> _onHit;

    public event UnityAction<Vector2> OnAttack { add => _onAttack.AddListener(value); remove => _onAttack.RemoveListener(value); }
    public event UnityAction<GameObject> OnHit { add => _onHit.AddListener(value); remove => _onHit.RemoveListener(value); }

    bool _isAttacking = false;
    int _currentCombo = 0;

    public bool CanAttack => !_isAttacking;
    public bool IsAttacking => _isAttacking;

    private void Awake() {
        _attackHitbox.OnCollide += _InvokeOnHit;
    }

    private void Start() {
        _attackHitbox.Damage = _damage[_currentCombo];
        _attackHitbox.Damageables = _damageables;
    }

    private void OnDestroy() {
        _attackHitbox.OnCollide -= _InvokeOnHit;
    }

    public void Attack(Vector2 direction) {
        if (_isAttacking) { return; }
        _isAttacking = true;
        _attackAnimator.SetBool("Combo", true);
        _onAttack?.Invoke(direction);
    }

    public void InstanceDamage() {

    }

    public void UpdateDirection(Vector2 direction) {
        _attackParent.rotation = Quaternion.LookRotation(Vector3.forward, direction.To3D());
    }

    void _InvokeOnHit(GameObject obj) {
        _onHit?.Invoke(obj);
    }
}
