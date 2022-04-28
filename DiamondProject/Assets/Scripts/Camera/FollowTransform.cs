using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class FollowTransform : CameraAction {
    [SerializeField] Reference<Transform> target;

    Vector3 _targetPosition = Vector3.zero;

    void Update() {
        //if (_targetPosition != target.Instance.position) {
            _targetPosition = target.Instance.position;
        //    Move(_targetPosition);
        //    Move(target.Instance.position);
        //}
    }

    //void Update() {
    //    _movements.transform.position = _movements.transform.position.Override(target.Instance.position, _concernedAxis);
    //}
 
}


