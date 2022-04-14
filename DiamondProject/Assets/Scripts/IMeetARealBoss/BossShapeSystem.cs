using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossShapeSystem : MonoBehaviour {
    [SerializeField] BossShape _initialForm;
    [SerializeField] BossShape _currentShape;
    [Space]
    [SerializeField] UnityEvent<BossShape> _onEnterShape;
    [SerializeField] UnityEvent<BossShape> _onExitShape;

    public BossShape Shape => _currentShape;

    #region Events

    public event UnityAction<BossShape> OnEnterShape { add => _onEnterShape.AddListener(value); remove => _onEnterShape.RemoveListener(value); }
    public event UnityAction<BossShape> OnExitShape { add => _onExitShape.AddListener(value); remove => _onExitShape.RemoveListener(value); }

    #endregion

    void Start() {
        ChangeShape(_initialForm);
    }

    public void ChangeShape(BossShape shape) {
        if (_currentShape != null) { _onExitShape?.Invoke(_currentShape); }
        _currentShape = shape;
        if (_currentShape != null) { _onEnterShape?.Invoke(_currentShape); }
    }
}
