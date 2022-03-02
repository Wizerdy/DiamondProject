using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : BossAction {
    public override IEnumerator StartAction() {
        Debug.Log("Transition");
        _boss.Instance.ChangeState(GetState());
        _durationTimer = _duration;
        while (_durationTimer > 0) {
            yield return null;
            _durationTimer -= Time.deltaTime;
        }
        _boss.Instance.NewState();
    }

    public override Boss.State GetState() {
        return Boss.State.TRANSITION;
    }
}
