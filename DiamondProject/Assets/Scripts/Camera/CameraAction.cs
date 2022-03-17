using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public abstract class CameraAction : MonoBehaviour {
    [SerializeField] protected CameraEngine2D _movements;
    [SerializeField] float _speed = 5f;
    [SerializeField] protected Axis[] _concernedAxis = { Axis.X, Axis.Y, Axis.Z };

    private void Reset() {
        _movements = GetComponent<CameraEngine2D>();
        OnReset();
    }

    protected void OnReset() { }

    public void Move(Vector3 localPosition) {
        float time = (localPosition - _movements.CameraLocalPosition).magnitude / _speed;
        if (time > 0.11f || time < 0.1f) {
            Tools.Print(time);
        }
        _movements.Move(localPosition, time, _concernedAxis);
    }
}