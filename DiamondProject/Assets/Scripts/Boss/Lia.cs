using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Update() {
        if (spring.IsTrigger()) {
            NewForm(Shape.SPRING);
        }
        if (summer.IsTrigger()) {
            NewForm(Shape.SUMMER);
        }
        if (fall.IsTrigger()) {
            NewForm(Shape.FALL);
        }
        if (winter.IsTrigger()) {
            NewForm(Shape.WINTER);
        }
    }

    public void NewForm(Shape shape) {
        _bossShapeSystem.ChangeShape(_shapeLibrary.GetBossShape(shape));
        _health.CurrentHealth = _health.MaxHealth;
    }

    public void ComputeDeath() {
        if (_bossShapeSystem.Shape.Type != Shape.NEUTRAL) {
            NewForm(Shape.NEUTRAL);
        } else {
            _boss.Death();
        }
    }
}
