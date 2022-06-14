using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMove : MonoBehaviour {
    [SerializeField] float _speed;
    [SerializeField] float _distMax;
    [SerializeField] float _time;
    [SerializeField] AnimationCurve _smooth;
    float _dest = 0;
    Vector3 origin;
    Vector3 firstDestination;
    Vector3 destination;
    private void Start() {
        origin = transform.position;
        firstDestination = origin;
        destination = origin + new Vector3(Random.Range(-_distMax, _distMax), Random.Range(-_distMax, _distMax), 0);
    }
    public void Update() {
        _time = 1/(Vector3.Distance(firstDestination, destination) / _speed);
        _dest += Time.deltaTime * _time * _smooth.Evaluate(_dest);
        transform.position = Vector3.Lerp(firstDestination, destination, _dest);
        if (_dest > 1) {
            firstDestination = destination;
            destination = origin + new Vector3(Random.Range(-_distMax, _distMax), Random.Range(-_distMax, _distMax), 0);
            _dest = 0;
        }
    }
}
