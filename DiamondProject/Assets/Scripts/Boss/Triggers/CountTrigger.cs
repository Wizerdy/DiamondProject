using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class CountTrigger : Trigger {
    [SerializeField] Comparison _comparison = Comparison.EQUAL;
    [SerializeField] int _target = 3;
    [SerializeField] bool _resetByTime = false;
    [SerializeField] float _resetTime = 3f;

    float _timer = 0;
    int _count = 0;

    public void Update() {
        if (!_resetByTime) { return; }
        if (_timer >= _resetTime) {
            Clear();
        } else {
            _timer += Time.deltaTime;
        }
    }

    public override bool IsSelfTrigger() {
        return _comparison.Compare(_count, _target);
    }

    public void Increment() {
        _timer = 0f;
        ++_count;
    }

    public void Decrement() {
        --_count;
    }

    public void Clear() {
        _count = 0;
    }
}
