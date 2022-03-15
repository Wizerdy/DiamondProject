using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class EntityMeleeAttack : MonoBehaviour {
    [SerializeField] Transform _attackParent = null;
    [SerializeField] GameObject _sword = null;
    [SerializeField] Animator _attackAnimator = null;
    [SerializeField] AttackHitbox _attackHitbox = null;
    [SerializeField] int _damage = 10;

    public Tools.BasicDelegate<Vector2> OnAttack;
    public Tools.BasicDelegate<Collider2D> OnHit;

    bool isAttacking = false;

    public bool CanAttack => !isAttacking;
    public bool IsAttacking => isAttacking;

    private void Awake() {
        _attackHitbox.OnHit += OnHit;
    }

    private void Start() {
        _sword.GetComponent<AttackHitbox>().damage = _damage;
    }

    private void OnDestroy() {
        _attackHitbox.OnHit -= OnHit;
    }

    public void Attack(Vector2 direction) {
        isAttacking = true;
        _attackParent.rotation = Quaternion.LookRotation(Vector3.forward, direction.To3D());
        _attackAnimator.SetTrigger("Attack");
        OnAttack?.Invoke(direction);
    }

    public void NotAttacking() {
        isAttacking = false;
    }
}
