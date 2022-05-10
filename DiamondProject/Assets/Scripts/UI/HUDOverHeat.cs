using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HUDOverHeat : MonoBehaviour {
    [SerializeField] Reference<EntityOverHeat> _overheat;
    [SerializeField] Slider _slider;

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

    IEnumerator ChangeHealthOverTime(Slider slider, float target, float time) {
        if (time <= 0f) { slider.value = target; }
        float timePassed = 0f;
        float startPercentage = slider.value;
        while (timePassed < time) {
            yield return new WaitForEndOfFrame();
            timePassed += Time.deltaTime;
            slider.value = Mathf.Lerp(startPercentage, target, timePassed / time);
        }
    }
}
