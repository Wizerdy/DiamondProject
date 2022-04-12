using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_ParticleShapeFollowSize : MonoBehaviour {
    [SerializeField] ParticleSystem _target;
    [SerializeField] ParticleSystem _self;

    private void Reset() {
        _target = GetComponent<ParticleSystem>();
        _self = GetComponent<ParticleSystem>();
    }

    void Start() {

    }

    void Update() {
        ParticleSystem.ShapeModule circle = _self.shape;
        //circle.radius = _target[0];
    }
}
