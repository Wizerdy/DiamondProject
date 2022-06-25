using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectUIOnEnable : MonoBehaviour {
    //[SerializeField] GameObject _target;
    [SerializeField] Selectable _target;

    private void OnEnable() {
        if (_target == null) { return; }
        Select(_target);
    }

    public void Select(Selectable obj) {
        Debug.Log("Selected " + obj.name);
        obj.Select();
    }
}
