using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ToolsBoxEngine;

public class Lia : MonoBehaviour {
    [SerializeField] Health _health;
    [SerializeField] IMeetARealBoss _boss;
    [SerializeField] BossShapeSystem _bossShapeSystem;
    [SerializeField] ShapeLibrary _shapeLibrary;
    [SerializeField] LiaAttack _attacks;

    [Header("Values")]
    [SerializeField] float _moveCenterTime = 2f;
    [SerializeField] Shape _initialShape = Shape.NEUTRAL;

    [Header("Triggers")]
    [SerializeField] Trigger spring;
    [SerializeField] Trigger summer;
    [SerializeField] Trigger fall;
    [SerializeField] Trigger winter;

    [SerializeField, HideInInspector] UnityEvent _onCentering;

    List<Shape> _beatenShape = new List<Shape>();
    bool _morphing = false;

    public event UnityAction OnCentering { add => _onCentering.AddListener(value); remove => _onCentering.RemoveListener(value); }

    private void Start() {
        _health.CanTakeDamage = false;
        StartCoroutine(
            Tools.Delay(
                (Shape shape, bool movetocenter) => { NewForm(shape, movetocenter); _health.CanTakeDamage = true; },
                _initialShape,
                false,
                StartCoroutine(WaitForActing())
            )
        );
    }

    IEnumerator WaitForActing() {
        while (!_attacks.CanAct) {
            yield return new WaitForEndOfFrame();
        }
    }

    private void Update() {
        if (_morphing) { return; }
        if (!_beatenShape.Contains(Shape.FALL) && fall.IsTrigger()) {
            NewForm(Shape.FALL);
        }
        if (!_beatenShape.Contains(Shape.WINTER) && winter.IsTrigger()) {
            NewForm(Shape.WINTER);
        }
    }

    public void NewForm(Shape shape, bool moveToCenter = true) {
        _attacks.StopBehaviour();
        _attacks.ClearAttacks();
        if (!moveToCenter) {
            ChangeForm(shape);
            return;
        }
        _boss.MoveTo(Vector2.zero, _moveCenterTime);
        StartCoroutine(Tools.Delay(ChangeForm, shape, _moveCenterTime));
        _health.CanTakeDamage = false;
        StartCoroutine(Tools.Delay(() => _health.CanTakeDamage = true, _moveCenterTime));
        _morphing = true;
        StartCoroutine(Tools.Delay(() => _morphing = false, _moveCenterTime));
        _onCentering?.Invoke();
    }

    public void ChangeForm(Shape shape) {
        _health.CurrentHealth = _health.MaxHealth;
        _bossShapeSystem.ChangeShape(_shapeLibrary.GetBossShape(shape));
        if (shape != Shape.NEUTRAL) {
            _beatenShape.Add(shape);
        }
    }

    public void ComputeDeath() {
        if (_bossShapeSystem.Shape.Type != Shape.NEUTRAL) {
            NewForm(Shape.NEUTRAL);
        } else {
            _boss.Death();
        }
    }
}
