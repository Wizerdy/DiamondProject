using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PosterityObject")]
public class PosterityObject : ScriptableObject {
    public bool resetOnStart = true;
    public int deathCount = 0;
    public int bossDeathCount = 0;

    public void ResetValues() {
        deathCount = 0;
        bossDeathCount = 0;
    }
}
