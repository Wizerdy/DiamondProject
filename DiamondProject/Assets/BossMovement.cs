using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : BaseAttack {

    [SerializeField] float timeTravel;
    [SerializeField] float _speed;
    [SerializeField] float minDistOfWall;

    protected override IEnumerator IExecute() {
        RaycastHit2D[] raycastHit2D;
        Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        Vector3 depart = BossPos;
        float maxDist = 50f;
        raycastHit2D = Physics2D.RaycastAll(BossPos, randomDir, maxDist);
        //Debug.DrawRay(BossPos, randomDir * maxDist, Color.red, 100f);
        for (int i = 0; i < raycastHit2D.Length; i++) {
            //Debug.Log(raycastHit2D[i].collider.gameObject.name);
            if (raycastHit2D[i].collider.gameObject.tag == "Structure") {
                if (Vector3.Distance(BossPos, raycastHit2D[i].point) - minDistOfWall < maxDist) {
                    maxDist = Vector3.Distance(BossPos, raycastHit2D[i].point) - minDistOfWall;
                   // Debug.Log(maxDist);
                    //Debug.Log(raycastHit2D[i].collider.gameObject.name);
                }
            }
        }
        float timer = timeTravel;
        float currentDist = 0;
        while (currentDist < maxDist && timer >= 0) {
            timer -= Time.deltaTime;
            currentDist += Time.deltaTime * _speed;
            BossPos += randomDir * Time.deltaTime * _speed;
            yield return null;
        }
    }
}
