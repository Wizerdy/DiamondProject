using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ToolsBoxEngine;

public class EntityRangedAttack : MonoBehaviour {
    [Header("Static")]
    [SerializeField] GameObject _bullet;
    [SerializeField] Animator _animator;
    [SerializeField] Transform _attackParent;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [Header("Values")]
    [SerializeField] int _damage;
    [SerializeField] MultipleTagSelector _damageables = new MultipleTagSelector(MultipleTagSelector.State.EVERYTHING);
    [SerializeField] float _bulletSpeed;
    [SerializeField] float _rangedAttackCooldown = 1f;
    [SerializeField] UnityEvent<Vector2> _onAttack;

    bool _canRangeAttack = true;

    public bool CanAttack => _canRangeAttack;

    public event UnityAction<Vector2> OnAttack { add => _onAttack.AddListener(value); remove => _onAttack.RemoveListener(value); }

    public void Attack(Vector2 direction) {
        if (!_canRangeAttack) { return; }
        _onAttack?.Invoke(direction);

        UpdateDirection(direction);
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, -direction.To3D()) * Quaternion.Euler(0f, 0f, 90f);
        GameObject bull = Instantiate(_bullet, transform.position, rotation);
        DamageHealth damageHealth = bull.GetComponent<DamageHealth>();
        damageHealth?.SetValues(_damageables, _damage);
        bull.GetComponent<Rigidbody2D>().velocity = direction * _bulletSpeed;
        _animator.SetTrigger("Range Attack");
        _canRangeAttack = false;
        StartCoroutine(Tools.Delay(() => _canRangeAttack = true, _rangedAttackCooldown));
    }

    public void UpdateDirection(Vector2 direction) {
        //if (_spriteRenderer != null) { _spriteRenderer.transform.localScale = _spriteRenderer.transform.localScale.Override(); }
        _attackParent.rotation = Quaternion.LookRotation(Vector3.forward, direction.To3D());
    }
}
