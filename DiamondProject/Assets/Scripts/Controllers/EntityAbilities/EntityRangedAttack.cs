using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ToolsBoxEngine;

public class EntityRangedAttack : MonoBehaviour {
    [Header("Static")]
    [SerializeField] GameObject _bullet;
    [Header("Values")]
    [SerializeField] int _damage;
    [SerializeField] MultipleTagSelector _damageables = new MultipleTagSelector(MultipleTagSelector.State.EVERYTHING);
    [SerializeField] float _bulletSpeed;
    [SerializeField] float _rangedAttackCooldown = 1f;
    [SerializeField] UnityEvent<Vector2> _onAttack;

    public event UnityAction<Vector2> OnAttack { add => _onAttack.AddListener(value); remove => _onAttack.RemoveListener(value); }

    bool _canRangeAttack = true;

    public void Attack(Vector2 direction) {
        if (!_canRangeAttack) { return; }
        _onAttack?.Invoke(direction);

        GameObject bull = Instantiate(_bullet, transform.position, Quaternion.identity);
        DamageHealth damageHealth = bull.GetComponent<DamageHealth>();
        if (damageHealth != null) {
            damageHealth.Damage = _damage;
            damageHealth.Damageables = _damageables;
        }
        bull.GetComponent<Rigidbody2D>().velocity = direction * _bulletSpeed;
        _canRangeAttack = false;
        StartCoroutine(Tools.Delay(() => _canRangeAttack = true, _rangedAttackCooldown));
    }
}
