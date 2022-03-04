using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    public bool CanTakeDamage;
    [SerializeField] Slider _healthBar;
    [SerializeField] VisualEffect visualEffect;
    [SerializeField] int maxHealth = 500;
    [SerializeField] float hitVisualEffectTime;
    [SerializeField] int hitVisualEffectWeight;
    [SerializeField] Color hitVisualEffectColor;
    [SerializeField] Color healVisualEffectColor;
    int currentHealth;

    [SerializeField] public int CurrentHealth { get { return currentHealth; } }

    private void Start() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int life) {
        if (!CanTakeDamage) { return; }
        this.currentHealth -= life;
        Hit();
        if (this.currentHealth <= 0) {
            Die();
        }
    }

    public void TakeHeal(int life) {
        this.currentHealth += life;
        Heal();
        if (this.currentHealth <= 0) {
            Die();
        }
    }

    public void Die() {
        Destroy(gameObject);
    }

    void Hit() {
        TakeDamageHUD();
        visualEffect.AddColor(hitVisualEffectColor, hitVisualEffectWeight, hitVisualEffectTime);
    }

    void Heal() {
        visualEffect.AddColor(healVisualEffectColor, hitVisualEffectWeight, hitVisualEffectTime);
    }

    private void TakeDamageHUD() {
        HealthBar();
        //RedScreen();

        void HealthBar() {
            if (_healthBar == null) { return; }

            _healthBar.value = (float)currentHealth / (float)maxHealth;
        }
    }
}
