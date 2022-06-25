using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.InputSystem;
using ToolsBoxEngine;

public class CheatCode : MonoBehaviour {
    [SerializeField] HealthReference _boss;
    [SerializeField] LiaReference _lia;
    [SerializeField] PlayerControllerReference _playerController;
    [SerializeField] DeathManager _deathManager;
    [SerializeField] MMFeedbacks _zoomPlayerFeedback;
    [SerializeField] MMFeedbacks _seeWholeArenaFeedback;
    [SerializeField] MMFeedbacks _slowMotionFeedback;
    [SerializeField] MMFeedbacks _toggleUIFeedback;

    [Header("States")]
    [SerializeField] bool _active = false;
    [SerializeField] bool _killBoss = true;
    [SerializeField] bool _winterShape = true;
    [SerializeField] bool _fallShape = true;
    [SerializeField] bool _suicide = true;
    [SerializeField] bool _zoomPlayer = true;
    [SerializeField] bool _dezoomArena = true;
    [SerializeField] bool _slowMotion = true;
    [SerializeField] bool _toggleUI = true;
    [SerializeField] bool _invicibility = true;

    PlayerControls _playerControls;
    bool _isInvincible = false;

    void Start() {
        if (!_active) { return; }
        PlayerControls _playerControls = new PlayerControls();
        _playerControls.CheatCode.Enable();
        _playerControls.CheatCode.Killboss.started += _KillBoss;
        _playerControls.CheatCode.WinterShape.started += _ChangeShape_Winter;
        _playerControls.CheatCode.FallShape.started += _ChangeShape_Fall;
        _playerControls.CheatCode.Suicide.started += _Suicide;
        _playerControls.CheatCode.ZoomPlayer.started += _ZoomPlayer;
        _playerControls.CheatCode.SeeWholeArena.started += _SeeWholeArena;
        _playerControls.CheatCode.SlowMotion.started += _SlowMotion;
        _playerControls.CheatCode.ToggleUI.started += _ToggleUI;
        _playerControls.CheatCode.AlexandreSama.started += _Invicibility;
    }

    private void OnDestroy() {
        if (_playerControls == null) { return; }
        _playerControls.CheatCode.Killboss.started -= _KillBoss;
        _playerControls.CheatCode.WinterShape.started -= _ChangeShape_Winter;
        _playerControls.CheatCode.FallShape.started -= _ChangeShape_Fall;
        _playerControls.CheatCode.Suicide.started -= _Suicide;
        _playerControls.CheatCode.ZoomPlayer.started -= _ZoomPlayer;
        _playerControls.CheatCode.SeeWholeArena.started -= _SeeWholeArena;
        _playerControls.CheatCode.ToggleUI.started -= _ToggleUI;
    }

    private void _KillBoss(InputAction.CallbackContext cc) {
        if (!_killBoss) { return; }
        if (cc.ReadValue<float>() == 1f) {
            if (!_boss.IsValid()) { return; }
            for (int i = 0; i < 5; i++) {
                _boss.Instance.CanTakeDamage = true;
            }
            _boss.Instance.TakeDamage(_boss.Instance.CurrentHealth);
        }
    }

    private void _ChangeShape_Winter(InputAction.CallbackContext cc) {
        if (!_winterShape) { return; }
        if (!_lia.IsValid()) { return; }
        if (cc.ReadValue<float>() == 1f) {
            _lia.Instance.NewForm(Shape.WINTER);
        }
    }

    private void _ChangeShape_Fall(InputAction.CallbackContext cc) {
        if (!_fallShape) { return; }
        if (!_lia.IsValid()) { return; }
        if (cc.ReadValue<float>() == 1f) {
            _lia.Instance.NewForm(Shape.FALL);
        }
    }

    private void _Suicide(InputAction.CallbackContext cc) {
        if (!_suicide) { return; }
        if (!_playerController.IsValid()) { return; }
        if (cc.ReadValue<float>() == 1f) {
            _playerController.Instance.Health.TakeDamage(9999, "");
        }
    }

    private void _ZoomPlayer(InputAction.CallbackContext cc) {
        if (!_zoomPlayer) { return; }
        if (cc.ReadValue<float>() == 1f) {
            _zoomPlayerFeedback?.PlayFeedbacks();
        }
    }

    private void _SeeWholeArena(InputAction.CallbackContext cc) {
        if (!_dezoomArena) { return; }
        if (cc.ReadValue<float>() == 1f) {
            _seeWholeArenaFeedback?.PlayFeedbacks();
        }
    }

    private void _SlowMotion(InputAction.CallbackContext cc) {
        if (!_slowMotion) { return; }
        if (cc.ReadValue<float>() == 1f) {
            _slowMotionFeedback?.PlayFeedbacks();
        }
    }

    private void _ToggleUI(InputAction.CallbackContext cc) {
        if (!_toggleUI) { return; }
        if (cc.ReadValue<float>() == 1f) {
            _toggleUIFeedback?.PlayFeedbacks();
        }
    }

    private void _Invicibility(InputAction.CallbackContext cc) {
        if (!_invicibility) { return; }
        if (cc.ReadValue<float>() == 1f) {
            _isInvincible = !_isInvincible;
            _deathManager.CanDie = !_deathManager.CanDie;
        }
    }
}
