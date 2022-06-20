using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class BossFollowPlayer : BaseAttack {
    [SerializeField] float _speed;
    [SerializeField] float _minDistanceHarcelement = 2f;

    protected override IEnumerator IExecute() {
        float timer = duration;
        while (timer >= 0) {
            timer -= Time.deltaTime;
            Vector2 distance = (PlayerPos - BossPos);
            if (distance.sqrMagnitude > _minDistanceHarcelement * _minDistanceHarcelement) {
                BossPos += distance.normalized.To3D() * _speed * Time.deltaTime;
            }
            yield return null;
        }
    }
}
