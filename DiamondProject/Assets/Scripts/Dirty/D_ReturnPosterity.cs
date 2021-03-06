using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_ReturnPosterity : MonoBehaviour {
    [SerializeField] PosterityObject _posterity;

    public int ReturnDeathCount() {
        if (_posterity == null) { return 0; }
        return _posterity.deathCount;
    }
    public int ReturnNumberOfTimeTalkedToNorna() {
        if (_posterity == null) { return 0; }
        return _posterity.nbTimeTalkedToNorna;
    }

    public int ReturnNumberOfCorpse() {
        if (_posterity == null) { return 0; }
        return _posterity.nbCorpse;
    }

    public void SetMaxLifeModifier(int amount) {
        if (_posterity == null) { return; }
        _posterity.maxLifeModifier = amount;
    }

    public bool DidAnotherBossTrigger() {
        if (_posterity == null) { return false; }
        return _posterity.gotToAnotherBoss;
    }

    public bool IsFirstTimeTalking() {
        if (_posterity == null) { return true; }
        return _posterity.firstTimeTalking;
    }

    public int NumberOfTriggerActivate() {
        if (_posterity == null) { return 0; }
        return _posterity.numberOfTriggerActivate;
    }

    public void NotFirstTimeTalking() {
        _posterity.firstTimeTalking = false;
    }

    public void ResetValueBeforeBoss() {
        _posterity.ResetValuesBeforeBoss();
    }
}
