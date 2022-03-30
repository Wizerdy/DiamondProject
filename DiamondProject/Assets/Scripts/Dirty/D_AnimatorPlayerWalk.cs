using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_AnimatorPlayerWalk : MonoBehaviour {
    [SerializeField] EntityMovement _movement;
    [SerializeField] Animator _animator;

    void Start() {
        _movement.OnAcceleration += (Vector2) => _animator.SetBool("Walking", true);
        _movement.OnDeceleration += (Vector2) => _animator.SetBool("Walking", false);
    }

    void Update() {

    }
}
