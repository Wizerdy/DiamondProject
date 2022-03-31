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
    }
}
