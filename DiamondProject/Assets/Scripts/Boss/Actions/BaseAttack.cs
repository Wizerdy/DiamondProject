using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseAttack : MonoBehaviour {
    [SerializeField] UnityEvent<BaseAttack> OnExecute;
    [SerializeField] UnityEvent<BaseAttack> OnEnd;
    [SerializeField] Reference<AttackSystem> attackSystem;
    [SerializeField] public string id = "";
    [SerializeField] protected BossReference _bossRef; // Fils de put
    [SerializeField] protected Reference<PlayerController> _playerRef; // Enculé
    [SerializeField] protected float duration = 1;
    [SerializeField] protected float coolDown = 1;
    [SerializeField] protected bool _dontNeedEnd = false;
    protected bool isPlaying = false;
    protected bool locked = false;
    [SerializeField] protected Vector3 BossPos { get => _bossRef?.Instance.transform.position ?? Vector3.zero; set => _bossRef.Instance.transform.position = value; }
    [SerializeField] protected Vector3 PlayerPos { get => _playerRef?.Instance.transform.position ?? Vector3.zero; set => _playerRef.Instance.transform.position = value; }

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
        if (_dontNeedEnd) {
            StartCoroutine(IExecute());
            yield break;
        } else {
            yield return StartCoroutine(IExecute());
            End();
        }
    }

    protected abstract IEnumerator IExecute();

    protected virtual void End() {
        isPlaying = false;
        OnEnd?.Invoke(this);
        gameObject.SetActive(false);
    }
}
