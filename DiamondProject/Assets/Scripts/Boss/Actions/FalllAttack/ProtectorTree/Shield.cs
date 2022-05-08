using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
    [SerializeField] Health _health = null;

    public void Activate() {
        _health.CanTakeDamage = false;
    }

    public void Desactivate() {
        _health.CanTakeDamage = true;
    }

    public void AttachToHealth(Health health) {
        _health = health;
    }
}
