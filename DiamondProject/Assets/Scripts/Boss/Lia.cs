using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lia : MonoBehaviour {
    BossShapeSystem _bossShapeSystem;
    ShapeLibrary _shapeLibrary;
    public Trigger spring;
    public Trigger summer;
    public Trigger fall;
    public Trigger winter;
    public Trigger unspring;
    public Trigger unsummer;
    public Trigger unfall;
    public Trigger unwinter;

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
        if (unspring.IsTrigger() || unsummer.IsTrigger() || unfall.IsTrigger() || unwinter.IsTrigger()) {
            NewForm(Shape.NEUTRAL);
        }
    }


    public void NewForm(Shape shape) {
        _bossShapeSystem.ChangeShape(_shapeLibrary.GetBossShape(shape));
    }
}
