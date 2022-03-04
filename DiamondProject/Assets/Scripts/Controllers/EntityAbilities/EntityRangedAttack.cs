using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class EntityRangedAttack : MonoBehaviour {
    [SerializeField] GameObject _bullet;
    [SerializeField] int _damage;
    [SerializeField] float _bulletSpeed;
    [SerializeField] float _rangedAttackCooldown = 1f;

    public Tools.BasicDelegate<Vector2> OnAttack;

    bool _canRangeAttack = true;

    public void Attack(Vector2 direction) {
        if (!_canRangeAttack) { return; }

        OnAttack?.Invoke(direction);

        GameObject bull = Instantiate(_bullet, transform.position, Quaternion.identity);
        bull.GetComponent<AttackHitbox>().damage = _damage;
        bull.GetComponent<Rigidbody2D>().velocity = direction * _bulletSpeed;
        _canRangeAttack = false;
        StartCoroutine(Tools.Delay(() => _canRangeAttack = true, _rangedAttackCooldown));
    }
}
