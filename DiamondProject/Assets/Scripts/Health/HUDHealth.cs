using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDHealth : MonoBehaviour {
    [SerializeField] Slider _healthBar;
    [SerializeField] Image _damageScreen;
    [SerializeField] Reference<Health> _health;

    Coroutine _routine_TakeDamageHUD;

    private void Reset() {
        _healthBar = GetComponent<Slider>();
        _damageScreen = GetComponent<Image>();
    }

    private void Start() {
        if (_health != null) {
            _health.Instance.OnHit += TakeDamageHUD;
        }
    }

    private void TakeDamageHUD(int damage) {
        HealthBar();
        RedScreen();

        void HealthBar() {
            if (_healthBar == null) { return; }

            _healthBar.value = (float)_health.Instance.CurrentHealth / (float)_health.Instance.MaxHealth;
        }

        void RedScreen() {
            if (_damageScreen == null) { return; }
            if (_routine_TakeDamageHUD != null) { StopCoroutine(_routine_TakeDamageHUD); }
            _routine_TakeDamageHUD = StartCoroutine(RedScreen(0.1f));

            IEnumerator RedScreen(float time) {
                _damageScreen.gameObject.SetActive(true);
                yield return new WaitForSeconds(time);
                _damageScreen.gameObject.SetActive(false);
            }
        }
    }
}
