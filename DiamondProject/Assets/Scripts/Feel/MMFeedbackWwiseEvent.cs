using MoreMountains.Feedbacks;
using UnityEngine;

[FeedbackHelp("This feedback lets you trigger a wwise event")]
[FeedbackPath("Wwise/Wwise Event")]
public class MMFeedbackWwiseEvent : MMFeedback
{
    // sets the inspector color for this feedback
    #if UNITY_EDITOR
        public override Color FeedbackColor { get { return new Color(0.5f, 0f, 0.5f); } }
    #endif

    public AK.Wwise.Event wwiseEvent;
    public GameObject myGameobject;

    protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
    {
        if (null != wwiseEvent)
        {
            wwiseEvent.Post(myGameobject);
        }
    }
}
