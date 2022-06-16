using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

[FeedbackHelp("This feedback lets you")]
[FeedbackPath("Custom/Template")]
public class MMFeedbackTemplate : MMFeedback {
    #if UNITY_EDITOR
    public override Color FeedbackColor { get { return new Color(1f, 0.9f, 0.9f); } }
#endif

    [Space]
    public float _nothing;

    protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f) {

    }
}
