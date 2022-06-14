using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOnShapeChange : MonoBehaviour {
    [SerializeField] Lia _lia;
    [SerializeField] IMeetARealBoss _boss;

    void Start() {
        _lia.OnCentering += _ShapeChange;
    }

    private void OnDestroy() {
        _lia.OnCentering -= _ShapeChange;
    }

    private void _ShapeChange() {
        _boss.SetAnimatorBool("Spike", false);
        _boss.SetAnimatorBool("Sleeping", false);
        _boss.SetAnimatorBool("Thunder", false);
        _boss.SetAnimatorBool("Tree", false);
        _boss.SetAnimatorBool("Dash", false);
    }
}
