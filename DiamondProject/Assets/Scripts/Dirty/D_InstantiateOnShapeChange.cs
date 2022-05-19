using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class D_InstantiateOnShapeChange : MonoBehaviour {
    [System.Serializable]
    enum EventType { ENTER, EXIT }
    [SerializeField] BossShapeSystemReference _shapeSystem;
    [SerializeField] List<GameObject> _toInstantiate;
    [SerializeField] List<Shape> _shapes;
    [SerializeField] UnityEvent _onShapeChange;
    [SerializeField] EventType _eventType = EventType.ENTER;
    public event UnityAction OnShapeChange { add => _onShapeChange.AddListener(value); remove => _onShapeChange.RemoveListener(value); }

    void Awake() {
        switch (_eventType) {
            case EventType.ENTER:
                _shapeSystem.Instance.OnEnterShape += _Instantiate;
                break;
            case EventType.EXIT:
                _shapeSystem.Instance.OnExitShape += _Instantiate;
                break;
            default:
                break;
        }
    }

    void _Instantiate(BossShape shape) {
        if (!_shapes.Contains(shape.Type)) { return; }
        for (int i = 0; i < _toInstantiate.Count; i++) {
            Instantiate(_toInstantiate[i], transform);
        }
        _onShapeChange?.Invoke();
    }
}
