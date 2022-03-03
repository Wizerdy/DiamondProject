using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class PlayerVFX : MonoBehaviour {
    [SerializeField] EntityMovement _eMovement;
    [SerializeField] EntityMeleeAttack _eMeleeAttack;
    [SerializeField] TempHealth _eTempHealth;
    [SerializeField] MMFeedbacks _run;
    [SerializeField] MMFeedbacks _meleeAttack;
    [SerializeField] MMFeedbacks _meleeHit;
    [SerializeField] MMFeedbacks _rangeAttack;
    [SerializeField] MMFeedbacks _turnAround;
    [SerializeField] MMFeedbacks _interact;
    [SerializeField] MMFeedbacks _hit;
    [SerializeField] MMFeedbacks _death;
    //[SerializeField] MMFeedbacks _heal;

    private void Start() {
        _eMovement.OnAcceleration += PlayRunFeedback;
        _eMovement.OnTurnAround += PlayTurnAroundFeedback;
        _eMeleeAttack.OnAttack += PlayMeleeAttackFeedback;
        _eMeleeAttack.OnHit += PlayMeleeHitFeedback;
        _eTempHealth.OnHit += PlayHitFeedback;
    }

    private void OnDestroy() {
        _eMovement.OnAcceleration -= PlayRunFeedback;
        _eMovement.OnTurnAround -= PlayTurnAroundFeedback;
        _eMeleeAttack.OnAttack -= PlayMeleeAttackFeedback;
        _eMeleeAttack.OnHit -= PlayMeleeHitFeedback;
        _eTempHealth.OnHit -= PlayHitFeedback;
    }

    private void PlayRunFeedback(Vector2 direction) {
        _run.PlayFeedbacks();
    }

    private void PlayTurnAroundFeedback(Vector2 direction) {
        _turnAround.PlayFeedbacks();
    }

    private void PlayMeleeAttackFeedback(Vector2 direction) {
        _meleeAttack.PlayFeedbacks();
    }

    private void PlayMeleeHitFeedback(Collider2D coll2D) {
        _meleeHit.PlayFeedbacks();
    }

    private void PlayHitFeedback(int amount) {
        _hit.PlayFeedbacks();
    }
}
