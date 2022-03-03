using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
    [SerializeField] HealthReference health;
    [SerializeField] VisualEffectReference visualEffect;
    [SerializeField] bool isShielding;

    protected void Protect() {
        isShielding = true;
        health.Instance.CanTakeDamage = false;
        visualEffect.Instance.AddColor(Color.blue, 10, 20);
    }

    protected void StopProtect() {
        isShielding = false;
        health.Instance.CanTakeDamage = true;
        visualEffect.Instance.RemoveColor(20);
    }
}
