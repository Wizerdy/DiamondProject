using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ToolsBoxEngine;

public enum AttackType {
    ATTACK,
    MELEE,
    RANGE
}

public class PlayerAttackTrigger : Trigger {
    [Space]
    [SerializeField] Reference<PlayerController> _player;
    [Header("System")]
    //[SerializeField] bool _shouldAttack;
    [SerializeField] int _attackNumberToReach = 5;
    [SerializeField] AttackType _attackType = AttackType.ATTACK;
    [SerializeField] Comparison _condition = Comparison.GREATER_EQUAL;
    [SerializeField] bool _resetOnError = true;
    [SerializeField] float _timeBeforeReset = 2f;

    int _attackNumber = 0;
    bool _isTriggerable = true;

    Coroutine _routine_ResetTrigger;

    public bool IsTriggerable { get => _isTriggerable; set => _isTriggerable = value; }

    private void Start() {
        if (_player != null) { _player.Instance.OnAttack += PlayerHasAttacked; }
    }

    private void OnDestroy() {
        if (_player != null) { _player.Instance.OnAttack -= PlayerHasAttacked; }
    }

    public override bool IsSelfTrigger() {
        return _isTriggerable && _condition.Compare(_attackNumber, _attackNumberToReach);

        //if (_isActivable && ((_attackNumberToReach <= _attackNumber && _shouldAttack) || (_attackNumber == 0 && !_shouldAttack))) {
        //    return true;
        //}
    }

    public void PlayerHasAttacked(AttackType attackType) {
        if (!_isTriggerable) { return; }
        if (_attackType != AttackType.ATTACK && attackType != _attackType) {
            if (_resetOnError) {
                _attackNumber = 0;
            }
            return;
        }

        ++_attackNumber;
        if (_timeBeforeReset <= 0) { return; }
        if (_routine_ResetTrigger != null) { StopCoroutine(_routine_ResetTrigger); }
        _routine_ResetTrigger = StartCoroutine(ResetTrigger());

        //if (_shouldAttack) {
        //    if (_isActivable && (_attackType == AttackType.ATTACK || attackType == _attackType)) {
        //        _attackNumber++;
        //    } else if (_isActivable) {
        //        _routine_ResetTrigger = StartCoroutine(ResetTrigger());
        //    }
        //} else {
        //    StopCoroutine(_routine_ResetTrigger);
        //    _routine_ResetTrigger = StartCoroutine(ResetTrigger());
        //}
    }

    IEnumerator ResetTrigger() {
        yield return new WaitForSeconds(_timeBeforeReset);
        ResetAttacks();
    }

    public void ResetAttacks() {
        _attackNumber = 0;
    }
}
