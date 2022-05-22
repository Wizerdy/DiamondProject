using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lia : MonoBehaviour {
    [SerializeField] Health _health;
    [SerializeField] IMeetARealBoss _boss;
    [SerializeField] BossShapeSystem _bossShapeSystem;
    [SerializeField] ShapeLibrary _shapeLibrary;

    [Header("Triggers")]
    [SerializeField] Trigger spring;
    [SerializeField] Trigger summer;
    [SerializeField] Trigger fall;
    [SerializeField] Trigger winter;

    List<Shape> _beatenShape = new List<Shape>();

    private void Update() {
        if (!_beatenShape.Contains(Shape.SPRING) && spring.IsTrigger()) {
            NewForm(Shape.SPRING);
        }
        if (!_beatenShape.Contains(Shape.SUMMER) && summer.IsTrigger()) {
            NewForm(Shape.SUMMER);
        }
        if (!_beatenShape.Contains(Shape.FALL) && fall.IsTrigger()) {
            NewForm(Shape.FALL);
        }
        if (!_beatenShape.Contains(Shape.WINTER) && winter.IsTrigger()) {
            NewForm(Shape.WINTER);
        }
    }

    public void NewForm(Shape shape) {
        Debug.Log("G RAISON");
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
