using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAction : MonoBehaviour, IAction {
    [SerializeField] protected Reference<Boss> _boss;

    [SerializeField] protected float _duration = 1;
    protected float _durationTimer = 1;
    [SerializeField] protected float _transition = 1;
    public float Duration { get { return _duration; } set { _duration = value; } }

    [SerializeField] delegate void ActionDelegate(IEnumerator action);

    public abstract Boss.State GetState();
    public abstract void StartAction();

    protected void Wait() {
        StartCoroutine(StartWaiting());
    }
    protected virtual IEnumerator StartWaiting() {
        _durationTimer = _duration;
        while (_durationTimer > 0) {
            yield return null;
            _durationTimer -= Time.deltaTime;
        }
        _boss.Instance.RemoveCoroutines(this);
        _boss.Instance.EndState(_transition);
    }

}
