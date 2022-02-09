using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rock : MonoBehaviour {
    [HideInInspector]
    public Sprite sprite;
    private void Start() {
        sprite = GetComponent<SpriteRenderer>().sprite;
        ShowZone(true);
        StartCoroutine(Delay(0.5f, ShowZone, false));
    }

    void ShowZone(bool show) {
        transform.GetChild(0).gameObject.SetActive(show);
    }

    IEnumerator Delay<T>(float delay, Action<T> method, T parameter) {
        yield return new WaitForSeconds(delay);
        method(parameter);
    }
}
