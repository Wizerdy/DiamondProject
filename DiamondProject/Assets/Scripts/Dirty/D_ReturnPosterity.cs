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

    public int ReturnNumberOfTimeTalkedToFairy() {
        if (_posterity == null) { return 0; }
        return _posterity.numberOfTimeTalkingToFairy;
    }

    public void NotFirstTimeTalking() {
        _posterity.firstTimeTalking = false;
    }

    public void IncrementeFairyDialogue() {
        ++_posterity.numberOfTimeTalkingToFairy;
    }

    public void ResetValueBeforeBoss() {
        _posterity.ResetValuesBeforeBoss();
    }
}
