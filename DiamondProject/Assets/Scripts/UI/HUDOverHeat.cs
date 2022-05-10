using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HUDOverHeat : MonoBehaviour {
    [SerializeField] Reference<EntityOverHeat> _overheat;
    [SerializeField] Slider _slider;
    [SerializeField] Image _fill;
    [SerializeField] Gradient _gradient;

    [HideInInspector, SerializeField] UnityEvent<float> _onValueChange;

    Coroutine _routine_UpdateSlider;

    void Start() {
        if (_overheat?.Instance != null) { _overheat.Instance.OnHeating += UpdateHUD; }
        else { Debug.LogError("Overheat object not defined"); }
    }

    private void UpdateHUD(int delta) {
        if (_slider == null) { return; }
        if (_routine_UpdateSlider != null) { StopCoroutine(_routine_UpdateSlider); }
        float percentage = _overheat.Instance.Percentage;
        _routine_UpdateSlider = StartCoroutine(ChangeHealthOverTime(_slider, percentage, 0.1f));
        _onValueChange?.Invoke(percentage);
    }

    private void UpdateFillColor(bool overheat) {
        if (_fill != null) {
            if (overheat) {
                _fill.color = _gradient.Evaluate(1f);
            } else {
                _fill.color = _gradient.Evaluate(_slider.value);
            }
        }
    }

    IEnumerator ChangeHealthOverTime(Slider slider, float target, float time) {
        if (time <= 0f) { slider.value = target; }
        float timePassed = 0f;
        float startPercentage = slider.value;
        bool overheat = _overheat.Instance.Overheating;
        while (timePassed < time) {
            yield return new WaitForEndOfFrame();
            timePassed += Time.deltaTime;
            float percentage = timePassed / time;
            slider.value = Mathf.Lerp(startPercentage, target, percentage);
            UpdateFillColor(overheat);
        }
    }
}
