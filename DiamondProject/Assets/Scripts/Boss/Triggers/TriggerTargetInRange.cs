using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTargetInRange : Trigger {
    [Header("Datas")]
    [SerializeField] Reference<Collider2D> _target;
    bool _isTrigger = false;

    public override bool IsSelfTrigger() {
        return _isTrigger;
    }

    #region Unity Callback

    private void OnCollisionEnter2D(Collision2D collision) {
        VerifyCollider(collision.collider, true);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        VerifyCollider(collision, true);
    }

    private void OnCollisionExit2D(Collision2D collision) {
        VerifyCollider(collision.collider, false);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        VerifyCollider(collision, false);
    }

    #endregion

    void VerifyCollider(Collider2D collider, bool state) {
        if (collider == _target) {
            _isTrigger = state;
        }
    }
}
