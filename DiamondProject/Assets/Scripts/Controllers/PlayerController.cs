using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [SerializeField] EntityMovement _movement;
    [SerializeField] EntityMeleeAttack _meleeAttack;
    [SerializeField] EntityInteract _interact;

    PlayerControls _controls = null;

    #region Properties

    public Vector2 Orientation => _movement?.Orientation ?? Vector2.zero;
    public Vector2 Direction => _movement?.Direction ?? Vector2.zero;

    #endregion

    void Awake() {
        _controls = new PlayerControls();
        _controls.GamePlay.Enable();
        _controls.GamePlay.Move.performed += Move;
        _controls.GamePlay.Move.canceled += Move;

        _controls.GamePlay.Interact.started += Interact;

        _controls.Battle.Enable();
        _controls.Battle.Attack.performed += Attack;
        //_controls.Battle.RangedAttack.performed += cc => RangedAttack(facingDirection);


        //_facingDirection = Vector2.up;
    }

    private void OnDestroy() {
        
    }

    private void Move(InputAction.CallbackContext cc) {
        _movement?.Move(cc.ReadValue<Vector2>());
    }

    private void Attack(InputAction.CallbackContext cc) {
        _meleeAttack?.Attack(Direction != Vector2.zero ? Direction : Orientation);
    }

    private void Interact(InputAction.CallbackContext cc) {
        NPC npc = _interact.GetNearestNpc();
        _interact?.Interact(npc);
        _controls.GamePlay.Disable();
        //npc.OnStopInteracting += ;
    }

    //private void StopInteracting()
}
