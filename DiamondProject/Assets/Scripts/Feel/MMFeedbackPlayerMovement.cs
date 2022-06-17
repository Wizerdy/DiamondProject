using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

[FeedbackHelp("This feedback lets you move the player to a position")]
[FeedbackPath("Custom/Player Movements")]
public class MMFeedbackPlayerMovement : MMFeedback {
#if UNITY_EDITOR
    public override Color FeedbackColor { get { return new Color(1f, 0.9f, 0.9f); } }
#endif

    public PlayerController _player;
    public Vector3 _position;
    public float _speedFactor = 1f;

    protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f) {
        _player?.MoveTo(_position, _speedFactor);
    }
}
