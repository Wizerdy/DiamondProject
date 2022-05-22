using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ToolsBoxEngine;

public class CheatCode : MonoBehaviour {
    [SerializeField] HealthReference _boss;

    PlayerControls _playerControls;

    void Start() {
        PlayerControls _playerControls = new PlayerControls();
        _playerControls.CheatCode.Killboss.Enable();
        _playerControls.CheatCode.Killboss.started += _KillBoss;
    }

    private void _KillBoss(InputAction.CallbackContext cc) {
        if (cc.ReadValue<float>() == 1f) {
            if (!_boss.IsValid()) { return; }
            for (int i = 0; i < 5; i++) {
                _boss.Instance.CanTakeDamage = true;
            }
            _boss.Instance.TakeDamage(_boss.Instance.CurrentHealth);
        }
    }
}
