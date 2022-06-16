using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VNTrigger")]
public class VNTrigger : ScriptableObject {
    public bool KnowFirstHintBoss = false;
    public bool KnowSecondHintBoss = false;
    public bool KnowFirstHintCharacter = false;
    public int questionNumber = 0;

    public bool knowWinterBossHint = false;
    public bool knowFallBossHint = false;

    public List<bool> alreadySawText = new List<bool>();

    public void ResetValue() {
        KnowFirstHintBoss = false;
        KnowSecondHintBoss = false;
        KnowFirstHintCharacter = false;
        knowWinterBossHint = false;
        knowFallBossHint = false;
    }
}
