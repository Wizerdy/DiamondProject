using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [SerializeField] EntityMovement _movements;

    PlayerControls _controls = null;

    void Start() {
        _controls = new PlayerControls();
        _controls.GamePlay.Enable();
        _controls.GamePlay.Move.performed += Move;
        _controls.GamePlay.Move.canceled += Move;

        //_controls.Battle.Enable();
        //_controls.Battle.Attack.performed += cc => Attack(facingDirection);
        //_controls.Battle.RangedAttack.performed += cc => RangedAttack(facingDirection);

        //_facingDirection = Vector2.up;
    }

    void Update() {

    }

    private void OnDestroy() {
        
    }

    private void Move(InputAction.CallbackContext cc) {
        _movements?.Move(cc.ReadValue<Vector2>());
    }
}
