using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Hint {
    public int id;
    public string defaultName;
    public string unlockedName;
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
    public float textSpeed = 50f;
    public int numberOfTimeDyingWithoutKillingForm = 0;
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
    public GameObject chargedArrow;

    [Header("Fall form")]
    public List<Hint> fallBossHintList = new List<Hint>();
    public List<Hint> fallAttackHintList = new List<Hint>();
    [Header("Winter form")]
    public List<Hint> winterBossHintList = new List<Hint>();
    public List<Hint> winterAttackHintList = new List<Hint>();

    private Hint hint;

    public void ResetValues() {
        textSpeed = 50f;
        numberOfTimeDyingWithoutKillingForm = 0;
        killNeutralForm = false;
        killFallForm = false;
        killWinterForm = false;
        sawFallForm = false;
        sawWinterForm = false;
        firstTimeTalking = true;

        winterFormTriggerActivated = false;

        nbCorpse = 0;
        nbTimeTalkedToNorna = 0;
        deathCount = 0;
        bossDeathCount = 0;
        ResetBonusAndMalus();
        ResetHint();
    }

    public void ResetHint() {
        for (int i = 0; i < fallBossHintList.Count; i++) {
            hint = fallBossHintList[i];
            hint.isTrigger = false;
            fallBossHintList[i] = hint;
        }

        for (int i = 0; i < fallAttackHintList.Count; i++) {
            hint = fallAttackHintList[i];
            hint.isTrigger = false;
            fallAttackHintList[i] = hint;
        }

        for (int i = 0; i < winterBossHintList.Count; i++) {
            hint = winterBossHintList[i];
            hint.isTrigger = false;
            winterBossHintList[i] = hint;
        }

        for (int i = 0; i < winterAttackHintList.Count; i++) {
            hint = winterAttackHintList[i];
            hint.isTrigger = false;
            winterAttackHintList[i] = hint;
        }
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
