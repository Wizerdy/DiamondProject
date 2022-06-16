using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ToolsBoxEngine;

public class ShadowHUDHealth : MonoBehaviour {
    [SerializeField] HUDHealth _hudHealth;
    [SerializeField] Slider _slider;

    [Header("Parameters")]
    [SerializeField] float _timeBeforeFade;
    [SerializeField] float _fadeTime;

    float _timer;

    Coroutine _routine_ChangeHealthOverTime = null;

    void Start() {
        _hudHealth.OnHealthChange += _ShadowHealthBar;
    }

    private void OnEnable() {
        SetShadowHealthBar(0f);
    }

    private void OnDestroy() {
        _hudHealth.OnHealthChange -= _ShadowHealthBar;
    }

    void Update() {
        UpdateDamageHealthBar();
    }

    private void UpdateDamageHealthBar() {
        if (_slider == null) { return; }
        if (_timer > 0f) {
            _timer -= Time.deltaTime;
        } else if (_slider.value != _hudHealth.SliderValue) {
            StartCoroutine(ChangeHealthOverTime(_slider, _hudHealth.SliderValue, _fadeTime));
        }
    }

    void _ShadowHealthBar(float percentage) {
        if (percentage >= _slider.value) { SetShadowHealthBar(percentage); return; }
        if (_routine_ChangeHealthOverTime != null) { StopCoroutine(_routine_ChangeHealthOverTime); }
        _timer = _timeBeforeFade;
    }

    public void SetShadowHealthBar(float value) {
        if (_routine_ChangeHealthOverTime != null) { StopCoroutine(_routine_ChangeHealthOverTime); }
        _slider.value = value;
    }

    IEnumerator ChangeHealthOverTime(Slider slider, float target, float time) {
        if (time <= 0f) { slider.value = target; yield break; }
        float timePassed = 0f;
        float startPercentage = slider.value;
        while (timePassed < time) {
            yield return new WaitForEndOfFrame();
            timePassed += Time.deltaTime;
            slider.value = Mathf.Lerp(startPercentage, target, timePassed / time);
        }
    }
}
