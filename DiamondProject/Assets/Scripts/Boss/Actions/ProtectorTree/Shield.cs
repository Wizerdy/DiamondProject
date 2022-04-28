using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
    [SerializeField] Health _health = null;
    [SerializeField] bool isShielding;

    protected void Protect() {
        isShielding = true;
        _health.CanTakeDamage = false;
    }

    protected void StopProtect() {
        isShielding = false;
        _health.CanTakeDamage = true;
    }

    public void AttachToHealth(Health health) {
        _health = health;
    }
}
