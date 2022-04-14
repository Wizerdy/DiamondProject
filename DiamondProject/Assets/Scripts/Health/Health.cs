using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IHealth {
    [SerializeField] int _maxHealth = 50;
    [Space]
    [SerializeField] UnityEvent<int> _onHit;
    [SerializeField] UnityEvent<int> _onHeal;
    [SerializeField] UnityEvent _onDeath;
    [SerializeField] bool _destroyOnDeath = true;
    [SerializeField] List<string> _resistances = new List<string>();

    [SerializeField, HideInInspector] UnityEvent<int> _onMaxHealthChange;
    [SerializeField, HideInInspector] UnityEvent _onLateStart;

    int _invicibilityToken = 0;
    int _currentHealth;

    #region Properties

    public int MaxHealth { get => _maxHealth; set => SetMaxHealth(value); }
    public int CurrentHealth { get { return _currentHealth; } set { ChangeHealth(value - _currentHealth); } }
    public float Percentage { get { return MaxHealth == 0 ? _currentHealth / MaxHealth : 1f; } }
    public bool CanTakeDamage {
        get { return _invicibilityToken <= 0; }
        set { _invicibilityToken += (value ? -1 : 1); _invicibilityToken = Mathf.Max(0, _invicibilityToken); }
    }

    public event UnityAction<int> OnHit { add => _onHit.AddListener(value); remove => _onHit.RemoveListener(value); }
    public event UnityAction<int> OnHeal { add => _onHeal.AddListener(value); remove => _onHeal.RemoveListener(value); }
    public event UnityAction OnDeath { add => _onDeath.AddListener(value); remove => _onDeath.RemoveListener(value); }
    public event UnityAction<int> OnMaxHealthChange { add => _onMaxHealthChange.AddListener(value); remove => _onMaxHealthChange.RemoveListener(value); }
    public event UnityAction OnLateStart { add => _onLateStart.AddListener(value); remove => _onLateStart.RemoveListener(value); }

    #endregion

    private void Start() {
        _currentHealth = _maxHealth;
        _onLateStart?.Invoke();
    }

    private void ChangeHealth(int amount) {
        if (amount == 0) { return; }
        if (amount < 0) {
            TakeDamage(amount);
        } else {
            TakeHeal(amount);
        }
    }

    public void TakeDamage(int amount, string damageTypes = "") {
        if (!CanTakeDamage) { return; }
        if (damageTypes == "" || !_resistances.Contains(damageTypes)) {
            _currentHealth -= amount;
            _currentHealth = Mathf.Max(0, _currentHealth);
            _onHit?.Invoke(amount);

            if (_currentHealth <= 0) {
                Die();
            }
        }
    }

    public void TakeHeal(int amount) {
        _currentHealth += amount;
        _currentHealth = Mathf.Min(_maxHealth, _currentHealth);
        _onHeal?.Invoke(amount);
    }

    public void Die() {
        _onDeath?.Invoke();
        if (_destroyOnDeath) {
            Destroy(gameObject);
        }
    }

    public void AddResistance(string newResistances) {
        if (!_resistances.Contains(newResistances)) {
            _resistances.Add(newResistances);
        }
    }

    public void RemoveResistance(string newResistances) {
        if (_resistances.Contains(newResistances)) {
            _resistances.Remove(newResistances);
        }
    }

    void SetMaxHealth(int amount) {
        if (amount == _maxHealth) { return; }
        int delta = amount - _maxHealth;
        if (_currentHealth == _maxHealth || _currentHealth > amount) { _currentHealth = amount; }
        _maxHealth = amount;
        _onMaxHealthChange?.Invoke(delta);
    }
}
