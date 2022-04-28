using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;
using MoreMountains.Feedbacks;

public class BossVFX : MonoBehaviour {
    [Header("References")]
    [SerializeField] TrackingTree _trackingTree;
    [SerializeField] FireMissile _fireMissile;
    [SerializeField] Fireball _fireball;
    [SerializeField] HealthProxy _healthProxy;
    [SerializeField] IMeetARealBoss _boss;
    [Header("VFX")]
    [SerializeField] MMFeedbacks _growTree;
    [SerializeField] MMFeedbacks _minion;
    [SerializeField] MMFeedbacks _spike;
    [SerializeField] MMFeedbacks _hit;
    [SerializeField] MMFeedbacks _heal;
    [SerializeField] MMFeedbacks _death;

    private void Start() {
        //_trackingTree.OnCast += BossGrowTreeFeedback;
        if (_fireMissile != null) { _fireMissile.OnCast += BossMinionFeedback; }
        if (_fireball != null) { _fireball.OnCast += BossSpikeFeedback; }
        if (_healthProxy != null) {
            _healthProxy.OnHit += BossHitFeedback;
            _healthProxy.OnHeal += BossHealFeedback;
        }
        if (_boss != null) { _boss.OnDeath += BossDeathFeedback; }
    }

    private void OnDestroy() {
        //_trackingTree.OnCast -= BossGrowTreeFeedback;
        _fireMissile.OnCast -= BossMinionFeedback;
        _fireball.OnCast -= BossSpikeFeedback;
        _healthProxy.OnHit -= BossHitFeedback;
        _healthProxy.OnHeal -= BossHealFeedback;
        _healthProxy.OnDeath -= BossDeathFeedback;
    }

    private void BossGrowTreeFeedback() {
        _growTree?.PlayFeedbacks();
    }

    private void BossHitFeedback(int amount) {
        //_hit?.PlayFeedbacks();
    }

    private void BossHealFeedback(int amount) {
        _heal?.PlayFeedbacks();
    }

    private void BossDeathFeedback() {
        this.Hurl(DebugType.WARNING);
        _death?.PlayFeedbacks();
    }

    private void BossMinionFeedback() {
        _minion?.PlayFeedbacks();
    }

    private void BossSpikeFeedback() {
        _spike?.PlayFeedbacks();
    }
}
