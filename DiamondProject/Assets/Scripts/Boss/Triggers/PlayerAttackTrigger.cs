using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum AttackType {
    ATTACK,
    MELEE,
    RANGE
}

public class PlayerAttackTrigger : Trigger {
    [Space]
    [SerializeField] Reference<PlayerController> _player;
    [Header("System")]
    [SerializeField] bool _shouldAttack;
    [SerializeField] AttackType _attackType;
    [SerializeField] float _timeBeforeReset;
    [SerializeField] int _attackNumberToReach;

    int _attackNumber;
    bool _isActivable;

    Coroutine _routine_ResetTrigger;

    private void Start() {
        if (!_shouldAttack) {
            _routine_ResetTrigger = StartCoroutine(ResetTrigger());
        }
        if (_player != null) { _player.Instance.OnAttack += PlayerHasAttacked; }
    }

    private void OnDestroy() {
        if (_player != null) { _player.Instance.OnAttack -= PlayerHasAttacked; }
    }

    public override bool IsSelfTrigger() {
        if (_isActivable && ((_attackNumberToReach <= _attackNumber && _shouldAttack) || (_attackNumber == 0 && !_shouldAttack))) {
            return true;
        }
        return false;
    }

    public void PlayerHasAttacked(AttackType attackType) {
        if (_shouldAttack) {
            if (_isActivable && (_attackType == AttackType.ATTACK || attackType == _attackType)) {
                _attackNumber++;
            } else if (_isActivable) {
                _routine_ResetTrigger = StartCoroutine(ResetTrigger());
            }
        } else {
            StopCoroutine(_routine_ResetTrigger);
            _routine_ResetTrigger = StartCoroutine(ResetTrigger());
        }
    }

    IEnumerator ResetTrigger() {
        _attackNumber = 0;
        _isActivable = false;
        yield return new WaitForSeconds(_timeBeforeReset);
        _isActivable = true;
    }
}
