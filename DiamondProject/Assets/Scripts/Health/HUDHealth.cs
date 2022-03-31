using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ToolsBoxEngine;
using TMPro;

public class HUDHealth : MonoBehaviour {
    [SerializeField] Slider _healthBar;
    [SerializeField] Image _damageScreen;
    [SerializeField] Reference<Health> _health;
    [SerializeField] TextMeshProUGUI _lifeText;

    Coroutine _routine_TakeDamageHUD;

    private void Reset() {
        _healthBar = GetComponent<Slider>();
        _damageScreen = GetComponent<Image>();
    }

    private void Awake() {
        if (_health != null) {
            _health.Instance.OnHit += TakeDamageHUD;
            _health.Instance.OnHit += UpdateHUD;
            _health.Instance.OnHeal += UpdateHUD;
            _health.Instance.OnMaxHealthChange += ModifyMaxHealth;
            _health.Instance.OnLateStart += OnStart;
        }
    }

    private void OnStart() {
        UpdateHUD(0);
    }

    private void OnDestroy() {
        if (_health != null) {
            _health.Instance.OnHit -= TakeDamageHUD;
            _health.Instance.OnHit -= UpdateHUD;
            _health.Instance.OnHeal -= UpdateHUD;
            _health.Instance.OnMaxHealthChange -= ModifyMaxHealth;
            _health.Instance.OnLateStart -= OnStart;
        }
    }

    private void UpdateHUD(int delta) {
        if (_healthBar == null) { return; }
        _healthBar.value = (float)_health.Instance.CurrentHealth / (float)_health.Instance.MaxHealth;
        if (_lifeText != null) { _lifeText.text = _health.Instance.CurrentHealth + " / " + _health.Instance.MaxHealth; }
    }

    private void TakeDamageHUD(int damage) {
        RedScreen();

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

    private void ModifyMaxHealth(int delta) {
        if (_health == null || _healthBar == null) { return; }
        //float localScale = Tools.InverseLerpUnclamped(_healthBar.transform.localScale.x, _health.Instance.MaxHealth - delta, _health.Instance.MaxHealth);
        RectTransform healthRect = _healthBar.GetComponent<RectTransform>();
        float localScale = Tools.InverseLerpUnclamped(0f, _health.Instance.MaxHealth - delta, _health.Instance.MaxHealth);
        localScale = Mathf.LerpUnclamped(0f, healthRect.rect.width, localScale);
        Tools.Print(_healthBar.transform.localScale.x, _health.Instance.MaxHealth - delta, _health.Instance.MaxHealth, localScale, delta);
        //_healthBar.transform.localScale = _healthBar.transform.localScale.Override(localScale, Axis.X);
        healthRect.sizeDelta = new Vector2(localScale, healthRect.rect.height);
        UpdateHUD(0);
    }
}
