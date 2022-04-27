using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDash : BaseAttack {
    [SerializeField] Reference<Transform> _target;
    [SerializeField] private float dashSpeed = 80f;
    [SerializeField] private float dashDamage = 30f;
    [SerializeField] private float dashLoadingTime = 3f;
    [SerializeField] private float dashMaxRange = 5f;
    [SerializeField] private float leafDamage = 10f;
    [SerializeField] private float leafSpeed = 10f;

    [SerializeField] private GameObject fallDashEntity;
    [SerializeField] private GameObject leafEntity;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject bossObject;

    private void Dash() {

    }
    protected override IEnumerator IExecute() {
        isPlaying = true;
        float dashCharge = dashLoadingTime;
        while(isPlaying) {
            dashCharge -= Time.deltaTime;


            Vector3 dir = new Vector3(playerObject.transform.position.x - transform.position.x, playerObject.transform.position.y - transform.position.y, 0);
            Vector3 posDir = transform.rotation * dir;
            Vector3 dashPos = transform.position + posDir * dashMaxRange;
            lineRenderer.SetPosition(1, dashPos);

            if (dashCharge <= 0) {
                Dash();
                isPlaying = false;
                yield return null;
            }
            yield return null;
        }
        //UpdateIA();
        yield return null;
    }
}
