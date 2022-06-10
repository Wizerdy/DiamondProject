using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class TransformScaler : MonoBehaviour {
    [SerializeField] Vector3 _scale = Vector3.one;
    [SerializeField] float _time = 2f;
    [SerializeField] Axis[] _axis;
    [SerializeField] AnimationCurve _curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    public void SetTime(float time) {
        _time = time;
    }

    public void ScaleMe() {
        ScaleMe(_scale, _time, _curve, _axis);
    }

    public void ScaleMe(Vector3 scale, float time, AnimationCurve curve, params Axis[] axis) {
        StartCoroutine(IScaleMe(scale, time, curve, axis));
    }

    private IEnumerator IScaleMe(Vector3 scale, float time, AnimationCurve curve, params Axis[] axis) {
        if (time <= 0f) { transform.localScale = transform.localScale.Override(scale, axis); }
        float timePassed = 0f;
        Vector3 startScale = transform.localScale;
        while (timePassed < time) {
            yield return new WaitForEndOfFrame();
            timePassed += UnityEngine.Time.deltaTime;
            float percentage = timePassed / time;
            Vector3 next = Vector3.LerpUnclamped(startScale, scale, curve.Evaluate(percentage));
            transform.localScale = transform.localScale.Override(next, axis);
        }
    }
}
