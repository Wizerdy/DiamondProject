using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ToolsBoxEngine;

public class ChangeBossOnShape : MonoBehaviour {
    [Header("References")]
    [SerializeField] BossShapeSystem _shapeSystem;
    [SerializeField] IMeetARealBoss _boss;
    [Header("System")]
    [SerializeField] float _timeBeforeChange = 0f;
    [SerializeField] UnityEvent<BossShape> _onSpriteChange;

    public event UnityAction<BossShape> OnSpriteChange { add => _onSpriteChange.AddListener(value); remove => _onSpriteChange.RemoveListener(value); }

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
        _boss.SetSkin(shape.Type);
        _onSpriteChange?.Invoke(shape);
        //if (_boss == null) { return; }
        //if (shape.ColorSwap) {
        //    _boss.ColorSwap(shape.Red, shape.Green, shape.Blue);
        //} else {
        //    _boss.Sprite = shape.Sprite;
        //    _boss.Animator = shape.Animator;
        //}
    }
}
