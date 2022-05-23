using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Shadow : MonoBehaviour {
    GameObject _shadow;
    SpriteRenderer _sr;
    SpriteRenderer _shadowSR;
    [SerializeField] Color _shadowColor;
    [SerializeField] Transform _light;
    [SerializeField] float _height;
    private void Start() {
        _sr = GetComponent<SpriteRenderer>();
        _shadow = new GameObject(gameObject.name + " Shadow");
        _shadow.transform.parent = transform;
        _shadowSR = _shadow.AddComponent<SpriteRenderer>();
        _shadowSR.sprite = _sr.sprite;
        _shadowSR.color = _shadowColor;
        _shadowSR.sortingOrder = _sr.sortingOrder - 1;
    }

    private void Update() {
        UpdateShadow();
    }

    void UpdateShadow() {
        _shadow.transform.position = (transform.position - _light.transform.position).normalized * _height + transform.position;
    }
}
