using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempHealth : MonoBehaviour {
    [SerializeField] Image _damageImage = null;
    [SerializeField] Slider _healthBar = null;
    [SerializeField] int maxHealth = 100;
    int currentHealth;
    public bool CanTakeDamage = true;

    public ToolsBoxEngine.Tools.BasicDelegate<int> OnHit;

    Coroutine routine_TakeDamageHUD;

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
        OnHit?.Invoke(life);
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
        //visualEffect.AddColor(hitVisualEffectColor, hitVisualEffectWeight, hitVisualEffectTime);
    }

    void Heal() {
        //visualEffect.AddColor(healVisualEffectColor, hitVisualEffectWeight, hitVisualEffectTime);
    }

    private void TakeDamageHUD() {
        HealthBar();
        RedScreen();

        void HealthBar() {
            if (_healthBar == null) { return; }

            _healthBar.value = (float)currentHealth / (float)maxHealth;
        }

        void RedScreen() {
            if (_damageImage == null) { return; }
            if (routine_TakeDamageHUD != null) { StopCoroutine(routine_TakeDamageHUD); }
            routine_TakeDamageHUD = StartCoroutine(RedScreen(0.1f));

            IEnumerator RedScreen(float time) {
                _damageImage.gameObject.SetActive(true);
                yield return new WaitForSeconds(time);
                _damageImage.gameObject.SetActive(false);
            }
        }
    }
}
