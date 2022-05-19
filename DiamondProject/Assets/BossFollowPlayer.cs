using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFollowPlayer : BaseAttack {
    [SerializeField] float _speed;

    protected override IEnumerator IExecute() {
        float timer = duration;
        while (timer >= 0) {
            timer -= Time.deltaTime;
            BossPos += (PlayerPos - BossPos).normalized * _speed * Time.deltaTime;
            yield return null;
        }
    }
}
