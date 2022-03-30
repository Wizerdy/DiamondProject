using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PosterityObject")]
public class PosterityObject : ScriptableObject {
    public bool resetOnStart = true;
    public int deathCount = 0;
    public int bossDeathCount = 0;

    public bool knowFirstHint = false;
    public bool knowSecondHint = false;
    public bool knowThirdtHint = false;

    public bool gotToAnotherBoss = false;
    public int numberOfTriggerActivate = 0;

    public void ResetValues() {
        deathCount = 0;
        bossDeathCount = 0;

        knowFirstHint = false;
        knowSecondHint = false;
        knowThirdtHint = false;

        gotToAnotherBoss = false;
        numberOfTriggerActivate = 0;
    }
}
