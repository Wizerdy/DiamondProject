using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VNTrigger")]
public class VNTrigger : ScriptableObject {
    public bool KnowFirstHintBoss = false;
    public bool KnowSecondHintBoss = false;
    public bool KnowFirstHintCharacter = false;

    public void ResetValue() {
        KnowFirstHintBoss = false;
        KnowSecondHintBoss = false;
        KnowFirstHintCharacter = false;
    }
}
