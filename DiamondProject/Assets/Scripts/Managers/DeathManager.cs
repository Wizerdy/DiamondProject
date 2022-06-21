using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ToolsBoxEngine;

public class DeathManager : MonoBehaviour {
    [SerializeField] int _extraLife = 0;
    [SerializeField] string _nextLevel = "ProtoFairy 1";
    [SerializeField] float _nextLevelDelay = 5f;
    [SerializeField] LevelLoader _levelLoader = null;
    [SerializeField] Reference<PlayerController> _player = null;
    [SerializeField] UnityEvent _onDeath;
    bool _canDie = false;

    public event UnityAction OnDeath { add => _onDeath.AddListener(value); remove => _onDeath.RemoveListener(value); }

    public int ExtraLife { get => _extraLife; set => SetExtraLife(value); }

    public bool CanDie { get => _canDie; set => _canDie = value; }

    public void PlayerDeath() {
        if (_player == null) { return; }

        PlayerDeath(_player.Instance);
    }

    public void PlayerDeath(PlayerController player) {
        if (!CanDie) { return; }
        if (_extraLife > 0) {
            --_extraLife;
            Revive(player);
            return;
        }

        _onDeath?.Invoke();
        if (_levelLoader != null) {
            _levelLoader.LoadLevel(_nextLevel, false);
            StartCoroutine(Tools.UnscaledDelay(_levelLoader.ChangeToLoadedScene, _nextLevelDelay));
        }
        //_levelLoader?.LoadLevel(_nextLevel);
    }

    public void Revive(PlayerController player) {
        player.Health.CurrentHealth = player.Health.MaxHealth;
    }

    void SetExtraLife(int amount) {
        _extraLife = amount;
    }
}
