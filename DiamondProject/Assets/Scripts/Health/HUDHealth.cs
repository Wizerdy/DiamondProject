using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using ToolsBoxEngine;
using TMPro;

public class HUDHealth : MonoBehaviour {
    [SerializeField] Slider _healthBar;
    [SerializeField] Image _fill;
    [SerializeField] Image _damageScreen;
    [SerializeField] Reference<Health> _health;
    [SerializeField] TextMeshProUGUI _lifeText;
    [SerializeField] Color _invicibleColor;
    [HideInInspector, SerializeField] UnityEvent<float> _onHealthChange;

    Coroutine _routine_UpdateHealthBar;
    Coroutine _routine_RedScreen;

    Color _startColor;

    #region Properties

    public event UnityAction<float> OnHealthChange { add => _onHealthChange.AddListener(value); remove => _onHealthChange.RemoveListener(value); }
    public float SliderValue => _healthBar?.value ?? 0f;

    #endregion

    private void Reset() {
        _healthBar = GetComponent<Slider>();
        _damageScreen = GetComponent<Image>();
    }

    private void Start() {
        _startColor = _fill.color;
        if (_health != null) {
            _health.Instance.OnHit += TakeDamageHUD;
            _health.Instance.OnHit += UpdateHUD;
            _health.Instance.OnHeal += UpdateHUD;
            _health.Instance.OnMaxHealthChange += ModifyMaxHealth;
            _health.Instance.OnLateStart += OnStart;
            _health.Instance.OnInvicible += _Invincible;
            _health.Instance.OnVulnerable += _Vulnerable;
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
            _health.Instance.OnInvicible -= _Invincible;
            _health.Instance.OnVulnerable -= _Vulnerable;
        }
    }

    private void UpdateHUD(int delta) {
        if (_healthBar == null) { return; }
        //_healthBar.value = (float)_health.Instance.CurrentHealth / (float)_health.Instance.MaxHealth;
        if (_routine_UpdateHealthBar != null) { StopCoroutine(_routine_UpdateHealthBar); }
        float percentage = (float)_health.Instance.CurrentHealth / (float)_health.Instance.MaxHealth;
        _routine_UpdateHealthBar = StartCoroutine(ChangeHealthOverTime(_healthBar, percentage, 0.1f));
        if (_lifeText != null) { _lifeText.text = _health.Instance.CurrentHealth + " / " + _health.Instance.MaxHealth; }
        _onHealthChange?.Invoke(percentage);
    }

    private void _Invincible() {
        if (_fill == null) { return; }
        _fill.color = _invicibleColor;
    }

    private void _Vulnerable() {
        if (_fill == null) { return; }
        _fill.color = _startColor;
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

    private void TakeDamageHUD(int damage) {
        RedScreen();

        void RedScreen() {
            if (_damageScreen == null) { return; }
            if (_routine_RedScreen != null) { StopCoroutine(_routine_RedScreen); }
            _routine_RedScreen = StartCoroutine(RedScreen(0.1f));

            IEnumerator RedScreen(float time) {
                _damageScreen.gameObject.SetActive(true);
                yield return new WaitForSeconds(time);
                _damageScreen.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator ChangeHealthOverTime(Slider slider, float target, float time) {
        if (time <= 0f) { slider.value = target; }
        float timePassed = 0f;
        float startPercentage = slider.value;
        while (timePassed < time) {
            yield return new WaitForEndOfFrame();
            timePassed += Time.deltaTime;
            slider.value = Mathf.Lerp(startPercentage, target, timePassed / time);
        }
    }
}
