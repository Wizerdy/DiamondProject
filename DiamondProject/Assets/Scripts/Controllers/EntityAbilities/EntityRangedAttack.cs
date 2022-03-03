using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class EntityRangedAttack : MonoBehaviour {
    [SerializeField] GameObject _bullet;
    [SerializeField] float _bulletSpeed;
    [SerializeField] float _rangedAttackCooldown = 1f;

    bool _canRangeAttack = true;

    public void Attack(Vector2 direction) {
        if (!_canRangeAttack) { return; }

        GameObject bull = Instantiate(_bullet, transform.position, Quaternion.identity);
        bull.GetComponent<Rigidbody2D>().velocity = direction * _bulletSpeed;
        _canRangeAttack = false;
        StartCoroutine(Tools.Delay(() => _canRangeAttack = true, _rangedAttackCooldown));
    }
}
