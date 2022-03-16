using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public Tools.BasicDelegate<Vector2> OnAttack;
    public Tools.BasicDelegate<GameObject> OnHit;

    bool isAttacking = false;

    public bool CanAttack => !isAttacking;
    public bool IsAttacking => isAttacking;

    private void Awake() {
        _attackHitbox.OnCollide += OnHit;
    }

    private void Start() {
        _attackHitbox.Damage = _damage;
        _attackHitbox.Damageables = _damageables;
    }

    private void OnDestroy() {
        _attackHitbox.OnCollide -= OnHit;
    }

    public void Attack(Vector2 direction) {
        if (isAttacking) { return; }
        isAttacking = true;
        _attackParent.rotation = Quaternion.LookRotation(Vector3.forward, direction.To3D());
        _attackAnimator.SetTrigger("Attack");
        OnAttack?.Invoke(direction);
        StartCoroutine(Tools.Delay(() => isAttacking = false, _cooldownTime));
    }
}
