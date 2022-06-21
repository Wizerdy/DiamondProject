using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimatorTrigger : MonoBehaviour {
    [SerializeField] Animator _animator = null;
    [SerializeField] string _trigger = "";
    [SerializeField] Vector2Int _bounds = Vector2Int.zero;

    void Start() {
        _animator?.SetInteger(_trigger, Random.Range(_bounds.x, _bounds.y));
    }
}
