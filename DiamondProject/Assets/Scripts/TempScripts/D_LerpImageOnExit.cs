using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class D_LerpImageOnExit : MonoBehaviour {
    [SerializeField] Image _imageToFade;
    [SerializeField] Reference<Transform> _transform;

    void Start() {
        Color color = _imageToFade.color;
        color.a = 0f;
        _imageToFade.color = color;
    }

    private void OnCollisionExit2D(Collision2D collision) {
        //if () {

        //}

    }
}
