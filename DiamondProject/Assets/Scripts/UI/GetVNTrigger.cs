using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetVNTrigger : MonoBehaviour
{
    [SerializeField] private VNTrigger vnTrigger;
    [SerializeField] private PosterityObject posterity;
    [SerializeField] private GameObject arrowNeutral;
    [SerializeField] private GameObject arrowSleep;
    [SerializeField] private GameObject arrowBoomerang;

    private Hint hint;
    public void LearnHint(hintType type, int index) {
        switch (type) {
            case hintType.FallBoss:
                hint.isTrigger = true;
                posterity.fallBossHintList[index] = hint;
                break;
            case hintType.FallAttack:
                hint.isTrigger = true;
                posterity.fallAttackHintList[index] = hint;
                break;
            case hintType.WinterBoss:
                hint.isTrigger = true;
                posterity.winterBossHintList[index] = hint;
                break;
            case hintType.WinterAttack:
                hint.isTrigger = true;
                posterity.winterAttackHintList[index] = hint;
                break;
            default:
                break;
        }
    }

    public bool DidSawText(int index) {
        return vnTrigger.alreadySawText[index];
    }

    public void SeeText(int index) {
        vnTrigger.alreadySawText[index] = true;
    }

    public void AddNumberOfTimeDyingWithoutKillingForm() {
        ++posterity.numberOfTimeDyingWithoutKillingForm;
    }

    public int ReturnNumberOfTimeDyingWithoutKillingForm() {
        return posterity.numberOfTimeDyingWithoutKillingForm;
    }

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

    public void ChangeToNeutralArrow() {
        posterity.arrow = arrowNeutral;
    }

    public void ChangeToSleepArrow() {
        posterity.arrow = arrowSleep;
    }

    public void ChangeToBoomerangArrow() {
        posterity.arrow = arrowBoomerang;
    }
}
