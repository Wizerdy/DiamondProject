using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ToolsBoxEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] EntityMovement _movement;
    [SerializeField] EntityMeleeAttack _meleeAttack;
    [SerializeField] EntityInteract _interact;
    [SerializeField] EntityRangedAttack _rangedAttack;
    [SerializeField] Health _health;

    [Header("Dialogue")]
    [SerializeField] TextInteraction textInteraction;

    PlayerControls _controls = null;

    Vector2 mousePosition = Vector2.up;

    #region Properties

    public Vector2 Orientation => _movement?.Orientation ?? Vector2.zero;
    public Vector2 Direction => _movement?.Direction ?? Vector2.zero;
    public Health Health => _health;
    public EntityMovement Movement => _movement;

    #endregion

    private void Reset() {
        _health = GetComponent<Health>();
    }

    void Awake() {
        _controls = new PlayerControls();
        _controls.GamePlay.Enable();
        _controls.GamePlay.Move.performed += Move;
        _controls.GamePlay.Move.canceled += Move;

        _controls.GamePlay.Interact.started += Interact;

        _controls.Battle.Enable();
        _controls.Battle.Attack.performed += MeleeAttack;
        _controls.Battle.Attack.started += MeleeAttack;
        _controls.Battle.RangedAttack.performed += RangedAttack;
        _controls.Battle.AttackDirection.performed += MousePosition;

        _controls.Dialogue.Enable();
        _controls.Dialogue.DialogueInteraction.started += DialogueInteraction;

        if (_interact != null) { _interact.OnStopInteract += StopInteracting; }
    }

    private void OnDestroy() {
        _controls.GamePlay.Move.performed -= Move;
        _controls.GamePlay.Move.canceled -= Move;

        _controls.GamePlay.Interact.started -= Interact;

        _controls.Battle.Attack.performed -= MeleeAttack;
        _controls.Battle.RangedAttack.performed -= RangedAttack;

        _controls.Dialogue.DialogueInteraction.started -= DialogueInteraction;

        if (_interact != null) { _interact.OnStopInteract -= StopInteracting; }
    }

    private void Move(InputAction.CallbackContext cc) {
        _movement?.Move(cc.ReadValue<Vector2>());
    }

    private void MeleeAttack(InputAction.CallbackContext cc) {
        //Vector2 direction = Direction != Vector2.zero ? Direction : Orientation;
        Vector2 direction = (mousePosition - transform.Position2D()).normalized;
        //this.Hurl(mousePosition.ToString());
        if (direction == Vector2.zero) { direction = Vector2.up; }
        _meleeAttack?.Attack(direction);
    }

    private void Interact(InputAction.CallbackContext cc) {
        NPC npc = _interact?.GetNearestNpc() ?? null;
        if (npc == null) { return; }
        _interact.Interact(npc);
        _controls.GamePlay.Disable();
        _controls.Battle.Disable();
    }

    private void StopInteracting(NPC npc) {
        _controls.GamePlay.Enable();
    }

    private void DialogueInteraction(InputAction.CallbackContext cc) {
        textInteraction?.OnClickEvent();
    }

    private void RangedAttack(InputAction.CallbackContext cc) {
        //Vector2 direction = Direction != Vector2.zero ? Direction : Orientation;
        //if (direction == Vector2.zero) { direction = Vector2.up; }
        Vector2 direction = Vector2.up;
        _rangedAttack?.Attack(direction);
    }

    private void MousePosition(InputAction.CallbackContext cc) {
        mousePosition = Camera.main.ScreenToWorldPoint(cc.ReadValue<Vector2>());
    }
}
