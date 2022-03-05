using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinalHealth : MonoBehaviour {
    [SerializeField] int _maxHealth = 50;
    [Space]
    [SerializeField] UnityEvent<int> _onHit;
    [SerializeField] UnityEvent<int> _onHeal;
    [SerializeField] UnityEvent _onDeath;

    int _invicibiltyToken = 0;
    int _currentHealth;

    #region Properties

    public bool CanTakeDamage { 
        get { return _invicibiltyToken <= 0; }
        set { _invicibiltyToken += (value ? -1 : 1); _invicibiltyToken = Mathf.Max(0, _invicibiltyToken); }
    }
    public int Health { get { return _currentHealth; } set { ChangeHealth(value - _currentHealth); } }

    public event UnityAction<int> OnHit { add { _onHit.AddListener(value); } remove { _onHit.RemoveListener(value); } }
    public event UnityAction<int> OnHeal { add { _onHeal.AddListener(value); } remove { _onHeal.RemoveListener(value); } }
    public event UnityAction OnDeath { add { _onDeath.AddListener(value); } remove { _onDeath.RemoveListener(value); } }

    #endregion

    private void Start() {
        _currentHealth = _maxHealth;
    }

    private void ChangeHealth(int amount) {
        if (amount == 0) { return; }
        if (amount < 0) {
            TakeDamage(amount);
        } else {
            TakeHeal(amount);
        }
    }

    public void TakeDamage(int amount) {
        if (!CanTakeDamage) { return; }

        _currentHealth -= amount;
        _currentHealth = Mathf.Max(0, _currentHealth);
        _onHit?.Invoke(amount);

        if (_currentHealth <= 0) {
            Die();
        }
    }

    public void TakeHeal(int amount) {
        _currentHealth += amount;
        _currentHealth = Mathf.Min(_maxHealth, _currentHealth);
        _onHeal?.Invoke(amount);
    }

    public void Die() {
        _onDeath?.Invoke();
    }
}
