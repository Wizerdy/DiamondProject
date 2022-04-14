using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseAttack : MonoBehaviour {
    [SerializeField] UnityEvent<BaseAttack> OnExecute;
    [SerializeField] UnityEvent<BaseAttack> OnEnd;
    [SerializeField] Reference<AttackSystem> attackSystem;
    [SerializeField] public string id = "";
    [SerializeField] protected float duration = 1;
    [SerializeField] protected float coolDown = 1;
    [SerializeField] protected bool isPlaying = false;

    private void OnEnable() {
        Execute();
        attackSystem?.Instance?.Register(this);
    }

    private void OnDisable() {
        attackSystem?.Instance?.Unregister(this);
    }

    public void Execute() {
        isPlaying = true;
        StartCoroutine(ILaunch());
        OnExecute?.Invoke(this);
    }

    protected IEnumerator ILaunch() {
        yield return StartCoroutine(IExecute());
        End();
    }

    protected abstract IEnumerator IExecute();

    protected virtual void End() {
        isPlaying = false;
        OnEnd?.Invoke(this);
        gameObject.SetActive(false);
    }
}
