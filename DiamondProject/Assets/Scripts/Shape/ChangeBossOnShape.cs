using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class ChangeBossOnShape : MonoBehaviour {
    [Header("References")]
    [SerializeField] BossShapeSystem _shapeSystem;
    [SerializeField] IMeetARealBoss _boss;
    [Header("System")]
    [SerializeField] float _timeBeforeChange = 0f;

    private void Reset() {
        _shapeSystem = GetComponent<BossShapeSystem>();
        _boss = GetComponent<IMeetARealBoss>();
    }

    void Awake() {
        _shapeSystem.OnEnterShape += _OnEnterShape;
    }

    void OnDestroy() {
        _shapeSystem.OnEnterShape -= _OnEnterShape;
    }

    void _OnEnterShape(BossShape shape) {
        if (_timeBeforeChange <= 0f) { ChangeShape(shape); return; }
        StartCoroutine(Tools.Delay(ChangeShape, shape, _timeBeforeChange));
    }

    public void ChangeShape(BossShape shape) {
        if (_boss == null) { return; }
        if (shape.ColorSwap) {
            _boss.ColorSwap(shape.Red, shape.Green, shape.Blue);
        } else {
            _boss.Sprite = shape.Sprite;
            _boss.Animator = shape.Animator;
        }
    }
}
