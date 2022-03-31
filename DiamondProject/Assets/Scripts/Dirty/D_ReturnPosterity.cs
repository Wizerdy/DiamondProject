using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_ReturnPosterity : MonoBehaviour {
    [SerializeField] PosterityObject _posterity;

    public int ReturnDeathCount() {
        if (_posterity == null) { return 0; }
        return _posterity.deathCount;
    }

    public void SetMaxLifeModifier(int amount) {
        if (_posterity == null) { return; }
        _posterity.maxLifeModifier = amount;
    }
}
