using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionZone : MonoBehaviour {
    [SerializeField] string _objectTag = "Player";
    [SerializeField] UnityEvent OnEnter;
    [SerializeField] UnityEvent OnExit;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(_objectTag)) {
            OnEnter.Invoke();
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(_objectTag)) {
            OnExit.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag(_objectTag)) {
            OnEnter.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag(_objectTag)) {
            OnExit.Invoke();
        }
    }
}
