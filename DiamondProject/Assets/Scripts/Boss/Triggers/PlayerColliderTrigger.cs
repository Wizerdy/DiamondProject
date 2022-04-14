using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderTrigger : Trigger {
    [SerializeField] bool _isInside;
    [SerializeField] bool _shouldEnter;
    [SerializeField] float radius;
    [SerializeField] float _timeBeforeTrigger;
    [SerializeField] float _timeSince;
    [SerializeField] Coroutine _coroutine;

    private void Start() {
        if (GetComponent<Collider2D>() == null) {
            CircleCollider2D cc = gameObject.AddComponent<CircleCollider2D>();
            cc.radius = radius;
        }
    }
    public override bool IsSelfTrigger() {
        if (_isInside == _shouldEnter && _timeSince >= _timeBeforeTrigger) {
            return true;
        } else {
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            _isInside = true;
            if (_shouldEnter) {
                StopCoroutine(_coroutine);
                _coroutine = StartCoroutine(Timer());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            {
                _isInside = false;
                if (!_shouldEnter) {
                    StopCoroutine(_coroutine);
                    _coroutine = StartCoroutine(Timer());
                }
            }
        }
    }

    IEnumerator Timer() {
        _timeSince = 0;
        while (_isInside) {
            _timeSince += Time.deltaTime;
            yield return null;
        }
    }






}
