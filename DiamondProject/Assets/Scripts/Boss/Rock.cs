using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ToolsBoxEngine;

public class Rock : MonoBehaviour {
    [HideInInspector]
    public Sprite sprite;
    private void Start() {
        sprite = GetComponent<SpriteRenderer>().sprite;
        ShowZone(true);
        StartCoroutine(Tools.Delay(ShowZone, false, 0.5f));
    }

    void ShowZone(bool show) {
        transform.GetChild(0).gameObject.SetActive(show);
    }
}
