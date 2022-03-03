using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAction : MonoBehaviour, IAction {
    [SerializeField] protected Reference<Boss> _boss;

    [SerializeField] protected float _duration = 1;
    [SerializeField] protected float _durationTimer = 1;
    [SerializeField] protected float transitionTime = 1;
    public float Duration { get { return _duration; } set { _duration = value; } }

    [SerializeField] delegate void ActionDelegate(IEnumerator action);

    public abstract Boss.State GetState();
    public abstract IEnumerator StartAction();
}
