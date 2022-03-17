using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayHere : MonoBehaviour {
    private Vector3 _startPos;

    void Start() {
        _startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update() {
        transform.localPosition = _startPos;
    }
}
