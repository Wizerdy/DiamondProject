using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryPosterity : MonoBehaviour
{
    [SerializeField] private PosterityObject posterity;

    public void NornaTellYouTriggerHint(int id) {
        Hint bossHint = posterity.triggerHintList[id];
        bossHint.didNornaGaveHint = true;
        bossHint.isTrigger = true;
        bossHint.alreadySawHint = true;
        posterity.triggerHintList[id] = bossHint;
    }

    public void NornaTellYouCharacterHint(int id) {
        Hint characterHint = posterity.characterHintList[id];
        characterHint.didNornaGaveHint = true;
        characterHint.isTrigger = true;
        characterHint.alreadySawHint = true;
        posterity.characterHintList[id] = characterHint;
    }
}
