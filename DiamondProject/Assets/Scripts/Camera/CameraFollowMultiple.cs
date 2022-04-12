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

    private float Height => _camera.orthographicSize;
    private float Width => _camera.orthographicSize * _camera.aspect;

    void Start() {
        if (_camera == null) { throw new System.NullReferenceException(); }
    }

    void Update() {
        Vector2 barycenter = Barycenter(_targets);
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
    }

    Vector2 Barycenter(List<TransformDelta> targets) {
        Vector2 barycenter = Vector2.zero;
        for (int i = 0; i < targets.Count; i++) {
            barycenter += targets[i].transform.Position2D();
        }
        barycenter /= targets.Count;
        return barycenter;
    }
}
