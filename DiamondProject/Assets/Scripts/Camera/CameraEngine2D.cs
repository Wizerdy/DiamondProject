using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class CameraEngine2D : MonoBehaviour {
    [System.Serializable]
    class AxisRoutine {
        public Coroutine routine;
        public List<Axis> axisPriority;

        public AxisRoutine(Coroutine routine, params Axis[] axisPriority) {
            this.routine = routine;
            this.axisPriority = new List<Axis>(axisPriority);
        }
    }

    [SerializeField] Camera _camera;
    [SerializeField] Camera _parent = null;
    [SerializeField] float _baseDepth = 0f;
    [SerializeField] float _baseCameraSize;

    Vector3 startPosition;

    #region Properties

    public Vector3 CameraLocalPosition => _camera.transform.localPosition;
    Vector3 LocPosition { get { return _camera.transform.localPosition; } set { _camera.transform.localPosition = value; } }
    Vector3 Position { get { return _camera.transform.position; } set { _camera.transform.position = value; } }

    #endregion

    List<AxisRoutine> axisRoutines = new List<AxisRoutine>();

    private void Reset() {
        _camera = GetComponent<Camera>();
        _baseCameraSize = _camera?.orthographicSize ?? 5f;
        _parent = _camera.transform.parent.GetComponent<Camera>();
    }

    private void Start() {
        ResetStartPosition();
        //Zoom(2f, 0.2f);
        //StartCoroutine(Tools.Delay<float, float, Vector3?>(Zoom, 0.2f, 5f, null, 0.2f));
    }

    private void Update() {
        if (_parent != null) {
            _camera.orthographicSize = _parent.orthographicSize;
        }
    }

    public void Zoom(float zoom, float time, Vector3? unzoomedPosition = null) {
        if (zoom == 0f) { Debug.LogError("NO."); return; }
        if (unzoomedPosition == null) { unzoomedPosition = _parent != null ? _parent.transform.position : startPosition; }

        float unzoomedZ = unzoomedPosition.Value.z;
        float newZ = Mathf.LerpUnclamped(_baseDepth, unzoomedZ, zoom);
        Vector3 destination = Vector3.zero.Override(newZ, Axis.Z);
        destination -= unzoomedPosition.Value;
        Move(destination, time, Axis.Z);
    }

    public void ChangeParent(Camera parent, float time) {
        this._parent = parent;
        ResetLocalPosition(time);
    }

    public void ResetLocalPosition(float time) {
        Move(Vector3.zero, time, Axis.X, Axis.Y);
    }

    public void ResetStartPosition() {
        startPosition = transform.localPosition;
    }

    public void Move(Vector3 locPos, float time, params Axis[] concernedAxis) {
        GetAxisPriority(concernedAxis);
        if (time <= 0f) { LocPosition = LocPosition.Override(locPos, concernedAxis); return; }

        Coroutine routine = StartCoroutine(IMove(locPos, time, axisRoutines.Count, concernedAxis));
        axisRoutines.Add(new AxisRoutine(routine, concernedAxis));

        IEnumerator IMove(Vector3 position, float time, int routineIndex, params Axis[] concernedAxis) {
            Vector3 startPosition = LocPosition;
            float timePassed = 0f;
            AxisRoutine routine = null;
            while (timePassed < time) {
                Vector3 pos = Vector3.Lerp(startPosition, position, timePassed / time);
                LocPosition = LocPosition.Override(pos, concernedAxis);
                if (concernedAxis.Contains(Axis.Z)) {
                    float worldDepth = (_parent != null ? _parent.transform.position.z : transform.position.z);
                    ComputeDepth(_baseDepth, worldDepth - this.startPosition.z);
                }
                yield return new WaitForEndOfFrame();
                if (routine == null) { routine = axisRoutines[routineIndex]; }
                if (routine != null) { concernedAxis = routine.axisPriority.ToArray(); }
                timePassed += Time.deltaTime;
            }
        }
    }

    public void ComputeDepth(float minDepth, float maxDepth) {
        Tools.Print(minDepth, maxDepth, Position.z);
        float percentage = Tools.InverseLerpUnclamped(minDepth, maxDepth, Position.z);
        _camera.orthographicSize = Mathf.LerpUnclamped(0f, _baseCameraSize, percentage);
    }

    private void GetAxisPriority(params Axis[] axis) {
        for (int i = 0; i < axisRoutines.Count; i++) {
            for (int j = 0; j < axis.Length; j++) {
                if (axisRoutines[i].axisPriority.Contains(axis[j])) {
                    axisRoutines[i].axisPriority.Remove(axis[j]); // On retire la priorité
                    if (axisRoutines[i].axisPriority.Count == 0) {
                        StopCoroutine(axisRoutines[i].routine);
                        axisRoutines.RemoveAt(i); // Il n'a plus de priorité
                    }
                }
            }
        }
    }
}
