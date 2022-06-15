using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

[FeedbackHelp("This feedback lets you play other feedbacks")]
[FeedbackPath("Custom/Feedback Player")]
public class MMFeedbackPlayFeedback : MMFeedback {
#if UNITY_EDITOR
    public override Color FeedbackColor { get { return new Color(1f, 0.9f, 0.9f); } }
#endif

    [Space]
    public MMFeedbacks _feedback;

    public override float FeedbackDuration { get => _feedback?.TotalDuration ?? 0f; }

    protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f) {
        _feedback.PlayFeedbacks();
    }
}