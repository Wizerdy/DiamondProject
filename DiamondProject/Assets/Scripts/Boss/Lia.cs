using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MoreMountains.Feedbacks;
using ToolsBoxEngine;

public class Lia : MonoBehaviour {
    [SerializeField] Health _health;
    [SerializeField] IMeetARealBoss _boss;
    [SerializeField] BossShapeSystem _bossShapeSystem;
    [SerializeField] ShapeLibrary _shapeLibrary;
    [SerializeField] LiaAttack _attacks;
    [SerializeField] MMFeedbacks _deathFeedback;

    [Header("Values")]
    [SerializeField] float _moveCenterTime = 2f;
    [SerializeField] Shape _initialShape = Shape.NEUTRAL;

    [Header("Triggers")]
    [SerializeField] Trigger spring;
    [SerializeField] Trigger summer;
    [SerializeField] Trigger fall;
    [SerializeField] Trigger winter;

    [SerializeField] UnityEvent _onRealDeath;
    [SerializeField, HideInInspector] UnityEvent _onCentering;

    public event UnityAction OnRealDeath { add => _onRealDeath?.AddListener(value); remove => _onRealDeath?.RemoveListener(value); }

    int _neutralHealth = 1001;

    List<Shape> _beatenShape = new List<Shape>();
    bool _morphing = false;

    public event UnityAction OnCentering { add => _onCentering.AddListener(value); remove => _onCentering.RemoveListener(value); }

    private void Start() {
        _neutralHealth = _health.MaxHealth;
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

        if (_beatenShape.Contains(Shape.WINTER) && _beatenShape.Contains(Shape.FALL) && _health.CurrentHealth == 1 && _attacks.CanAct) {
            _attacks.CanAct = false;
            _boss.SetAnimatorTrigger("KO");
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
        if (_bossShapeSystem.Shape != null && _bossShapeSystem.Shape.Type != Shape.NEUTRAL) {
            _beatenShape.Add(_bossShapeSystem.Shape.Type);
        }
        if (shape == Shape.NEUTRAL) {
            _health.CurrentHealth = _neutralHealth;
        } else {
            _health.CurrentHealth = _health.MaxHealth;
        }
        _bossShapeSystem.ChangeShape(_shapeLibrary.GetBossShape(shape));
    }

    public void ComputeDeath() {
        //if (_beatenShape.Contains(_bossShapeSystem.Shape.Type)) { return; }
        if (_morphing) { return; }
        if (_bossShapeSystem.Shape.Type != Shape.NEUTRAL) {
            _neutralHealth -= 500;
            NewForm(Shape.NEUTRAL);
        } else {
            EndGame();
        }
    }

    public void EndGame() {
        _deathFeedback?.PlayFeedbacks();
        _onRealDeath?.Invoke();
    }
}
