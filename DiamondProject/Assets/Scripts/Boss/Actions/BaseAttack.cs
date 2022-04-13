using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAttack : MonoBehaviour {
    [SerializeField] AttackSystem attackSystem;
    [SerializeField] public string id = "";
    [SerializeField] protected float duration = 1;
    [SerializeField] protected float coolDown = 1;
    [SerializeField] protected bool isPlaying = false;

    private void OnEnable() {
        Execute();
        attackSystem?.Register(this);
    }

    private void OnDisable() {
        attackSystem?.Unregister(this);
    }

    public void Execute() {
        isPlaying = true;
        StartCoroutine(ILaunch());
    }

    protected IEnumerator ILaunch() {
        yield return StartCoroutine(IExecute());
        End();
    }

    protected abstract IEnumerator IExecute();

    protected virtual void End() {
        isPlaying = false;
        gameObject.SetActive(false);
    }
}
