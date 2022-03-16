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
    [SerializeField] Reference<Camera> _camera;

    [Header("Dialogue")]
    [SerializeField] TextInteraction textInteraction;

    PlayerControls _controls = null;

    Vector2 mousePosition = Vector2.up;

    #region Properties

    public Vector2 Orientation => _movement?.Orientation ?? Vector2.zero;
    public Vector2 Direction => _movement?.Direction ?? Vector2.zero;
    public Health Health => _health;
    public EntityMovement Movement => _movement;

    Vector2 MousePosition => _camera.Instance.ScreenToWorldPoint(mousePosition);

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
        _controls.Battle.AttackDirection.performed += SetMousePosition;

        _controls.Dialogue.Enable();
        _controls.Dialogue.DialogueInteraction.started += DialogueInteraction;

        if (_interact != null) { _interact.OnStopInteract += StopInteracting; }
    }

    private void Update() {
        if (_controls.Battle.Attack.ReadValue<float>() == 1) {
            MeleeAttack();
        }

        if (_controls.Battle.RangedAttack.ReadValue<float>() == 1) {
            RangedAttack();
        }
    }

    private void OnDestroy() {
        _controls.GamePlay.Move.performed -= Move;
        _controls.GamePlay.Move.canceled -= Move;

        _controls.GamePlay.Interact.started -= Interact;

        _controls.Battle.AttackDirection.performed += SetMousePosition;

        _controls.Dialogue.DialogueInteraction.started -= DialogueInteraction;

        if (_interact != null) { _interact.OnStopInteract -= StopInteracting; }
    }

    private void Move(InputAction.CallbackContext cc) {
        _movement?.Move(cc.ReadValue<Vector2>());
    }

    private void MeleeAttack() {
        Vector2 direction = (MousePosition - transform.Position2D()).normalized;
        if (direction == Vector2.zero) { direction = Vector2.up; }
        _meleeAttack?.Attack(direction);
    }

    private void RangedAttack() {
        Vector2 direction = (MousePosition - transform.Position2D()).normalized;
        if (direction == Vector2.zero) { direction = Vector2.up; }
        _rangedAttack?.Attack(direction);
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

    private void SetMousePosition(InputAction.CallbackContext cc) {
        mousePosition = cc.ReadValue<Vector2>();
    }
}
