using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class CameraFollowMultiple : MonoBehaviour {
    [System.Serializable]
    private struct TransformDelta {
        public Transform transform;
        public float delta;
    }

    [SerializeField] Camera _camera;
    [SerializeField] float _minOrthographicSize = 5f;
    [SerializeField] List<TransformDelta> _targets = new List<TransformDelta>();
    [SerializeField] protected bool _respectBoundaries;
    [SerializeField] protected Transform _topBoundaries;
    [SerializeField] protected Transform _botBoundaries;
    [SerializeField] protected Transform _rightBoundaries;
    [SerializeField] protected Transform _leftBoundaries;

    public float CameraSize => _camera.orthographicSize;
    public float CameraRatio => Height / Width;
    private float Height => _camera.orthographicSize;
    private float Width => _camera.orthographicSize * _camera.aspect;

    void Start() {
        if (_camera == null) { throw new System.NullReferenceException(); }
    }

    void Update() {
        Vector2 barycenter = Barycenter(_targets);
        Vector3 savePos = transform.position;
        transform.position = transform.position.Override(barycenter, Axis.X, Axis.Y);
        Vector2 maxDistance = new Vector2(0f, 0f);
        for (int i = 0; i < _targets.Count; i++) {
            Vector2 distance = (_targets[i].transform.Position2D() - barycenter).Abs();
            if (distance.x + _targets[i].delta > maxDistance.x) {
                maxDistance.x = distance.x + _targets[i].delta;
            }
            if (distance.y + _targets[i].delta > maxDistance.y) {
                maxDistance.y = distance.y + _targets[i].delta;
            }
        }

        float targetWidth = maxDistance.x / _camera.aspect;
        float targetHeight = maxDistance.y;
        float targetOrthoSize = Mathf.Max(targetWidth, targetHeight);
        _camera.orthographicSize = Mathf.Max(targetOrthoSize, _minOrthographicSize);
        if (_respectBoundaries && IsOutBoundaries(transform.position)) {
            float topSize = Mathf.Abs(transform.position.y - _topBoundaries.position.y);
            float botSize = Mathf.Abs(transform.position.y - _botBoundaries.position.y);
            float rightSize = Mathf.Abs(transform.position.x - _rightBoundaries.position.x) * CameraRatio;
            float leftSize = Mathf.Abs(transform.position.x - _leftBoundaries.position.x) * CameraRatio;
            _camera.orthographicSize = Mathf.Min(topSize, botSize, rightSize, leftSize);
        }
    }

    Vector2 Barycenter(List<TransformDelta> targets) {
        Vector2 barycenter = Vector2.zero;
        for (int i = 0; i < targets.Count; i++) {
            barycenter += targets[i].transform.Position2D();
        }
        barycenter /= targets.Count;
        return barycenter;
    }

    bool IsOutBoundaries(Vector3 position) {
        if (_topBoundaries.transform.position.y <= position.y + CameraSize)
            return true;
        if (_botBoundaries.transform.position.y >= position.y - CameraSize)
            return true;
        if (_rightBoundaries.transform.position.x <= position.x + CameraSize / CameraRatio)
            return true;
        if (_leftBoundaries.transform.position.x >= position.x - CameraSize / CameraRatio)
            return true;
        return false;
    }
}
