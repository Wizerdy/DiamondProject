using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : BossAction {
    [SerializeField] VisualEffectReference visualEffect;
    public override void StartAction() {
        visualEffect.Instance.AddColor(Color.magenta, 2, _duration);
        _boss.Instance.ChangeState(GetState());
        Wait();
    }

    public override Boss.State GetState() {
        return Boss.State.TRANSITION;
    }

    protected override IEnumerator StartWaiting() {
        _durationTimer = _duration;
        while (_durationTimer > 0) {
            yield return null;
            _durationTimer -= Time.deltaTime;
        }
        _boss.Instance.NewState();
    }
}
