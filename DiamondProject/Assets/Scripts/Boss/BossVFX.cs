using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class BossVFX : MonoBehaviour
{
    [SerializeField] GuardianSeeds _guardianSeeds;
    [SerializeField] FireMissile _fireMissile;
    [SerializeField] Fireball _fireball;
    [SerializeField] HealthProxy _healthProxy;
    [SerializeField] MMFeedbacks _growTree;
    [SerializeField] MMFeedbacks _minion;
    [SerializeField] MMFeedbacks _spike;
    [SerializeField] MMFeedbacks _hit;
    [SerializeField] MMFeedbacks _heal;
    [SerializeField] MMFeedbacks _death;

    private void Start()
    {
        _guardianSeeds.OnCast += BossGrowTreeFeedback;
        _fireMissile.OnCast += BossMinionFeedback;
        _fireball.OnCast += BossSpikeFeedback;
        _healthProxy.OnHit += BossHitFeedback;
        _healthProxy.OnHeal += BossHealFeedback;
        _healthProxy.OnDeath += BossDeathFeedback;
    }

    private void OnDestroy() { 
        _guardianSeeds.OnCast -= BossGrowTreeFeedback;
        _fireMissile.OnCast -= BossMinionFeedback;
        _fireball.OnCast -= BossSpikeFeedback;
        _healthProxy.OnHit -= BossHitFeedback;
        _healthProxy.OnHeal -= BossHealFeedback;
        _healthProxy.OnDeath -= BossDeathFeedback;
    }

    private void BossGrowTreeFeedback() {
        _growTree.PlayFeedbacks();
    }

    private void BossHitFeedback(int amount) {
        _hit.PlayFeedbacks();
    }

    private void BossHealFeedback(int amount) {
        _heal.PlayFeedbacks();
    }

    private void BossDeathFeedback() {
        _death.PlayFeedbacks();
    }

    private void BossMinionFeedback() {
        _minion.PlayFeedbacks();
    }

    private void BossSpikeFeedback() {
        _spike.PlayFeedbacks();
    }
}
