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
    [SerializeField] Color _invicibleColor = Color.white;
    [SerializeField] Color _disableColor = Color.grey;
    [SerializeField] bool _fillOnEnable = true;
    [SerializeField] float _fillTime = 3f;

    [HideInInspector, SerializeField] UnityEvent<float> _onHealthChange;

    Coroutine _routine_UpdateHealthBar;
    Coroutine _routine_RedScreen;

    Color _startColor;

    bool _isActive = true;
    HealthData _save;
    bool _isLinked = false;

    #region Properties

    public bool Active { get => _isActive; set => Enable(value); }
    public float SliderValue => _healthBar?.value ?? 0f;
    public Health HealthReference => _health?.Instance ?? null;
    public bool IsLinked => _isLinked;

    public event UnityAction<float> OnHealthChange { add => _onHealthChange.AddListener(value); remove => _onHealthChange.RemoveListener(value); }

    #endregion

    #region Unity Callbacks

    private void Reset() {
        _healthBar = GetComponent<Slider>();
        _damageScreen = GetComponent<Image>();
    }

    private void Awake() {
        _isActive = true;
        _startColor = _fill.color;
        Attach();
    }

    private void OnEnable() {
        if (_fillOnEnable) {
            StartCoroutine(ChangeHealthOverTime(_healthBar, 0f, 0f));
            float percentage = (float)_health.Instance.CurrentHealth / (float)_health.Instance.MaxHealth;
            StartCoroutine(ChangeHealthOverTime(_healthBar, percentage, _fillTime));
        }
    }

    private void OnStart() {
        //UpdateHUD(0);
    }

    private void OnDestroy() {
        Unattach();
    }

    #endregion

    public void Attach(Health health = null) {
        if (_isLinked) { return; }
        if (health == null) {
            if (!_health.IsValid()) { return; }
            health = _health.Instance;
        }
        _health.Instance.OnHit += TakeDamageHUD;
        _health.Instance.OnHit += UpdateHUD;
        _health.Instance.OnHeal += UpdateHUD;
        _health.Instance.OnMaxHealthChange += ModifyMaxHealth;
        _health.Instance.OnLateStart += OnStart;
        _health.Instance.OnInvicible += _Invincible;
        _health.Instance.OnVulnerable += _Vulnerable;
        _isLinked = true;
    }

    public void Unattach(Health health = null) {
        if (!_isLinked) { return; }
        if (health == null) {
            if (!_health.IsValid()) { return; }
            health = _health.Instance;
        }
        _health.Instance.OnHit -= TakeDamageHUD;
        _health.Instance.OnHit -= UpdateHUD;
        _health.Instance.OnHeal -= UpdateHUD;
        _health.Instance.OnMaxHealthChange -= ModifyMaxHealth;
        _health.Instance.OnLateStart -= OnStart;
        _health.Instance.OnInvicible -= _Invincible;
        _health.Instance.OnVulnerable -= _Vulnerable;
        _isLinked = false;
    }

    private void Enable(bool state = true) {
        if (_isActive == state) { return; }
        _isActive = state;

        if (!_isActive) {
            ChangeFillColor(_disableColor);
            _lifeText?.gameObject.SetActive(false);
        } else {
            if (!_health.IsValid()) { return; }
            _lifeText?.gameObject.SetActive(true);
            if (_health.Instance.CanTakeDamage) {
                ChangeFillColor(_startColor);
            } else {
                ChangeFillColor(_invicibleColor);
            }
        }
    }

    public void UpdateHUD(int delta) {
        if (!_isActive) { return; }
        if (_healthBar == null) { return; }

        if (_routine_UpdateHealthBar != null) { StopCoroutine(_routine_UpdateHealthBar); }
        float percentage = (float)_health.Instance.CurrentHealth / (float)_health.Instance.MaxHealth;
        _routine_UpdateHealthBar = StartCoroutine(ChangeHealthOverTime(_healthBar, percentage, 0.1f));
        if (_lifeText != null) { _lifeText.text = _health.Instance.CurrentHealth + " / " + _health.Instance.MaxHealth; }
        _onHealthChange?.Invoke(percentage);
    }

    private void _Invincible() {
        if (!_isActive) { return; }
        ChangeFillColor(_invicibleColor);
    }

    private void _Vulnerable() {
        if (!_isActive) { return; }
        ChangeFillColor(_startColor);
    }

    private void ChangeFillColor(Color color) {
        if (_fill == null) { return; }
        _fill.color = color;
    }

    private void ModifyMaxHealth(int delta) {
        if (!_isActive) { return; }
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
        if (!_isActive) { return; }
        if (damage <= 0) { return; }
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
        if (time <= 0f) { slider.value = target; yield break; }
        if (slider.value == target) { yield break; }
        float timePassed = 0f;
        float startPercentage = slider.value;
        while (timePassed < time) {
            yield return null;
            timePassed += Time.deltaTime;
            slider.value = Mathf.Lerp(startPercentage, target, timePassed / time);
        }
    }
}
