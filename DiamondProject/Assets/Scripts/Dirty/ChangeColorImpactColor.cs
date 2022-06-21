using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorImpactColor : MonoBehaviour {
    [SerializeField] BossShapeSystem _shapeSystem = null;
    [SerializeField] Color _neutral;
    [SerializeField] Color _fall;
    [SerializeField] Color _winter;

    private void Start() {
        _shapeSystem.OnEnterShape += ColorFromShape;
    }

    void ColorFromShape(BossShape shape) {
        switch (shape.Type) {
            case Shape.NEUTRAL:
                ChangeColor(_neutral);
                break;
            case Shape.FALL:
                ChangeColor(_fall);
                break;
            case Shape.WINTER:
                ChangeColor(_winter);
                break;
        }
    }

    void ChangeColor(Color color) {
        MakeImpactParticule._particleColor = color;
    }
}
