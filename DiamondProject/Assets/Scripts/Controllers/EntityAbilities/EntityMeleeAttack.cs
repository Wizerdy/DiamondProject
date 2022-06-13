using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ToolsBoxEngine;

public class EntityMeleeAttack : MonoBehaviour {
    [Header("Static")]
    [SerializeField] Transform _attackParent = null;
    [SerializeField] Animator _attackAnimator = null;
    [SerializeField] DamageHealth _attackHitbox = null;
    [Header("Values")]
    [SerializeField] int _damage = 10;
    [SerializeField] float _cooldownTime = 1f;
    [SerializeField] MultipleTagSelector _damageables = new MultipleTagSelector(MultipleTagSelector.State.EVERYTHING);
    [SerializeField] UnityEvent<Vector2> _onAttack;
    [SerializeField] UnityEvent<GameObject> _onHit;
    [SerializeField] UnityEvent<GameObject> _onTrigger;

    public event UnityAction<Vector2> OnAttack { add => _onAttack.AddListener(value); remove => _onAttack.RemoveListener(value); }
    public event UnityAction<GameObject> OnHit { add => _onHit.AddListener(value); remove => _onHit.RemoveListener(value); }
    public event UnityAction<GameObject> OnTrigger { add => _onTrigger.AddListener(value); remove => _onTrigger.RemoveListener(value); }

    bool _isAttacking = false;

    public bool CanAttack => !_isAttacking;
    public bool IsAttacking => _isAttacking;

    private void Awake() {
        _attackHitbox.OnCollide += _InvokeOnHit;
        _attackHitbox.OnTrigger += _InvokeOnTrigger;
    }

    private void OnDestroy() {
        _attackHitbox.OnCollide -= _InvokeOnHit;
        _attackHitbox.OnTrigger -= _InvokeOnTrigger;
    }

    public void Attack(Vector2 direction) {
        if (_isAttacking) { return; }
        _isAttacking = true;
        _attackHitbox.SetValues(_damageables, _damage);
        _attackParent.rotation = Quaternion.LookRotation(Vector3.forward, direction.To3D());
        _attackAnimator.SetTrigger("Slash Attack");
        _onAttack?.Invoke(direction);
        StartCoroutine(Tools.Delay(() => _isAttacking = false, _cooldownTime));
    }

    void _InvokeOnHit(GameObject obj) {
        _onHit?.Invoke(obj);
    }

    void _InvokeOnTrigger(GameObject obj) {
        _onTrigger?.Invoke(obj);
    }
}
