using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ToolsBoxEngine;

public abstract class BaseAttack : MonoBehaviour {
    [Header("Static")]
    [SerializeField] Reference<AttackSystem> attackSystem;
    [SerializeField] protected BossReference _bossRef;
    [SerializeField] public string id = "";
    [SerializeField] protected TransformReference _playerPos;

    [Header("Values")]
    [SerializeField] protected float duration = 1;
    [SerializeField] protected float coolDown = 1;
    [SerializeField] protected bool _dontNeedEnd = false;
    [SerializeField] protected float _castTime = 0f;
    protected bool isPlaying = false;
    protected bool locked = false;
    [SerializeField] protected Vector3 BossPos { get => _bossRef?.Instance.transform.position ?? Vector3.zero; set => _bossRef.Instance.transform.position = value; }
    [SerializeField] protected Vector3 PlayerPos { get => _playerPos.Instance.position; set => _playerPos.Instance.position = value; }

    [SerializeField] UnityEvent<BaseAttack> _onExecute;
    [SerializeField] UnityEvent<BaseAttack> _onCast;
    [SerializeField] UnityEvent<BaseAttack> _onEnd;

    public event UnityAction<BaseAttack> OnExecute { add => _onExecute.AddListener(value); remove => _onExecute.RemoveListener(value); }
    public event UnityAction<BaseAttack> OnCast { add => _onCast.AddListener(value); remove => _onCast.RemoveListener(value); }
    public event UnityAction<BaseAttack> OnEnd { add => _onEnd.AddListener(value); remove => _onEnd.RemoveListener(value); }

    private void Start() {
        attackSystem?.Instance?.Register(this);
        Execute();
    }

    private void OnDisable() {
        attackSystem?.Instance?.Unregister(this);
        Destroy(gameObject);
    }

    public void Execute() {
        isPlaying = true;

        _onExecute?.Invoke(this);

        StartCoroutine(ICast());
        StartCoroutine(Tools.Delay(() => { StartCoroutine(ILaunch()); _onCast?.Invoke(this); }, _castTime));
        //StartCoroutine(ILaunch());
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
    protected virtual IEnumerator ICast() { yield break; }

    public virtual void End() {
        StopAllCoroutines();
        isPlaying = false;
        _onEnd?.Invoke(this);
        gameObject.SetActive(false);
    }
}
