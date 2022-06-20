using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
//using ToolsBoxEngine;

[FeedbackHelp("This feedback lets you change Time Scale")]
[FeedbackPath("Custom/TimeScale")]
public class MMFeedbackTimeScale : MMFeedback {
#if UNITY_EDITOR
    public override Color FeedbackColor { get { return new Color(1f, 0.9f, 0.9f); } }
#endif

    [Space]
    public float _timeBeforeReset = 2f;
    public float _targetScale = 0.5f;
    public float _lerpTime = 1f;
    public AnimationCurve _curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    Coroutine _routine_LerpTimeScale = null;

    protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f) {
        float currentTimescale = Time.timeScale;
        _routine_LerpTimeScale = StartCoroutine(LerpTimeScale(_targetScale, _lerpTime, _curve));
        StartCoroutine(Delay(
            () => { if (_routine_LerpTimeScale != null) { StopCoroutine(_routine_LerpTimeScale); } StartCoroutine(LerpTimeScale(currentTimescale, _lerpTime, _curve)); },
            _timeBeforeReset)
        );
    }

    private IEnumerator LerpTimeScale(float target, float time, AnimationCurve curve) {
        if (time <= 0f) { Time.timeScale = target; yield break; }
        float timePassed = 0f;
        float startTimeScale = Time.timeScale;
        while (timePassed < time) {
            yield return null;
            timePassed += Time.unscaledDeltaTime;
            float next = Mathf.Lerp(startTimeScale, target, curve.Evaluate(timePassed / time));
            Time.timeScale = next;
        }
    }

    private IEnumerator Delay(Action function, float time) {
        if (time <= 0f) { function(); yield break; }
        yield return new WaitForSeconds(time);
        function();
    }
}
