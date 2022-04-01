using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class PlayerVFX : MonoBehaviour {
    [SerializeField] EntityMovement _eMovement;
    [SerializeField] EntityMeleeAttack _eMeleeAttack;
    [SerializeField] Health _eTempHealth;
    [SerializeField] EntityRangedAttack _eRangedAttack;
    [SerializeField] MMFeedbacks _acceleration;
    [SerializeField] MMFeedbacks _deceleration;
    [SerializeField] MMFeedbacks _meleeAttack;
    [SerializeField] MMFeedbacks _meleeHit;
    [SerializeField] MMFeedbacks _rangeAttack;
    [SerializeField] MMFeedbacks _rangeHit;
    [SerializeField] MMFeedbacks _turnAround;
    [SerializeField] MMFeedbacks _interact;
    [SerializeField] MMFeedbacks _hit;
    [SerializeField] MMFeedbacks _death;
    //[SerializeField] MMFeedbacks _heal;

    private void Start() {
        _eMovement.OnAcceleration += PlayAccelerationFeedback;
        _eMovement.OnDeceleration += PlayDecelerationFeedback;
        _eMovement.OnTurnAround += PlayTurnAroundFeedback;
        _eMeleeAttack.OnAttack += PlayMeleeAttackFeedback;
        _eMeleeAttack.OnHit += PlayMeleeHitFeedback;
        _eTempHealth.OnHit += PlayHitFeedback;
        _eTempHealth.OnDeath += PlayDeathFeedback;
        _eRangedAttack.OnAttack += PlayRangedAttackFeedback;
        //_eRangedAttack.OnHit += PlayRangedHitFeedback;
    }

    private void OnDestroy() {
        _eMovement.OnAcceleration -= PlayAccelerationFeedback;
        _eMovement.OnDeceleration -= PlayDecelerationFeedback;
        _eMovement.OnTurnAround -= PlayTurnAroundFeedback;
        _eMeleeAttack.OnAttack -= PlayMeleeAttackFeedback;
        _eMeleeAttack.OnHit -= PlayMeleeHitFeedback;
        _eTempHealth.OnDeath -= PlayDeathFeedback;
        _eTempHealth.OnHit -= PlayHitFeedback;
        _eRangedAttack.OnAttack -= PlayRangedAttackFeedback;
        //_eRangedAttack.OnHit -= PlayRangedHitFeedback;
    }

    private void PlayAccelerationFeedback(Vector2 direction) {
        _acceleration.PlayFeedbacks();
    }

    private void PlayDecelerationFeedback(Vector2 direction) {
        _deceleration.PlayFeedbacks();
    }

    private void PlayTurnAroundFeedback(Vector2 direction) {
        _turnAround.PlayFeedbacks();
    }

    private void PlayMeleeAttackFeedback(Vector2 direction) {
        _meleeAttack.PlayFeedbacks();
    }

    private void PlayMeleeHitFeedback(GameObject go) {
        _meleeHit.PlayFeedbacks();
    }

    private void PlayHitFeedback(int amount) {
        _hit.PlayFeedbacks();
    }

    private void PlayDeathFeedback() {
        _death.PlayFeedbacks();
    }

    private void PlayRangedAttackFeedback(Vector2 direction) {
        _rangeAttack.PlayFeedbacks();
    }

    private void PlayRangedHitFeedback(Vector2 direction) {
        _rangeHit.PlayFeedbacks();
    }

}
