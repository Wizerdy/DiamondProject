using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PosterityVNValue : MonoBehaviour
{
    [SerializeField] private PosterityObject posterityObj;
    [SerializeField] private GameObject spriteFeedBack;

    private Hint hint;
    public void ArrowHit(GameObject obj) {
        hint = posterityObj.fallAttackHintList[0];
        if (!hint.isTrigger) {
            if (obj.gameObject.GetComponent<ProtectorTree>()) {
                spriteFeedBack?.SetActive(true);
                hint.isTrigger = true;
                posterityObj.fallAttackHintList[0] = hint;
                return;
            }
        }
    }

    public void ArrowTrigger(GameObject obj) {
        hint = posterityObj.fallAttackHintList[2];
        if (!hint.isTrigger) {
            if (obj.gameObject.GetComponent<Boomerang>()) {
                spriteFeedBack?.SetActive(true);
                hint.isTrigger = true;
                posterityObj.fallAttackHintList[2] = hint;
                return;
            }
        }

        hint = posterityObj.winterAttackHintList[2];
        if (!hint.isTrigger) {
            if (obj.gameObject.GetComponent<IceWall>()) {
                spriteFeedBack.SetActive(true);
                hint.isTrigger = true;
                posterityObj.winterAttackHintList[2] = hint;
                return;
            }
        }

    }

    public void IfBeamHitArrow(GameObject obj) {
        hint = posterityObj.fallAttackHintList[3];
        if (!hint.isTrigger) {
            if (obj.GetComponent<ChargedBullet>()) {
                hint.isTrigger = true;
                posterityObj.fallAttackHintList[3] = hint;
                return;
            }
        }

    }

    public void SwordHit(GameObject obj) {
        //hit leaf shield
        hint = posterityObj.fallAttackHintList[1];
        if (!hint.isTrigger) {
            HealthProxy _health = obj.gameObject.GetComponent<HealthProxy>();
            if (_health != null) {
                if (!_health.CanTakeDamage && _health.gameObject.tag == "Boss") {
                    spriteFeedBack?.SetActive(true);
                    hint.isTrigger = true;
                    posterityObj.fallAttackHintList[1] = hint;
                    return;
                }
            }
        }
    }

    public void SwordTrigger(GameObject obj) {
        hint = posterityObj.winterAttackHintList[1];
        if (!hint.isTrigger) {
            if (obj.GetComponent<IceShard>()?.shardType == ShardType.bulletHell) {
                spriteFeedBack?.SetActive(true);
                hint.isTrigger = true;
                posterityObj.winterAttackHintList[1] = hint;
                return;
            }
        }


        hint = posterityObj.winterAttackHintList[0];
        if (!hint.isTrigger) {
            if (obj.GetComponent<IceShard>()?.shardType == ShardType.iceHell) {
                spriteFeedBack?.SetActive(true);
                hint.isTrigger = true;
                posterityObj.winterAttackHintList[0] = hint;
                return;
            }
        }

    }

    public void OnFirstTimeSawForm (BossShapeSystem shape) {
        switch (shape.Shape.Type) {
            case Shape.NEUTRAL:
                break;
            case Shape.SPRING:
                break;
            case Shape.SUMMER:
                break;
            case Shape.FALL:
                posterityObj.sawFallForm = true;
                break;
            case Shape.WINTER:
                posterityObj.sawWinterForm = true;
                break;
            default:
                break;
        }
    }

    public void OnKillForm(BossShapeSystem shape) {
        posterityObj.numberOfTimeDyingWithoutKillingForm = 0;
        switch (shape.Shape.Type) {
            case Shape.NEUTRAL:
                posterityObj.killNeutralForm = true;
                break;
            case Shape.SPRING:
                break;
            case Shape.SUMMER:
                break;
            case Shape.FALL:
                posterityObj.killFallForm = true;
                break;
            case Shape.WINTER:
                posterityObj.killWinterForm = true;
                break;
            default:
                break;
        }
    }

    public GameObject GetArrow() {
        return posterityObj.arrow;
    }
}
