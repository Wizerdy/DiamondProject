using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetVNTrigger : MonoBehaviour
{
    [SerializeField] private VNTrigger vnTrigger;
    [SerializeField] private PosterityObject posterity;

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

    public bool DoKnowWinterBossHint() {
        return vnTrigger.knowWinterBossHint;
    }

    public void LearnFallBossHint() {
        vnTrigger.knowFallBossHint = true;
    }

    public bool DoKnowFallBossHint() {
        return vnTrigger.knowFallBossHint;
    }

    public void LearnWinterBossHint() {
        vnTrigger.knowWinterBossHint = true;
    }

    // CHARACTER HINT

    public bool DoKnowCharacterFirstHint() {
        return vnTrigger.KnowFirstHintCharacter;
    }

    public void LearnCharacterFirstHint() {
        vnTrigger.KnowFirstHintCharacter = true;
    }

    public bool DidSawFall() {
        if (posterity.sawFallForm)
            return true;

        return false;
    }

    public bool DidSawWinter() {
        if (posterity.sawWinterForm)
            return true;

        return false;
    }
    public bool DidKillNeutral() {
        if (posterity.killNeutralForm)
            return true;

        return false;
    }

    public bool DidKillFall() {
        if (posterity.killFallForm)
            return true;

        return false;
    }

    public bool DidKillWinter() {
        if (posterity.killWinterForm)
            return true;

        return false;
    }

}
