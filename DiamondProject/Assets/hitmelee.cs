using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitmelee : MonoBehaviour {
    //private void OnCollisionEnter2D(Collision2D collision) {
    //    if (collision.gameObject.tag == "Boss") {
    //        collision.gameObject.GetComponentInParent<TriggerNoctali>().hit++;
    //        if(collision.gameObject.GetComponentInParent<TriggerNoctali>().hit > 3 && !collision.gameObject.GetComponentInParent<TriggerNoctali>().phaseuno) {
    //            collision.gameObject.GetComponentInParent<TriggerNoctali>().phaseuno = true;
    //            collision.gameObject.GetComponentInParent<TriggerNoctali>().Active();
    //        }
    //        if (collision.gameObject.GetComponentInParent<Boss>().CurrentState == Boss.State.FIREBALL && !collision.gameObject.GetComponentInParent<TriggerNoctali>().phasedo) {
    //            collision.gameObject.GetComponentInParent<TriggerNoctali>().phasedo = true;
    //            collision.gameObject.GetComponentInParent<TriggerNoctali>().Active();
    //        }
    //        collision.gameObject.GetComponentInParent<TriggerNoctali>().Verif();
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Boss") {
            if (collision.gameObject.GetComponentInParent<TriggerNoctali>()) {
                collision.gameObject.GetComponentInParent<TriggerNoctali>().hit++;
                if (collision.gameObject.GetComponentInParent<TriggerNoctali>().hit > 3 && !collision.gameObject.GetComponentInParent<TriggerNoctali>().phaseuno) {
                    collision.gameObject.GetComponentInParent<TriggerNoctali>().phaseuno = true;
                    collision.gameObject.GetComponentInParent<TriggerNoctali>().post.numberOfTriggerActivate++;
                    collision.gameObject.GetComponentInParent<TriggerNoctali>().Active();
                }
                if (collision.gameObject.GetComponentInParent<Boss>().CurrentState == Boss.State.FIREBALL && !collision.gameObject.GetComponentInParent<TriggerNoctali>().phasedo) {
                    collision.gameObject.GetComponentInParent<TriggerNoctali>().phasedo = true;
                    collision.gameObject.GetComponentInParent<TriggerNoctali>().post.numberOfTriggerActivate++;
                    collision.gameObject.GetComponentInParent<TriggerNoctali>().Active();
                }
                collision.gameObject.GetComponentInParent<TriggerNoctali>().Verif();
            }
        }


    }
}
