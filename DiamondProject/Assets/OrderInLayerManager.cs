using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class OrderInLayerManager : MonoBehaviour {
    [SerializeField] bool Offset = false;
    [SerializeField] Transform OffsetTransform;
    Renderer[] _renderers;
    Dictionary<Renderer, int> _offsetByRenderer = new Dictionary<Renderer, int>();

    private void Start() {
        _renderers = GetComponents<Renderer>();
        for (int i = 0; i < _renderers.Length; i++) {
            _offsetByRenderer.Add(_renderers[i], _renderers[i].GetComponent<OrderInLayerOffset>()?.LayerOffset ?? 0);
        }
    }

    private void Update() {
        if (!Offset) {
            for (int i = 0; i < _renderers.Length; i++) {
                _renderers[i].sortingOrder = (int)(_renderers[i].gameObject.transform.position.y * -100) + _offsetByRenderer[_renderers[i]];
            }
        } else {
            for (int i = 0; i < _renderers.Length; i++) {
                _renderers[i].sortingOrder = (int)(OffsetTransform.position.y * -100) + _offsetByRenderer[_renderers[i]];
            }
        }
    }
}
