using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_PosterityUpdate : MonoBehaviour {
    [SerializeField] Reference<Health> _playerHealth;
    [SerializeField] Reference<Health> _bossHealth;
    [SerializeField] PosterityObject _posterity;

    void Start() {
        if (_playerHealth != null) { _playerHealth.Instance.OnDeath += PosterityDeathCount; }
        if (_bossHealth != null) { _bossHealth.Instance.OnDeath += PosterityBossDeathCount; }
    }

    private void OnDestroy() {
        if (_playerHealth != null) { _playerHealth.Instance.OnDeath -= PosterityDeathCount; }
        if (_bossHealth != null) { _bossHealth.Instance.OnDeath -= PosterityBossDeathCount; }
    }

    private void PosterityDeathCount() {
        if (_posterity == null) { return; }
        ++_posterity.deathCount;
    }

    private void PosterityBossDeathCount() {
        if (_posterity == null) { return; }
        ++_posterity.bossDeathCount;
    }

    public void PosterityNbTimeTalkingUpdate(int nb) {
        _posterity.nbTimeTalkedToNorna = nb;
    }
}
