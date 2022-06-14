using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Hint {
    public int id;
    public string description;
    public bool isTrigger;
    public bool didNornaGaveHint;
    public bool alreadySawHint;
    public string defaultText;
    public string unlockedText;
}

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

    [Header("For VN Scene")]
    public bool killNeutralForm = false;
    public bool killFallForm = false;
    public bool killWinterForm = false;

    public bool sawFallForm = false;
    public bool sawWinterForm = false;

    public bool firstTimeTalking = true;

    public int nbTimeTalkedToNorna = 0;
    public int nbCorpse = 0;

    public bool winterFormTriggerActivated = false;

    public GameObject arrow;
    //public List<Hint> triggerHintList = new List<Hint>();
    //public List<Hint> bossHintList = new List<Hint>();
    //public List<Hint> characterHintList = new List<Hint>();
    [Header("Fall form")]
    public List<Hint> fallBossHintList = new List<Hint>();
    public List<Hint> fallAttackHintList = new List<Hint>();
    [Header("Winter form")]
    public List<Hint> winterBossHintList = new List<Hint>();
    public List<Hint> winterAttackHintList = new List<Hint>();
    public void ResetValues() {
        nbCorpse = 0;
        nbTimeTalkedToNorna = 0;
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
    }

    public void ResetValuesBeforeBoss() {
        gotToAnotherBoss = false;
        numberOfTriggerActivate = 0;
    }
}
