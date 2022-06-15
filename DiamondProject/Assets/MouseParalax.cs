using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseParalax : MonoBehaviour {
    [SerializeField] GameObject _layer1;
    [SerializeField] GameObject _layer2;
    [SerializeField] GameObject _layer3;
    [SerializeField] float _poidsMin;
    [SerializeField] float _poidsMax;
    [SerializeField] float[] _paralaxPowers = new float[0];

    List<GameObject> _paralaxImagesLayer1 = new List<GameObject>();
    List<GameObject> _paralaxImagesLayer2 = new List<GameObject>();
    List<GameObject> _paralaxImagesLayer3 = new List<GameObject>();

    float[] _paralaxPowersLayer1;
    float[] _paralaxPowersLayer2;
    float[] _paralaxPowersLayer3;

    Vector3[] _imagesOriginLayer1;
    Vector3[] _imagesOriginLayer2;
    Vector3[] _imagesOriginLayer3;

    private void Start() {
        for (int i = 0; i < _layer1.transform.childCount; i++) {
            _paralaxImagesLayer1.Add(_layer1.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < _layer2.transform.childCount; i++) {
            _paralaxImagesLayer2.Add(_layer2.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < _layer3.transform.childCount; i++) {
            _paralaxImagesLayer3.Add(_layer3.transform.GetChild(i).gameObject);
        }

        _imagesOriginLayer1 = new Vector3[_paralaxImagesLayer1.Count];
        _paralaxPowersLayer1 = new float[_paralaxImagesLayer1.Count]; 
        _imagesOriginLayer2 = new Vector3[_paralaxImagesLayer2.Count];
        _paralaxPowersLayer2 = new float[_paralaxImagesLayer2.Count]; 
        _imagesOriginLayer3 = new Vector3[_paralaxImagesLayer3.Count];
        _paralaxPowersLayer3 = new float[_paralaxImagesLayer3.Count]; 

        for (int i = 0; i < _paralaxImagesLayer1.Count; i++) {
            _imagesOriginLayer1[i] = _paralaxImagesLayer1[i].transform.localPosition;
            _paralaxPowersLayer1[i] = Random.Range(_poidsMin, _poidsMax);
        }
        for (int i = 0; i < _paralaxImagesLayer2.Count; i++) {
            _imagesOriginLayer2[i] = _paralaxImagesLayer2[i].transform.localPosition;
            _paralaxPowersLayer2[i] = Random.Range(_poidsMin, _poidsMax);
        }
        for (int i = 0; i < _paralaxImagesLayer3.Count; i++) {
            _imagesOriginLayer3[i] = _paralaxImagesLayer3[i].transform.localPosition;
            _paralaxPowersLayer3[i] = Random.Range(_poidsMin, _poidsMax);
        }
    }

    private void FixedUpdate() {
        Vector3 mousePositionVecNorm = new Vector3(Screen.width /2, Screen.height /2, 0) - Input.mousePosition;
        for (int i = 0; i < _paralaxImagesLayer1.Count; i++) {
            _paralaxImagesLayer1[i].transform.localPosition = mousePositionVecNorm * _paralaxPowers[0] * _paralaxPowersLayer1[i] + _imagesOriginLayer1[i];
        }
        for (int i = 0; i < _paralaxImagesLayer2.Count; i++) {
            _paralaxImagesLayer2[i].transform.localPosition = mousePositionVecNorm * _paralaxPowers[1] * _paralaxPowersLayer2[i] + _imagesOriginLayer2[i];
        }
        for (int i = 0; i < _paralaxImagesLayer3.Count; i++) {
            _paralaxImagesLayer3[i].transform.localPosition = mousePositionVecNorm * _paralaxPowers[2] * _paralaxPowersLayer3[i] + _imagesOriginLayer3[i];
        }
    }
}
