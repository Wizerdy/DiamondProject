using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(SpriteRenderer))]
public class Shadow : MonoBehaviour {
    GameObject _shadow;
    SpriteRenderer _sr;
    SpriteRenderer _shadowSR;
    [SerializeField] Color _shadowColor = new Color(0,0,0,0.5f);
    [SerializeField] TransformReference _light;
    [SerializeField] VisualEffect _effect;
    [SerializeField] float _height;

    private void Start() {
        _sr = GetComponent<SpriteRenderer>();
        _shadow = new GameObject(gameObject.name + " Shadow");
        _shadow.transform.parent = transform;
        _shadow.transform.localEulerAngles = Vector3.zero;
        _shadow.transform.localScale = Vector3.one;
        _shadowSR = _shadow.AddComponent<SpriteRenderer>();
        _shadowSR.sprite = _sr.sprite;
        _shadowSR.color = _shadowColor;
        _shadowSR.sortingOrder = _sr.sortingOrder - 1;
    }

    private void Update() {
        UpdateShadow();
    }

    void UpdateShadow() {
        if (_shadowSR.sprite != _sr.sprite) {
            _shadowSR.sprite = _sr.sprite;
        }
        _shadow.transform.position = (transform.position - _light.Instance.transform.position).normalized * _height + transform.position;
    }

    public void YesShadow() {
        _shadow.SetActive(true);
    }

    public void NoShadow() {
        _shadow.SetActive(false);
    }
}
