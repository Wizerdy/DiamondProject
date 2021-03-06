using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using ToolsBoxEngine;

public class PlayerController : MonoBehaviour {
    private enum ClickType {
        NONE, MELEE, RANGE
    }

    public enum InputType {
        MOVEMENT, COMBAT, UI
    }

    [SerializeField] PlayerInput _input;

    [SerializeField] EntityMovement _movement;
    [SerializeField] EntityMeleeAttack _meleeAttack;
    [SerializeField] EntityChargeAttack _chargeMeleeAttack;
    [SerializeField] EntityRangedAttack _rangedAttack;
    [SerializeField] EntityChargeRanged _chargeRangedAttack;
    [SerializeField] EntityOverHeat _overheat;
    [SerializeField] EntityInteract _interact;
    [SerializeField] Health _health;
    [SerializeField] DamageModifier _lightningImmunity;
    [SerializeField] EntitySprite _sprite;
    [SerializeField] Reference<Camera> _camera;
    [SerializeField] SkeletonMecanim _spine;
    [SerializeField] Animator _animator;
    [SerializeField] Reference<IMeetARealBoss> _boss;
    [SerializeField] GameObject _pauseUI;
    [SerializeField] VisualEffect _healingPart;
    [SerializeField] UnityEvent<AttackType> _onAttack;

    [Header("Value")]
    [SerializeField] GameObject _bullet;
    [SerializeField] GameObject _chargeBullet;
    [SerializeField] float _clickTime = 0.1f;
    //[SerializeField] int _rangeAttackHeat = 5;
    [SerializeField, Range(0f, 1f)] float _chargeSlow = 0.35f;

    //[Header("Dialogue")]
    //[SerializeField] TextInteraction textInteraction;

    PlayerControls _controls = null;
    Vector2 mousePosition = Vector2.up;
    int _cantMoveToken = 0;

    EntityMovement.SpeedModifier _currentChargeSlow = null;

    Vector2 _lastDirection = Vector2.up;

    // Clicks
    float _clickTimer = 0f;
    ClickType _clickType = ClickType.NONE;

    float _deltaHeat = 0f;

    InputAction _inMove;

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
    public bool PerfomingRangeAttack => (_chargeRangedAttack?.IsAttacking ?? false);

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
        _controls.Dialogue.Enable();
        _controls.Battle.Enable();
        _controls.UI.Enable();

        //_controls.GamePlay.Interact.started += _Interact;

        //_controls.Battle.MeleeAttack.performed += _MeleeAttackPerformed;
        //_controls.Battle.MeleeAttack.canceled += _MeleeAttackCanceled;

        //_controls.Battle.RangedAttack.performed += _RangeAttackPerformed;
        //_controls.Battle.RangedAttack.canceled += _RangeAttackCanceled;

        //_controls.Battle.AttackDirection.performed += _SetMousePosition;

        //_controls.UI.Pause.performed += _TogglePause;

        #endregion

        if (_interact != null) { _interact.OnStopInteract += _StopInteracting; }

        #region Melee Attack

        if (_chargeMeleeAttack != null) {
            //_chargeMeleeAttack.OnCharging += _DontMove;
            //_chargeMeleeAttack.OnAttackEnd += _YouCanMove;
            _chargeMeleeAttack.OnOverCharge += _OverChargedMeleeAttack;
        }

        #endregion

        #region Range Attack

        if (_chargeRangedAttack != null) {
            //_chargeRangedAttack.OnCharging += _DontMove;
            //_chargeRangedAttack.OnAttackEnd += _YouCanMove;
            _chargeRangedAttack.OnOverCharge += _OverChargedRangeAttack;
        }

        if (_overheat != null) {
            _overheat.OnOverheat += UnleashChargeRangeAttack;
            _overheat.OnOverheat += _OverChargedRangeAttack;
        }

        #endregion

        _inMove = _input.actions["Move"];
        _input.actions["AttackDirection"].performed += _SetMousePosition;
        _input.actions["MeleeAttack"].performed += _MeleeAttackPerformed;
        _input.actions["MeleeAttack"].canceled += _MeleeAttackCanceled;
        _input.actions["RangedAttack"].performed += _RangeAttackPerformed;
        _input.actions["RangedAttack"].canceled += _RangeAttackCanceled;
        _input.actions["Pause"].performed += _TogglePause;

        _input.actions.FindActionMap("Battle").Enable();
        _input.actions.FindActionMap("UI").Enable();
        _input.actions.FindActionMap("GamePlay").Enable();
    }

    private void Start() {
        _lightningImmunity.Resistance = DamageModifier.ResistanceType.NOMODIFIER;
        _chargeRangedAttack?.SetBullet(_chargeBullet);

        _chargeMeleeAttack.OnAttack += _DashingAnimator;
        _chargeMeleeAttack.OnAttackEnd += _StopDashingAnimator;
        //InputSystem.EnableDevice(Mouse.current);
    }

    private void OnDestroy() {
        _controls.GamePlay.Interact.started -= _Interact;

        _controls.Battle.MeleeAttack.performed -= _MeleeAttackPerformed;
        _controls.Battle.MeleeAttack.canceled -= _MeleeAttackCanceled;

        _controls.Battle.RangedAttack.performed -= _RangeAttackPerformed;
        _controls.Battle.RangedAttack.canceled -= _RangeAttackCanceled;

        _controls.Battle.AttackDirection.performed -= _SetMousePosition;

        _controls.UI.Pause.performed -= _TogglePause;

        if (_interact != null) { _interact.OnStopInteract -= _StopInteracting; }
    }

    private void Update() {
        //Debug.DrawLine(_camera.Instance.ScreenToWorldPoint(Vector3.zero), _camera.Instance.ScreenToWorldPoint(mousePosition), Color.red);
        if (_controls.GamePlay.enabled && (_input?.actions.FindActionMap("GamePlay")?.enabled ?? true)) {
            //Move(_controls.GamePlay.Move.ReadValue<Vector2>());
            Move(_inMove.ReadValue<Vector2>());
        }

        if ((_input?.actions.FindActionMap("GamePlay")?.enabled ?? true) && (_input?.currentControlScheme.Equals("Gamepad") ?? false)) {
            _SetMousePosition(_lastDirection);
        }

        //_SetMousePosition(_inAttackDirection.ReadValue<Vector2>());
        //Debug.Log("d: " + _inAttackDirection.ReadValue<Vector2>());

        if (_controls.Battle.enabled && (_input?.actions.FindActionMap("Battle")?.enabled ?? true)) {
            if (_clickType == ClickType.MELEE) {
                _clickTimer += Time.deltaTime;
                if (_clickTimer > _clickTime) {
                    if (!_chargeMeleeAttack?.IsAttacking ?? false) { ChargeMeleeAttack(); }
                    _chargeMeleeAttack?.UpdateDirection(LookDirection);
                }
            }

            if (_clickType == ClickType.RANGE) {
                _clickTimer += Time.deltaTime;
                if (_clickTimer > _clickTime) {
                    if (!_chargeRangedAttack?.IsAttacking ?? false) { ChargeRangeAttack(); }

                    _chargeRangedAttack?.UpdateDirection(LookDirection);
                    float delta = (_overheat.MaxHeat / _chargeRangedAttack.MaxChargeTime) * Time.deltaTime - 0.001f;
                    if (_deltaHeat >= 1f) {
                        _overheat.Heat += Mathf.FloorToInt(delta + _deltaHeat);
                        _deltaHeat = (delta + _deltaHeat) % 1;
                    } else {
                        _deltaHeat += delta;
                    }
                }
            }
        }
    }

    private void LateUpdate() {
        if (MousePosition == null) { return; }
        _sprite?.LookAt(MousePosition.Value - transform.Position2D());
    }

    #endregion

    #region Movements

    public void MoveTo(Vector2 position, float speedFactor = 1f) {
        StartCoroutine(IMoveTo(position, speedFactor));

        IEnumerator IMoveTo(Vector2 position, float speedFactor = 1f) {
            CanMove = false;
            _animator?.SetBool("Running", true);
            yield return _movement?.MoveTo(position, speedFactor);
            _animator?.SetBool("Running", false);
            CanMove = true;
        }
    }

    public void LookAt(Vector2 direction) {
        PlayerTurnover(direction);
    }

    private void _Move(InputAction.CallbackContext cc) {
        Move(cc.ReadValue<Vector2>());
    }

    private void Move(Vector2 direction) {
        if (!CanMove) { direction = Vector2.zero; }
        _animator?.SetBool("Running", direction != Vector2.zero);
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
        if (PerfomingRangeAttack) { return; }
        if (_clickType == ClickType.MELEE) { return; }
        _clickType = ClickType.MELEE;
        _clickTimer = 0f;
    }

    private void _MeleeAttackCanceled(InputAction.CallbackContext cc) {
        if (_clickType != ClickType.MELEE) { return; }
        _clickType = ClickType.NONE;

        if (_clickTimer <= _clickTime) {
            NormalAttack();
        } else {
            UnleashChargeMeleeAttack();
        }
    }

    private void _OverChargedMeleeAttack(Vector2 direction) {
        if (_clickType != ClickType.MELEE) { return; }
        _clickType = ClickType.NONE;
    }

    private void NormalAttack() {
        if (!_meleeAttack?.CanAttack ?? true) { return; }
        CanMove = false;
        StartCoroutine(Tools.Delay(() => CanMove = true, 0.2f));
        _meleeAttack?.Attack(LookDirection);
        _onAttack?.Invoke(AttackType.MELEE);
    }

    private void ChargeMeleeAttack() {
        if (!_chargeMeleeAttack?.CanAttack ?? true) { return; }
        if (_currentChargeSlow != null) { _movement.RemoveSlow(_currentChargeSlow); }
        _currentChargeSlow = _movement.Slow(1f - _chargeSlow, _chargeMeleeAttack.MaxChargeTime);
        _chargeMeleeAttack?.StartCharging(LookDirection);
    }

    private void UnleashChargeMeleeAttack() {
        if (_chargeMeleeAttack?.CanAttack ?? true) { return; }

        if (_currentChargeSlow != null) {
            _movement.RemoveSlow(_currentChargeSlow);
            _currentChargeSlow = null;
        }
        _chargeMeleeAttack?.StopCharging(LookDirection);
        _onAttack?.Invoke(AttackType.MELEE);
    }

    #endregion

    #region Range Attack

    private void _RangeAttackPerformed(InputAction.CallbackContext cc) {
        if (_overheat?.Overheating ?? false) { return; }
        if (PerfomingMeleeAttack) { return; }
        if (_clickType == ClickType.RANGE) { return; }
        _clickType = ClickType.RANGE;
        _clickTimer = 0f;
    }

    private void _RangeAttackCanceled(InputAction.CallbackContext cc) {
        if (_clickType != ClickType.RANGE) { return; }
        _clickType = ClickType.NONE;

        if (_clickTimer <= _clickTime) {
            RangeAttack();
        } else {
            UnleashChargeRangeAttack();
        }
    }

    private void _OverChargedRangeAttack(Vector2 direction) {
        if (_clickType != ClickType.RANGE) { return; }
        _clickType = ClickType.NONE;
        _overheat.SetHeat(1f);
    }

    private void _OverChargedRangeAttack() {
        _OverChargedRangeAttack(Vector2.zero);
    }

    private void ChargeRangeAttack() {
        if (!_chargeRangedAttack?.CanAttack ?? true) { return; }
        if (_currentChargeSlow != null) { _movement.RemoveSlow(_currentChargeSlow); }
        _lightningImmunity.Resistance = DamageModifier.ResistanceType.IMMUNITY;
        _currentChargeSlow = _movement.Slow(1f - _chargeSlow, _chargeRangedAttack.MaxChargeTime);
        _chargeRangedAttack?.StartCharging(LookDirection);
    }

    private void UnleashChargeRangeAttack() {
        if (_chargeRangedAttack?.CanAttack ?? true) { return; }

        if (_currentChargeSlow != null) {
            _movement.RemoveSlow(_currentChargeSlow);
            _currentChargeSlow = null;
        }
        _lightningImmunity.Resistance = DamageModifier.ResistanceType.NOMODIFIER;
        _chargeRangedAttack?.StopCharging(LookDirection);
        _onAttack?.Invoke(AttackType.RANGE);
    }

    private void RangeAttack() {
        if (_bullet == null) { return; }
        if (PerfomingMeleeAttack) { return; }
        if (!_rangedAttack?.CanAttack ?? true) { return; }
        if (_overheat?.Overheating ?? true) { return; }

        _rangedAttack?.SetBullet(_bullet);
        _rangedAttack?.Attack(LookDirection);
        _onAttack?.Invoke(AttackType.RANGE);
        if (_overheat != null) { _overheat.Heat += _bullet.GetComponent<Bullet>()?.Overheat ?? 5; }
    }

    #endregion

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

    private void _SetMousePosition(Vector2 position) {
        if (_input.currentControlScheme == "Gamepad") {
            mousePosition = _camera?.Instance?.WorldToScreenPoint(transform.Position2D() + position) ?? Vector2.up;
        } else {
            mousePosition = position;
        }
        PlayerTurnover(LookDirection);
    }

    private void _SetMousePosition(InputAction.CallbackContext cc) {
        if (_input.currentControlScheme == "Gamepad") {
            if (cc.ReadValue<Vector2>() != Vector2.zero) {
                _lastDirection = cc.ReadValue<Vector2>().normalized;
            }
            _SetMousePosition(_lastDirection);
        } else {
            _SetMousePosition(cc.ReadValue<Vector2>());
        }
    }

    private void PlayerTurnover(Vector2 direction) {
        //Debug.Log(_spine.skeleton.Skin);
        if (_spine == null || _spine.skeleton == null) { return; }
        float targetScale = 1f;
        string skin = "Front";
        if (direction.x > 0) { targetScale = -1f; }
        if (direction.y > 0) { skin = "Back"; }

        if (targetScale != _spine.skeleton.ScaleX || skin != _spine.skeleton.Skin.Name) {
            _spine.skeleton.SetSkin(skin);
            _spine.skeleton.ScaleX = targetScale;

            _spine.Skeleton.SetSlotsToSetupPose();
            _spine.LateUpdate();
        }
    }

    private void _DashingAnimator(Vector2 vector) {
        _animator?.SetBool("Dashing", true);
    }

    private void _StopDashingAnimator() {
        _animator?.SetBool("Dashing", false);
    }

    private void _TogglePause(InputAction.CallbackContext cc) {
        if (_pauseUI.gameObject.activeSelf) {
            Unpause();
        } else {
            Pause();
        }
    }

    public void Pause() {
        Time.timeScale = 0f;
        _pauseUI.SetActive(true);
    }

    public void Unpause() {
        Time.timeScale = 1f;
        _pauseUI.SetActive(false);
    }

    public void SetBullet(GameObject bullet, GameObject chargedBullet) {
        if (bullet != null) {
            _bullet = bullet;
        }
        if (chargedBullet != null) {
            _chargeBullet = chargedBullet;
            _chargeRangedAttack?.SetBullet(_chargeBullet);
        }
    }

    public void EnableInput(InputType inputType, bool state) {
        switch (inputType) {
            case InputType.MOVEMENT:
                if (state) {
                    _controls.GamePlay.Enable();
                    _input?.actions.FindActionMap("GamePlay")?.Enable();
                } else {
                    _controls.GamePlay.Disable();
                    _input?.actions.FindActionMap("GamePlay")?.Disable();
                }
                break;
            case InputType.COMBAT:
                if (state) {
                    _controls.Battle.Enable();
                    _input?.actions.FindActionMap("Battle")?.Enable();
                } else {
                    _controls.Battle.Disable();
                    _input?.actions.FindActionMap("Battle")?.Disable();
                }
                break;
            case InputType.UI:
                if (state) {
                    _controls.UI.Enable();
                    _input?.actions.FindActionMap("UI")?.Enable();
                } else {
                    _controls.UI.Disable();
                    _input?.actions.FindActionMap("UI")?.Disable();
                }
                break;
        }
    }

    public void HealingParticule() {
        _healingPart.gameObject.SetActive(true);
        _healingPart.Play();
    }
}
