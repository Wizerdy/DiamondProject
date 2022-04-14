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
    }
}
