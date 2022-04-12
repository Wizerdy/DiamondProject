using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PosterityObject")]
public class PosterityObject : ScriptableObject {
    public bool resetOnStart = true;
    public int deathCount = 0;
    public int bossDeathCount = 0;

    [Header("Bonus")]
    public int maxLifeModifier = 0;
    public int extraLife = 0;
    public bool nearSight = false;
    public bool seeBossHealthBar = false;
    public bool dontSeeHealthBar = false;
    public bool gotToAnotherBoss = false;
    public int numberOfTriggerActivate = 0;

    public bool firstTimeTalking = true;

    public int numberOfTimeTalkingToFairy = 0;

    public void ResetValues() {
        deathCount = 0;
        bossDeathCount = 0;
        ResetBonusAndMalus();
    }

    public void ResetBonusAndMalus() {
        maxLifeModifier = 0;
        extraLife = 0;
        nearSight = false;
        seeBossHealthBar = false;
        dontSeeHealthBar = false;

        gotToAnotherBoss = false;
        numberOfTriggerActivate = 0;

        firstTimeTalking = true;
        numberOfTimeTalkingToFairy = 0;
    }

    public void ResetValuesBeforeBoss() {
        gotToAnotherBoss = false;
        numberOfTriggerActivate = 0;
    }
}
