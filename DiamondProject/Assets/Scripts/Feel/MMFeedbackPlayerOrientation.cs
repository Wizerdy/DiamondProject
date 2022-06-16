using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

[FeedbackHelp("This feedback lets you orientate the sprite of the player")]
[FeedbackPath("Custom/Player Orientation")]
public class MMFeedbackPlayerOrientation : MMFeedback {
#if UNITY_EDITOR
    
    public override Color FeedbackColor { get { return new Color(1f, 0.9f, 0.9f); } }
#endif

    [Space]

    public PlayerController _playerController;
    public Vector2 _direction = Vector2.up;

    protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f) {
        if (_direction == Vector2.zero) { return; }
        _playerController?.LookAt(_direction);
    }
}
