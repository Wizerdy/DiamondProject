using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : BossAction {
    [SerializeField] VisualEffectReference visualEffect;
    public override IEnumerator StartAction() {
        Debug.Log("Transition");
        visualEffect.Instance.AddColor(Color.magenta, 2, _duration);
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
