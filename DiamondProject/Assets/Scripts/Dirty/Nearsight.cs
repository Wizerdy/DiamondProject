using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nearsight : MonoBehaviour {
    [SerializeField] PosterityObject _posterity;
    [SerializeField] Reference<Transform> _target;
    [SerializeField] Material _material;

    void Start() {
        if (_posterity != null) {
            transform.GetChild(0).gameObject.SetActive(_posterity.nearSight);
        }
    }

    void Update() {
        if (_posterity != null) { transform.GetChild(0).gameObject.SetActive(_posterity.nearSight); }
        if (_target == null) { return; }
        _material.SetVector("_Center", _target.Instance.position);
    }
}
