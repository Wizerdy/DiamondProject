using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_ImageOnEnter : MonoBehaviour {
    [SerializeField] GameObject _imageToFade;
    [SerializeField] Reference<Transform> _transform;
    [SerializeField] string _tag;

    void Start() {
        _imageToFade.SetActive(false);
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(_tag)) {
            _imageToFade.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(_tag)) {
            _imageToFade.SetActive(true);
        }
    }
}
