using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

using InputType = PlayerController.InputType;

[FeedbackHelp("This feedback lets you enable or disable player inputs")]
[FeedbackPath("Custom/Input Enabler")]
public class MMFeedbackInputEnabler : MMFeedback {
    public enum InputState {
        INHERIT, DISABLE, ENABLE
    }

#if UNITY_EDITOR
    public override Color FeedbackColor { get { return new Color(1f, 0.9f, 0.9f); } }
#endif

    [Space]
    public PlayerController _playerController;
    public InputState _movement;
    public InputState _combat;
    public InputState _ui;

    protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f) {
        ChangeInput(InputType.MOVEMENT, _movement);
        ChangeInput(InputType.COMBAT, _combat);
        ChangeInput(InputType.UI, _ui);
    }

    private void ChangeInput(InputType type, InputState state) {
        if (state == InputState.INHERIT) { return; }
        _playerController?.EnableInput(type, state == InputState.ENABLE);
    }
}
