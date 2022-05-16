using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class TimeTrigger : Trigger {
    [Space]
    [SerializeField] BossShapeSystem _shapeSystem;
    [Header("System")]
    [SerializeField] float _time;
    [SerializeField] BasicComparison _condition;
    [SerializeField] bool _resetOnNewShape = true;

    bool _timePassed = false;

    Coroutine _routine_StartCountdown;

    private void Start() {
        ResetTrigger();
        if (_resetOnNewShape && _shapeSystem != null) {
            _shapeSystem.OnEnterShape += _ResetTrigger;
        }
    }

    private void OnDestroy() {
        if (_resetOnNewShape && _shapeSystem != null) {
            _shapeSystem.OnEnterShape -= _ResetTrigger;
        }
    }

    public override bool IsSelfTrigger() {
        switch (_condition) {
            case BasicComparison.LESS:
                return !_timePassed;
            case BasicComparison.GREATER:
                return _timePassed;
        }
        return false;
    }

    void _ResetTrigger(BossShape shape) {
        ResetTrigger();
    }

    public void ResetTrigger() {
        _timePassed = false;
        StartCountdown();
    }

    void StartCountdown() {
        if (_routine_StartCountdown != null) { StopCoroutine(_routine_StartCountdown); }
        _routine_StartCountdown = StartCoroutine(IStartCountdown());

        IEnumerator IStartCountdown() {
            yield return new WaitForSeconds(_time);
            _timePassed = true;
        }
    }
}
