using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class D_InstantiateOnShapeChange : MonoBehaviour {
    [SerializeField] BossShapeSystemReference _shapeSystem;
    [SerializeField] List<GameObject> _toInstantiate;
    [SerializeField] List<Shape> _shapes;
    [SerializeField] UnityEvent _onShapeChange;
    public event UnityAction OnShapeChange { add => _onShapeChange.AddListener(value); remove => _onShapeChange.RemoveListener(value); }

    void Awake() {
        _shapeSystem.Instance.OnEnterShape += _Instantiate;
    }

    void _Instantiate(BossShape shape) {
        if (!_shapes.Contains(shape.Type)) { return; }
        for (int i = 0; i < _toInstantiate.Count; i++) {
            Instantiate(_toInstantiate[i], transform);
        }
        _onShapeChange?.Invoke();
    }
}
