using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderInLayerOffset : MonoBehaviour {
    [SerializeField] int _layerOffset = -1;

    public int LayerOffset => _layerOffset;
}
