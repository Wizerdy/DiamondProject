using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField] TransformReference _target;
    [SerializeField] Vector3 _offset = Vector3.zero;
    [SerializeField] Camera _camera;
    [Header("Bounds")]
    [SerializeField] bool _useBounds = false;
    [SerializeField] float _top;
    [SerializeField] float _right;
    [SerializeField] float _bot;
    [SerializeField] float _left;

    public float Height => _camera.orthographicSize;
    public float Width => _camera.orthographicSize * _camera.aspect;

    void Update() {
        //if (!_camera?.IsValid ?? true) { return; }
        if (!_target?.IsValid ?? true) { return; }

        Vector3 position = _target.Instance.position + _offset;
        if (_useBounds) {
            if (position.x + Width > _right) {
                position.x = _right - Width;
            } else if (position.x - Width < _left) {
                position.x = _left + Width;
            }

            if (position.y + Height > _top) {
                position.y = _top - Height;
            } else if (position.y - Height < _bot) {
                position.y = _bot + Height;
            }
        }
        _camera.transform.position = position.Override(_camera.transform.position.z, Axis.Z);
    }

    private void OnDrawGizmosSelected() {
        if (!_useBounds) { return; }
        Color color = Color.green;
        if (_right <= _left || _top <= _bot) { color = Color.red; }
        Gizmos.color = color;
        Vector3 size = new Vector3(_right - _left, _top - _bot, 0f).Abs();
        Vector3 center = new Vector3((_left < _right ? _left : _right) + size.x / 2f, (_bot < _top ? _bot : _top) + size.y / 2f, 0f);
        Gizmos.DrawWireCube(center, size);
    }
}
