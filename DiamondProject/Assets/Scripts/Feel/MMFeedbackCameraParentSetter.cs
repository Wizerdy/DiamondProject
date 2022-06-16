using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

[FeedbackHelp("This feedback lets you lerp between camera parent")]
[FeedbackPath("Custom/Adopt Camera")]
public class MMFeedbackCameraParentSetter : MMFeedback {
    #if UNITY_EDITOR
        public override Color FeedbackColor { get { return new Color(1f, 0.9f, 0.9f); } }
    #endif

    [Space]

    public SimpleCameraEngine _target;
    public Camera _parent;
    public float _duration = 1f;
    public AnimationCurve _transitionCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    public override float FeedbackDuration { get { return ApplyTimeMultiplier(_duration); } set { _duration = value; } }

    protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f) {
        if (_parent == null || _target == null) { return; }

        _target.Adopt(_parent, FeedbackDuration, _transitionCurve);
    }
}
