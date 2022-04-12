using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class ApplyPosterity : MonoBehaviour {
    [SerializeField] PosterityObject _posterity;

    [SerializeField] Reference<Transform> _playerTransform;

    [Header("Health")]
    [SerializeField] GameObject _playerHealthBar;
    [SerializeField] GameObject _bossHealthBar;
    [SerializeField] Reference<Health> _playerHealth;
    [SerializeField] DeathManager _deathManager;

    [Header("Nearsight")]
    [SerializeField] GameObject _nearsightPanel;
    [SerializeField] Material _nearsightMaterial;

    void Start() {
        if (_deathManager != null) { _deathManager.ExtraLife = _posterity.extraLife; }
        if (_playerHealth != null) { _playerHealth.Instance.MaxHealth += _posterity.maxLifeModifier; }
    }

    void Update() {
        if (_posterity == null) { return; }

        _nearsightPanel?.SetActive(_posterity.nearSight);
        _nearsightMaterial?.SetVector("_Center", _playerTransform.Instance.position);

        _playerHealthBar?.SetActive(!_posterity.dontSeeHealthBar);
        //_bossHealthBar?.SetActive(!_posterity.dontSeeHealthBar && _posterity.seeBossHealthBar);
    }
}
