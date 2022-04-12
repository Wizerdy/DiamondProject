using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class DeathManager : MonoBehaviour {
    [SerializeField] int _extraLife = 0;
    [SerializeField] string _nextLevel = "ProtoFairy 1";
    [SerializeField] LevelLoader _levelLoader = null;
    [SerializeField] Reference<PlayerController> _player = null;

    public int ExtraLife { get => _extraLife; set => SetExtraLife(value); }

    public void PlayerDeath() {
        if (_player == null) { return; }

        PlayerDeath(_player.Instance);
    }

    public void PlayerDeath(PlayerController player) {
        if (_extraLife > 0) {
            --_extraLife;
            Revive(player);
            return;
        }

        if (_levelLoader != null) {
            StartCoroutine(Tools.Delay(_levelLoader.LoadLevel, _nextLevel, 1.5f));
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
