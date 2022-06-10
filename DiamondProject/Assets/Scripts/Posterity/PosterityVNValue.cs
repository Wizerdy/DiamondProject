using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PosterityVNValue : MonoBehaviour
{
    [SerializeField] private PosterityObject posterityObj;
    [SerializeField] private BossShapeSystem shapeSystem;
    [SerializeField] private EntityChargeRanged chargeRange;

    private void Start() {
        chargeRange.OnHit += ArrowHit;
    }
    public void ArrowHit(GameObject obj) {
        Debug.Log("arrow hit");
        Debug.Log(obj.name);
        //if (collision.gameObject.GetComponent<ProtectorTree>()) {

        //}

        //if (collision.gameObject.GetComponent<Boomerang>()) {

        //}

        //if (collision.gameObject.GetComponent<LeafBeam>()) {

        //}
        //if (collision.gameObject.GetComponent<SnowAbsorption>()) {

        //}

    }

    public void SwordHit() {
        //if (collision.gameObject.GetComponent<shield>) {

        //}

        //if (collision.gameObject.GetComponent<>)
    }

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
