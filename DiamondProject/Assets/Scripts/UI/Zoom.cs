using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    [SerializeField] private GameObject image;
    private bool _isDragging;
    private float _currentScale;
    public float minScale, maxScale; // minScale and maxScale are limits of scaling
    private float _temp;
    private float _scalingRate = 2;
    private Vector3 scale = new Vector3();
    public float speed = 1;
    public void ZoomIn() {
        StartCoroutine(Zooming());
    }
    IEnumerator Zooming() {
        //image.transform.localScale = new Vector2(_currentScale, _currentScale);
        while (scale.x < maxScale) {
            scale = image.transform.localScale;
            scale.x += Time.deltaTime * speed;
            scale.y += Time.deltaTime * speed;


            image.transform.localScale = scale;
        }

        //float distance = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
        //if (_temp > distance) {
        //    if (_currentScale < minScale)
        //        yield return null;
        //    _currentScale -= (Time.deltaTime) * _scalingRate;
        //} else if (_temp < distance) {
        //    if (_currentScale > maxScale)
        //        yield return null;
        //    _currentScale += (Time.deltaTime) * _scalingRate;
        //}

        //_temp = distance;
        yield return null;
    }
}
