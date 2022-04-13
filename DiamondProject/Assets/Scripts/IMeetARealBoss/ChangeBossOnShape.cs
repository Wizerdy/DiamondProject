using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBossOnShape : MonoBehaviour {
    [SerializeField] BossShapeSystem _shapeSystem;
    [SerializeField] IMeetARealBoss _boss;

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
        if (_boss == null) { return; }
        _boss.Sprite = shape.Sprite;
        _boss.Animator = shape.Animator;
    }
}
