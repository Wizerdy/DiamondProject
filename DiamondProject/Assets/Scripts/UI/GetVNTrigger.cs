using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetVNTrigger : MonoBehaviour
{
    [SerializeField] private VNTrigger vnTrigger;

    // BOSS HINT
    public bool DoKnowBossFirstHint() {
        return vnTrigger.KnowFirstHintBoss;
    }
    public void LearnBossFirstHint() {
        vnTrigger.KnowFirstHintBoss = true;
    }

    public bool DoKnowBossSecondHint() {
        return vnTrigger.KnowSecondHintBoss;
    }
    public void LearnBossSecondHint() {
        vnTrigger.KnowSecondHintBoss = true;
    }

    // CHARACTER HINT

    public bool DoKnowCharacterFirstHint() {
        return vnTrigger.KnowFirstHintCharacter;
    }

    public void LearnCharacterFirstHint() {
        vnTrigger.KnowFirstHintCharacter = true;
    }
}
