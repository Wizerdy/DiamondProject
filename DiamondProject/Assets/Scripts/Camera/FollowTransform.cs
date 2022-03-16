using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class FollowTransform : MonoBehaviour {
    [SerializeField] Transform target;
    [SerializeField] Transform self;
    [SerializeField] Axis[] concernedAxis = { Axis.X, Axis.Y, Axis.Z };

    private void Reset() {
        self = transform;
    }

    void Update() {
        self.position = self.position.Override(target.position, concernedAxis);
    }
}
