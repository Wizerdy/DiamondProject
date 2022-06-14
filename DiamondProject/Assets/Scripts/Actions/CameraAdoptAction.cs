using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdoptAction : MonoBehaviour {
    [SerializeField] SimpleCameraEngine _camera = null;
    [SerializeField] Camera _target = null;
    [SerializeField] AnimationCurve _curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    [SerializeField] float _time = 1f;

    public void Execute() {
        _camera.Adopt(_target, _time, _curve);
    }
}
