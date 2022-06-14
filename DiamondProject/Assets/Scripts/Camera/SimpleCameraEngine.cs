using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ToolsBoxEngine;

public class SimpleCameraEngine : MonoBehaviour {
    [SerializeField] Camera _parent;
    [SerializeField] float _defaultTimer = 2f;
    [SerializeField] AnimationCurve _defaultCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    Camera _camera;

    Coroutine _routine_MoveTo = null;
    Coroutine _routine_ZoomTo = null;

    private void Awake() {
        _camera = GetComponent<Camera>();
    }

    void Start() {
        Adopt(_parent, 0f);
    }

    public void Adopt(Camera camera) {
        Adopt(camera, _defaultTimer);
    }

    public void Adopt(Camera camera, float time, AnimationCurve curve = null) {
        transform.parent = camera.transform;
        _parent = camera;
        if (time <= 0f) {
            transform.localPosition = Vector2.zero;
            _camera.orthographicSize = camera.orthographicSize;
            return;
        }
        ZoomTo(camera.orthographicSize, time, curve);
        MoveTo(Vector2.zero, time, curve);
        _parent = camera;
    }

    private void MoveTo(Vector2 target, float time, AnimationCurve curve = null) {
        if (_routine_MoveTo != null) { StopCoroutine(_routine_MoveTo); }
        StartCoroutine(IMoveTo(target, time, curve));
    }

    private IEnumerator IMoveTo(Vector2 target, float time, AnimationCurve curve = null) {
        if (curve == null) { curve = _defaultCurve; }
        float timePassed = 0f;
        Vector2 startPosition = transform.localPosition;
        while (timePassed <= time) {
            yield return new WaitForEndOfFrame();
            timePassed += Time.deltaTime;
            float percentage = timePassed / time;
            Vector2 nextPosition = Vector2.Lerp(startPosition, target, curve.Evaluate(percentage));
            transform.localPosition = nextPosition;
        }
    }

    private void ZoomTo(float target, float time, AnimationCurve curve = null) {
        if (_routine_ZoomTo != null) { StopCoroutine(_routine_ZoomTo); }
        StartCoroutine(IZoomTo(target, time, curve));
    }

    private IEnumerator IZoomTo(float target, float time, AnimationCurve curve = null) {
        if (curve == null) { curve = _defaultCurve; }
        float timePassed = 0f;
        float startSize = _camera.orthographicSize;
        while (timePassed <= time) {
            yield return new WaitForEndOfFrame();
            timePassed += Time.deltaTime;
            float percentage = timePassed / time;
            float nextSize = Mathf.Lerp(startSize, target, curve.Evaluate(percentage));
            _camera.orthographicSize = nextSize;
        }
    }
}
