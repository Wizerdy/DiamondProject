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
    [SerializeField] bool _shouldAttack;
    [SerializeField] AttackType _attackType;
    [SerializeField] float _timeBeforeReset;
    [SerializeField] int _attackNumberToReach;
    [SerializeField] int _attackNumber;
    [SerializeField] bool _isActivable;
    [SerializeField] Coroutine _coroutine;

    public UnityAction<AttackType> _playerHasAttacked;

    private void Start() {
        _playerHasAttacked += PlayerHasAttacked;
        if (!_shouldAttack) {
            _coroutine = StartCoroutine(ResetTrigger());
        }
    }

    public override bool IsSelfTrigger() {
        if(_isActivable && ((_attackNumberToReach <= _attackNumber && _shouldAttack) || (_attackNumber == 0 && !_shouldAttack))) {
            return true;
        }
        return false;
    }
    
    void PlayerHasAttacked(AttackType attackType) {
        if (_shouldAttack) {
            if (_isActivable && (_attackType == AttackType.ATTACK || attackType == _attackType)) {
                _attackNumber++;
            } else if (_isActivable) {
                _coroutine = StartCoroutine(ResetTrigger());
            }
        } else {
            StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(ResetTrigger());
        }
    }

    IEnumerator ResetTrigger() {
        _attackNumber = 0;
        _isActivable = false;
        yield return new WaitForSeconds(_timeBeforeReset);
        _isActivable = true;
    }
    
}
