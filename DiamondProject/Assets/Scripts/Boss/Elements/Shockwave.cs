using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : BossEntities {
    public bool change = false;
    public Fissure fissure;
    public void Change() {
        change = true;
    }

    private void Update() {
        if (!change) {
            transform.localScale = Vector3.one * (transform.localScale.x + fissure._fissureSpeed * Time.deltaTime);
            if (transform.localScale.x >= fissure._sizeToReach) {
                fissure.NextState();
                Destroy(gameObject);
            }
        } else {
            transform.localScale = Vector3.one * (transform.localScale.x + -fissure._fissureSpeed * Time.deltaTime);
            if (transform.localScale.x < 1) {
                fissure.NextState();
                bossRef.Instance.gameObject.GetComponent<Health>().TakeDamage(50);
                Destroy(gameObject);
            }
        }        
    }
}
