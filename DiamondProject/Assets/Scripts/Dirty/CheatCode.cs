using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ToolsBoxEngine;

public class CheatCode : MonoBehaviour {
    [SerializeField] HealthReference _boss;
    [SerializeField] LiaReference _lia;
    [SerializeField] PlayerControllerReference _playerController;

    PlayerControls _playerControls;

    void Start() {
        PlayerControls _playerControls = new PlayerControls();
        _playerControls.CheatCode.Enable();
        _playerControls.CheatCode.Killboss.started += _KillBoss;
        _playerControls.CheatCode.WinterShape.started += _ChangeShape_Winter;
        _playerControls.CheatCode.FallShape.started += _ChangeShape_Fall;
        _playerControls.CheatCode.Suicide.started += _Suicide;
    }

    //private void OnDestroy() {
    //    _playerControls.CheatCode.Killboss.started -= _KillBoss;
    //    _playerControls.CheatCode.WinterShape.started -= _ChangeShape_Winter;
    //    _playerControls.CheatCode.FallShape.started -= _ChangeShape_Fall;
    //    _playerControls.CheatCode.Suicide.started -= _Suicide;
    //}

    private void _KillBoss(InputAction.CallbackContext cc) {
        if (cc.ReadValue<float>() == 1f) {
            if (!_boss.IsValid()) { return; }
            for (int i = 0; i < 5; i++) {
                _boss.Instance.CanTakeDamage = true;
            }
            _boss.Instance.TakeDamage(_boss.Instance.CurrentHealth);
        }
    }

    private void _ChangeShape_Winter(InputAction.CallbackContext cc) {
        if (!_lia.IsValid()) { return; }
        if (cc.ReadValue<float>() == 1f) {
            _lia.Instance.NewForm(Shape.WINTER);
        }
    }

    private void _ChangeShape_Fall(InputAction.CallbackContext cc) {
        if (!_lia.IsValid()) { return; }
        if (cc.ReadValue<float>() == 1f) {
            _lia.Instance.NewForm(Shape.FALL);
        }
    }

    private void _Suicide(InputAction.CallbackContext cc) {
        if (!_playerController.IsValid()) { return; }
        if (cc.ReadValue<float>() == 1f) {
            _playerController.Instance.Health.TakeDamage(9999, "");
        }
    }
}
