using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowingTransform : MonoBehaviour {
    [SerializeField] Transform _target;
    [SerializeField] TransformReference _light;
    [SerializeField] float _height = 1f;

    void Update() {
        transform.position = (_target.position - _light.Instance.transform.position).normalized * _height + _target.transform.position;
    }
}
