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

    [Header("Triggers")]
    [SerializeField] Trigger spring;
    [SerializeField] Trigger summer;
    [SerializeField] Trigger fall;
    [SerializeField] Trigger winter;

    List<Shape> _beatenShape = new List<Shape>();
    bool _morphing = false;

    private void Update() {
        if (_morphing) { return; }
        if (!_beatenShape.Contains(Shape.FALL) && fall.IsTrigger()) {
            NewForm(Shape.FALL);
        }
        if (!_beatenShape.Contains(Shape.WINTER) && winter.IsTrigger()) {
            NewForm(Shape.WINTER);
        }
    }

    public void NewForm(Shape shape) {
        _attacks.ClearAttacks();
        _boss.MoveTo(Vector2.zero, _moveCenterTime);
        StartCoroutine(Tools.Delay(ChangeForm, shape, _moveCenterTime));
        _health.CanTakeDamage = false;
        StartCoroutine(Tools.Delay(() => _health.CanTakeDamage = true, _moveCenterTime));
        _morphing = true;
        StartCoroutine(Tools.Delay(() => _morphing = false, _moveCenterTime));
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
