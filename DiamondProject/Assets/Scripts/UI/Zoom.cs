using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    private Vector3 scale = new Vector3();

    public void ZoomIn(GameObject objToZoom, float amountToScale, float speed) {
        StartCoroutine(Zooming(objToZoom, amountToScale, speed));
    }

    public void ZoomOut(GameObject objToZoom, float amountToScale, float speed) {
        StartCoroutine(UnZoom(objToZoom, amountToScale, speed));
    }

    IEnumerator Zooming(GameObject objToZoom, float amountToScale, float speed) {
        scale = objToZoom.transform.localScale;
        float maxScale = scale.x + amountToScale;
        while (scale.x < maxScale) {
            scale.x += Time.deltaTime * speed;
            scale.y += Time.deltaTime * speed;


            objToZoom.transform.localScale = scale;
            yield return null;
        }

        yield return null;
    }

    IEnumerator UnZoom(GameObject objToZoom, float amountToScale, float speed) {
        scale = objToZoom.transform.localScale;
        float maxScale = scale.x - amountToScale;
        while (scale.x < maxScale) {
            scale.x -= Time.deltaTime * speed;
            scale.y -= Time.deltaTime * speed;


            objToZoom.transform.localScale = scale;
            yield return null;
        }

        yield return null;
    }
}
