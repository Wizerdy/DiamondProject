using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBelowTrigger : Trigger {
    [Header("Health")]
    [SerializeField] Reference<Health> _health;
    [SerializeField, Range(0f, 1f)] float _percentage;

    public override bool IsSelfTrigger() {
        if (_health.Instance.Percentage <= _percentage) {
            return true;
        }
        return false;
    }
}
