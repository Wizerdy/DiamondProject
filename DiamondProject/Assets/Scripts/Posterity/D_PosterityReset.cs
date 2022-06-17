using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_PosterityReset : MonoBehaviour {
    [SerializeField] PosterityObject _posterity;
    [SerializeField] VNTrigger vnTrigger;

    static D_PosterityReset _instance;

    void Start() {
        if (_instance != null) { Destroy(gameObject); return; }

        _instance = this;
        DontDestroyOnLoad(this);
        if (_posterity == null) { return; }
        if (_posterity.resetOnStart) {
            _posterity.ResetValues();
        }
        if (vnTrigger == null) { return; }
        if (vnTrigger.ResetOnPlay) {
            vnTrigger.ResetValue();
        }
    }
}
