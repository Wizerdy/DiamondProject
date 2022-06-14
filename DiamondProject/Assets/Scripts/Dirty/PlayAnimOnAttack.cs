using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class PlayAnimOnAttack : MonoBehaviour {

    [SerializeField] BaseAttack _attack;
    [SerializeField] IMeetARealBossReference _boss;

    [SerializeField] string _trigger = "";
    [SerializeField] bool _onExecute = true;
    [SerializeField] bool _onCast = false;
    [SerializeField] bool _onEnd = false;

    private void Reset() {
        _attack = GetComponent<BaseAttack>();
    }

    void Awake() {
        _attack.OnExecute += _onExecute ? _PlayCastBool : _StopAttackBool;
        _attack.OnCast += _onCast ? _PlayCastBool : _StopAttackBool;
        _attack.OnEnd += _onEnd ? _PlayCastBool : _StopAttackBool;
    }

    void OnDestroy() {
        _attack.OnExecute -= _onExecute ? _PlayCastBool : _StopAttackBool;
        _attack.OnCast -= _onCast ? _PlayCastBool : _StopAttackBool;
        _attack.OnEnd -= _onEnd ? _PlayCastBool : _StopAttackBool;
    }

    private void _PlayCastBool(BaseAttack attack) {
        _boss.Instance.SetAnimatorBool(_trigger, true);
    }

    private void _StopAttackBool(BaseAttack attack) {
        _boss.Instance.SetAnimatorBool(_trigger, false);
    }
}
