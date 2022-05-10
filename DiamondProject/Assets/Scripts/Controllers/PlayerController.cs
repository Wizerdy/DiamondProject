using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using ToolsBoxEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] EntityMovement _movement;
    [SerializeField] EntityMeleeAttack _meleeAttack;
    [SerializeField] EntityChargeAttack _chargeMeleeAttack;
    [SerializeField] EntityRangedAttack _rangedAttack;
    [SerializeField] EntityInteract _interact;
    [SerializeField] Health _health;
    [SerializeField] EntityOverHeat _overheat;
    [SerializeField] EntitySprite _sprite;
    [SerializeField] Reference<Camera> _camera;
    [SerializeField] Animator _animator;
    [SerializeField] Reference<IMeetARealBoss> _boss;
    [SerializeField] UnityEvent<AttackType> _onAttack;

    [Header("Value")]
    [SerializeField] float _clickTime = 0.1f;
    [SerializeField] int _rangeAttackHeat = 5;

    //[Header("Dialogue")]
    //[SerializeField] TextInteraction textInteraction;

    PlayerControls _controls = null;
    Vector2 mousePosition = Vector2.up;
    int _cantMoveToken = 0;

    // Clicks
    float _clickTimer = 0f;
    bool _meleeAttackClick = false;

    #region Properties

    public bool CanMove { 
        get => _cantMoveToken <= 0; 
        set { 
            if (!value) { _cantMoveToken++; }
            else { _cantMoveToken = Mathf.Max(--_cantMoveToken, 0); }
        } 
    }
    public Health Health => _health;
    public EntityMovement Movement => _movement;
    public Vector2 Orientation => _movement?.Orientation ?? Vector2.zero;
    public Vector2 Direction => _movement?.Direction ?? Vector2.zero;

    Vector2? MousePosition => _camera?.Instance?.ScreenToWorldPoint(mousePosition) ?? null;
    Vector2 LookDirection { 
        get {
            Vector2 direction = MousePosition != null ? (MousePosition.Value - transform.Position2D()).normalized : Vector2.up;
            if (direction == Vector2.zero) { direction = Vector2.up; }
            return direction;
        }
    }

    public bool PerfomingMeleeAttack => (_chargeMeleeAttack?.IsAttacking ?? false) || (_meleeAttack?.IsAttacking ?? false);

    public event UnityAction<AttackType> OnAttack { add => _onAttack.AddListener(value); remove => _onAttack.RemoveListener(value); }

    #endregion

    #region Unity Callbacks

    private void Reset() {
        _health = GetComponent<Health>();
    }

    void Awake() {
        #region Player Controls

        _controls = new PlayerControls();
        _controls.GamePlay.Enable();
        //_controls.GamePlay.Move.performed += _Move;
        //_controls.GamePlay.Move.canceled += _Move;

        _controls.GamePlay.Interact.started += _Interact;

        _controls.Battle.Enable();
        _controls.Battle.MeleeAttack.performed += _MeleeAttackPerformed;
        _controls.Battle.MeleeAttack.canceled += _MeleeAttackCanceled;

        _controls.Battle.AttackDirection.performed += _SetMousePosition;

        _controls.Dialogue.Enable();
        //_controls.Dialogue.DialogueInteraction.started += _DialogueInteraction;

        #endregion

        if (_interact != null) { _interact.OnStopInteract += _StopInteracting; }

        #region Melee Attack

        if (_chargeMeleeAttack != null) {
            _chargeMeleeAttack.OnCharging += _DontMove;
            _chargeMeleeAttack.OnAttackEnd += _YouCanMove;
            _chargeMeleeAttack.OnOverCharge += _OverChargedMeleeAttack;
        }

        #endregion
    }

    private void OnDestroy() {
        _controls.GamePlay.Move.performed -= _Move;
        _controls.GamePlay.Move.canceled -= _Move;

        _controls.GamePlay.Interact.started -= _Interact;

        _controls.Battle.AttackDirection.performed += _SetMousePosition;

        //_controls.Dialogue.DialogueInteraction.started -= _DialogueInteraction;

        if (_interact != null) { _interact.OnStopInteract -= _StopInteracting; }
    }

    private void Update() {
        //if (_controls.Battle.Attack.ReadValue<float>() == 1) {
        //    MeleeAttack();
        //}

        //if (_controls.GamePlay.Move.ReadValue<float>() == 1) {
            Move(_controls.GamePlay.Move.ReadValue<Vector2>());
        //}

        if (_meleeAttackClick) {
            _clickTimer += Time.deltaTime;
            if (_clickTimer > _clickTime) {
                ChargeMeleeAttack();
                _chargeMeleeAttack?.UpdateDirection(LookDirection);
            }
        }

        if (_controls.Battle.RangedAttack.ReadValue<float>() == 1) {
            _RangedAttack();
        }
    }

    private void LateUpdate() {
        if (MousePosition == null) { return; }
        _sprite?.LookAt(MousePosition.Value - transform.Position2D());
    }

    #endregion

    #region Movements

    private void _Move(InputAction.CallbackContext cc) {
        Move(cc.ReadValue<Vector2>());
    }

    private void Move(Vector2 direction) {
        if (!CanMove) { direction = Vector2.zero; }
        _movement?.Move(direction);
    }

    private void _DontMove() {
        CanMove = false;
    }

    private void _YouCanMove() {
        CanMove = true;
    }

    #region Extensions

    private void _DontMove(Vector2 direction) {
        _DontMove();
    }

    #endregion

    #endregion

    #region Melee Attack

    private void _MeleeAttackPerformed(InputAction.CallbackContext cc) {
        if (_meleeAttackClick) { return; }
        _meleeAttackClick = true;
        _clickTimer = 0f;
    }

    private void _MeleeAttackCanceled(InputAction.CallbackContext cc) {
        if (!_meleeAttackClick) { return; }
        _meleeAttackClick = false;
        
        if (_clickTimer <= _clickTime) {
            NormalAttack();
        } else {
            UnleashChargeMeleeAttack();
        }
    }

    private void NormalAttack() {
        if (!_meleeAttack?.CanAttack ?? true) { return; }
        _meleeAttack?.Attack(LookDirection);
        _onAttack?.Invoke(AttackType.MELEE);
    }

    private void ChargeMeleeAttack() {
        if (!_chargeMeleeAttack?.CanAttack ?? true) { return; }
        _chargeMeleeAttack?.StartCharging(LookDirection);
    }

    private void UnleashChargeMeleeAttack() {
        if (_chargeMeleeAttack?.CanAttack ?? true) { return; }
        _chargeMeleeAttack?.StopCharging(LookDirection);
    }

    private void _OverChargedMeleeAttack(Vector2 direction) {
        _meleeAttackClick = false;
    }

    #endregion

    private void _RangedAttack() {
        if (PerfomingMeleeAttack) { return; }
        if (!_rangedAttack?.CanAttack ?? true) { return; }
        if (!_overheat?.Overheating ?? true) { return; }

        _rangedAttack?.Attack(LookDirection);
        _onAttack?.Invoke(AttackType.RANGE);
        if (_overheat != null) { _overheat.Heat += _rangeAttackHeat; }
    }

    private void _Interact(InputAction.CallbackContext cc) {
        NPC npc = _interact?.GetNearestNpc() ?? null;
        if (npc == null) { return; }
        _interact.Interact(npc);
        _controls.GamePlay.Disable();
        _controls.Battle.Disable();
    }

    private void _StopInteracting(NPC npc) {
        _controls.GamePlay.Enable();
    }

    //private void _DialogueInteraction(InputAction.CallbackContext cc) {
    //    textInteraction?.OnClickEvent();
    //}

    private void _SetMousePosition(InputAction.CallbackContext cc) {
        mousePosition = cc.ReadValue<Vector2>();
    }

    private void _Jump() {
        _animator.SetTrigger("Jump");
    }

    //private void OnDrawGizmos() {
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawLine(transform.position.Override(5f, Axis.Z), transform.position + (LookDirection * 5f).To3D(5f));
    //}
}
