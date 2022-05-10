using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ToolsBoxEngine;

public class EntityOverHeat : MonoBehaviour {
    [SerializeField] int _maxHeat = 100;
    [SerializeField] float _overHeatTime = 5f;
    [SerializeField] AnimationCurve _heatCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    [SerializeField] float _heatDropTime = 1f;

    [HideInInspector, SerializeField] UnityEvent<int> _onHeating;
    [HideInInspector, SerializeField] UnityEvent _onOverheat;
    [HideInInspector, SerializeField] UnityEvent _onUnoverheat;

    int _currentHeat = 0;
    bool _overheat = false;

    Coroutine _routine_WaitLoseHeat = null;
    Coroutine _routine_LoseHeat = null;

    #region Properties

    public int Heat { get => _currentHeat; set => SetHeat(value, true); }
    public int MaxHeat => _maxHeat;
    public float Percentage => (float)_currentHeat / (float)_maxHeat;
    public bool Overheating => _overheat;

    public event UnityAction<int> OnHeating { add => _onHeating.AddListener(value); remove => _onHeating.RemoveListener(value); }
    public event UnityAction OnOverheat { add => _onOverheat.AddListener(value); remove => _onOverheat.RemoveListener(value); }
    public event UnityAction OnUnoverheat { add => _onUnoverheat.AddListener(value); remove => _onUnoverheat.RemoveListener(value); }

    #endregion

    public bool HasLeft(int amount) {
        return _currentHeat + amount < _maxHeat;
    }

    public void SetHeat(float percentage) {
        SetHeat(_maxHeat * percentage);
    }

    public void SetHeat(int amount, bool restartTimer = true) {
        int delta = _currentHeat;
        _currentHeat = Mathf.Clamp(amount, 0, _maxHeat);
        delta = _currentHeat - delta;

        _onHeating?.Invoke(delta);

        if (_currentHeat >= _maxHeat) {
            OverHeat();
        } else if (restartTimer) {
            if (_routine_WaitLoseHeat != null) { StopCoroutine(_routine_WaitLoseHeat); }
            _routine_WaitLoseHeat = StartCoroutine(Tools.Delay(LoseHeat, _heatDropTime));
        }
    }

    public void Unoverheat() {
        _overheat = false;
        SetHeat(0);
        _onUnoverheat?.Invoke();
    }

    private void OverHeat() {
        _overheat = true;
        _onOverheat?.Invoke();
    }

    private void LoseHeat() {
        if (_routine_LoseHeat != null) { StopCoroutine(_routine_LoseHeat); }
        _routine_LoseHeat = StartCoroutine(ILoseHeat());

        IEnumerator ILoseHeat() {
            if (_currentHeat <= 0) { yield break; }
            while (_currentHeat > 0) {
                yield return new WaitForEndOfFrame();
                float percentage = _currentHeat / _maxHeat;
                float delta = Time.deltaTime / _overHeatTime;
                percentage -= delta;
                int heat = Mathf.RoundToInt(_heatCurve.Evaluate(percentage) * _maxHeat);
                SetHeat(heat, false);
            }

            if (_overheat) { Unoverheat(); }
        }
    }
}
