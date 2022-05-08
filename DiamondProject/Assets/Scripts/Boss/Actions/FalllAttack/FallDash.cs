using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDash : BaseAttack {
    [SerializeField] LeafProjectile leafPrefab;
    [SerializeField] private float dashSpeed = 80f;
    [SerializeField] private float dashDamage = 30f;
    [SerializeField] private float dashLoadingTime = 3f;
    [SerializeField] private float dashMaxRange = 5f;
    [SerializeField] private float minDistOfWall = 1f;
    [SerializeField] private float spawnLeafEveryOneDivideByX = 5f;
    [SerializeField] private int leafDamage = 10;
    [SerializeField] private float leafSpeed = 10f;

    protected override IEnumerator IExecute() {
        isPlaying = true;
        yield return new WaitForSeconds(dashLoadingTime);
        RaycastHit2D[] raycastHit2D;
        Vector3 dashDir = (PlayerPos - BossPos).normalized;
        dashDir = new Vector3(dashDir.x, dashDir.y, 0);
        raycastHit2D = Physics2D.RaycastAll(BossPos, dashDir, dashMaxRange + minDistOfWall);
        Debug.DrawRay(BossPos, dashDir * (dashMaxRange + minDistOfWall), Color.red, 100f);
        float dashDist = dashMaxRange;
        for (int i = 0; i < raycastHit2D.Length; i++) {
            Debug.Log(raycastHit2D[i].collider.gameObject.name);
            if (raycastHit2D[i].collider.gameObject.tag == "Structure") {
                if (Vector3.Distance(BossPos, raycastHit2D[i].point) - minDistOfWall < dashDist) {
                    dashDist = Vector3.Distance(BossPos, raycastHit2D[i].point) - minDistOfWall;
                    Debug.Log(dashDist);
                    Debug.Log(raycastHit2D[i].collider.gameObject.name);
                }
            }
        }
        Vector3 dashDepart = BossPos;
        Vector3 dashDest = BossPos + dashDir * dashDist;
        float dashCurrentPos = 0;
        float nextDashCurrentPos = 0;
        while (Vector3.Distance(BossPos, dashDest) > 0.1f) {
            nextDashCurrentPos += dashSpeed * Time.deltaTime / dashDist;
            dashCurrentPos += dashSpeed * Time.deltaTime / dashDist;
            if (nextDashCurrentPos > 1 / (spawnLeafEveryOneDivideByX + 1)) {
                nextDashCurrentPos = 0;
                LeafProjectile newLeaf = Instantiate(leafPrefab, BossPos, Quaternion.identity);
                newLeaf.SetDirection(Quaternion.Euler(0, 0, 45) * dashDir)
                    .SetSpeed(leafSpeed)
                    .SetDamage(leafDamage);
                newLeaf = Instantiate(leafPrefab, BossPos, Quaternion.identity);
                newLeaf.SetDirection(Quaternion.Euler(0, 0, -45) * dashDir)
                    .SetSpeed(leafSpeed)
                    .SetDamage(leafDamage);
            }
            BossPos = Vector3.Lerp(dashDepart, dashDest, dashCurrentPos);
            yield return null;
        }
        isPlaying = false;
    }
}
