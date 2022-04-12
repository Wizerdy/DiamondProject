using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarDisplay : MonoBehaviour {
    [SerializeField] PosterityObject _posterity;
    [SerializeField] GameObject _playerHealthBar;
    [SerializeField] GameObject _bossHealthBar;

    void Start() {

    }

    void Update() {
        if (_posterity == null) { return; }
        _playerHealthBar?.SetActive(!_posterity.dontSeeHealthBar);
        _bossHealthBar?.SetActive(!_posterity.dontSeeHealthBar && _posterity.seeBossHealthBar);
    }
}
