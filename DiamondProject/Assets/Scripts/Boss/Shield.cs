using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
    [SerializeField] public HealthReference health;
    [SerializeField] bool isShielding;

    protected void Protect() {
        isShielding = true;
        health.Instance.CanTakeDamage = false;
    }

    protected void StopProtect() {
        isShielding = false;
        health.Instance.CanTakeDamage = true;
    }
}
