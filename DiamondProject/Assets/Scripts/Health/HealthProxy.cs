using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthProxy : MonoBehaviour, IHealth {
    [SerializeField] Health health;

    public bool CanTakeDamage { get => health.CanTakeDamage; set => health.CanTakeDamage = value; }
    public int CurrentHealth { get => health.CurrentHealth; set => health.CurrentHealth = value; }

    public event UnityAction<int> OnHit { add => health.OnHit += value; remove => health.OnHit -= value; }
    public event UnityAction<int> OnHeal { add => health.OnHeal += value; remove => health.OnHeal -= value; }
    public event UnityAction OnDeath { add => health.OnDeath += value; remove => health.OnDeath -= value; }

    public void Die() {
        health.Die();
    }

    public void TakeDamage(int damage, string damageType = "") {
        health.TakeDamage(damage, damageType);
    }

    public void TakeHeal(int damage) {
        health.TakeHeal(damage);
    }
}
