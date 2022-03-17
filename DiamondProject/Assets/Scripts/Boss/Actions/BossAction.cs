using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAction : MonoBehaviour, IAction {
    [Header("For Prog: ")]
    [SerializeField] protected Reference<Boss> _boss;
    [Header("For GD: ")]
    [SerializeField] protected string _name = "Action";
    [SerializeField] protected float _duration = 1;
    protected float _durationTimer = 1;
    [SerializeField] protected float _transitionTime = 1;
    [SerializeField] protected string nextState = "Transition";
    public string Name { get { return _name; } }
    public float Duration { get { return _duration; } set { _duration = value; } }

    public abstract Boss.State GetState();
    public abstract void StartAction();

    protected void Wait() {
        StartCoroutine(StartWaiting());
    }
    protected virtual IEnumerator StartWaiting() {
        Debug.Log(GetState());
        _durationTimer = _duration;
        while (_durationTimer > 0) {
            yield return null;
            _durationTimer -= Time.deltaTime;
        }
        _boss.Instance.RemoveCoroutines(this);
        NextState();
    }

    protected void NextState() {
        if (nextState == "Transition") {
            _boss.Instance.NextState(nextState, _transitionTime);
        } else {
            _boss.Instance.NextState(nextState);
        }
    }
}
