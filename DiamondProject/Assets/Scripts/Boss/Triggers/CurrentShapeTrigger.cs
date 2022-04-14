using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentShapeTrigger : Trigger {
    [SerializeField] BossShapeSystem _bossShapeSystem;
    [SerializeField] Shape _isShape;
    public override bool IsSelfTrigger() {
        if (_bossShapeSystem.Shape.Type == _isShape) {
            return true;
        }
        return false;
    }
}
