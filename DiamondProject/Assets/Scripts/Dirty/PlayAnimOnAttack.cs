using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimOnAttack : MonoBehaviour {
    [SerializeField] BaseAttack _attack;
    [SerializeField] IMeetARealBoss _boss;

    [SerializeField] string _onCast = "";
    [SerializeField] string _onAttack = "";

    void Awake() {
        _attack.OnExecute += _PlayCastTrigger;
        _attack.OnCast += _PlayAttackTrigger;
    }

    void OnDestroy() {
        _attack.OnExecute -= _PlayCastTrigger;
        _attack.OnCast -= _PlayAttackTrigger;
    }

    private void _PlayCastTrigger(BaseAttack attack) {
        _boss.SetAnimatorTrigger(_onCast);
    }

    private void _PlayAttackTrigger(BaseAttack attack) {
        _boss.SetAnimatorTrigger(_onAttack);
    }
}
