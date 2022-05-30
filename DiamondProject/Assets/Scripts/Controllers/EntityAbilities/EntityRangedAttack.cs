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
    [Header("Default bullet values")]
    [SerializeField] int _damage;
    [SerializeField] MultipleTagSelector _damageables = new MultipleTagSelector(MultipleTagSelector.State.EVERYTHING);
    [SerializeField] float _bulletSpeed;
    [SerializeField] float _cooldown = 1f;

    [Space]
    [SerializeField] UnityEvent<Vector2> _onAttack;

    bool _canRangeAttack = true;

    #region Properties

    public bool CanAttack => _canRangeAttack;

    public event UnityAction<Vector2> OnAttack { add => _onAttack.AddListener(value); remove => _onAttack.RemoveListener(value); }

    #endregion

    public void Attack(Vector2 direction) {
        if (!_canRangeAttack) { return; }
        _onAttack?.Invoke(direction);

        Vector2 bulletDirection = direction;
        GameObject bull = Instantiate(_bullet, transform.position, Quaternion.identity);
        Bullet bullet = bull.GetComponent<Bullet>();
        if (bull == null) {
            DamageHealth damageHealth = bull.GetComponent<DamageHealth>();
            damageHealth?.SetValues(_damageables, _damage);
            bull.GetComponent<Rigidbody2D>().velocity = direction * _bulletSpeed;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, -direction.To3D()) * Quaternion.Euler(0f, 0f, 90f);
            bull.transform.rotation = rotation;
        } else {
            bulletDirection = bullet.ComputeDirection(direction);
            bullet.Launch();
        }
        UpdateDirection(bulletDirection);
        _animator.SetTrigger("Range Attack");
        _canRangeAttack = false;
        StartCoroutine(Tools.Delay(() => _canRangeAttack = true, _cooldown));
    }

    public void UpdateDirection(Vector2 direction) {
        //if (_spriteRenderer != null) { _spriteRenderer.transform.localScale = _spriteRenderer.transform.localScale.Override(); }
        _attackParent.rotation = Quaternion.LookRotation(Vector3.forward, direction.To3D());
    }

    public void SetBullet(GameObject bullet) {
        _bullet = bullet;
        _cooldown = bullet.GetComponent<Bullet>()?.Cooldown ?? _cooldown;
    }
}
