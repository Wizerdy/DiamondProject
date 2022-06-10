using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosterityVNValue : MonoBehaviour
{
    [SerializeField] private PosterityObject posterityObj;
    [SerializeField] private BossShapeSystem shapeSystem;

    public void OnTriggerActivate() {
        if (shapeSystem.Shape.Type == Shape.NEUTRAL) {
            return;
        }

        if (shapeSystem.Shape.Type == Shape.SPRING) {
            //Hint hint = posterityObj.triggerHintList[0];
            //hint.isTrigger = true;
            //posterityObj.triggerHintList[0] = hint;

        }

        if (shapeSystem.Shape.Type == Shape.SUMMER) {
            //Hint hint = posterityObj.triggerHintList[1];
            //hint.isTrigger = true;
            //posterityObj.triggerHintList[1] = hint;

        }

        if (shapeSystem.Shape.Type == Shape.FALL) {
            //Hint hint = posterityObj.triggerHintList[2];
            //hint.isTrigger = true;
            //posterityObj.triggerHintList[2] = hint;

        }

        if (shapeSystem.Shape.Type == Shape.WINTER) {
            //Hint hint = posterityObj.triggerHintList[3];
            //hint.isTrigger = true;
            //posterityObj.triggerHintList[3] = hint;
        }
    }
}
