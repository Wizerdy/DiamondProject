using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMissile : BossAction {
    [SerializeField] Missile missile = null;
    [SerializeField] float missileSpeed = 1f;
    [SerializeField] float missileLifetime = 1f;
    [SerializeField] float missileDistSpawn = 1f;
    [SerializeField] float missileRate = 1f;
    [SerializeField] Vector2 missileBounds = Vector2.zero;
    [SerializeField] Vector2 radiusBounds = Vector2.zero;
    public override IEnumerator StartAction() {
        Debug.Log("Missile");
        _boss.Instance.ChangeState(GetState());
        _durationTimer = _duration;
        while (_durationTimer > 0) {
            yield return null;
            _durationTimer -= Time.deltaTime;
        }
        _boss.Instance.EndState(transitionTime);
    }

    void SpawnMissile(float speed, Vector3 position) {
        Missile newMissile = Instantiate(missile.gameObject, position, Quaternion.identity).GetComponent<Missile>();
        newMissile.SetSpeed(speed)
            .SetLifeTime(missileLifetime);
    }

    IEnumerator MortalMissile() {
        float fireRateTimer = missileRate;
        float randomMissiles = Random.Range(missileBounds.x, missileBounds.y);
        float numberMissilesFired = 0;
        while (randomMissiles > numberMissilesFired) {
            fireRateTimer -= Time.deltaTime;
            if (fireRateTimer <= 0) {
                numberMissilesFired++;
                fireRateTimer = missileRate;
                float randomDegree = Random.Range(0, 360);
                float randomDist = Random.Range(radiusBounds.x, radiusBounds.y);
                Vector3 destination = new Vector3(Mathf.Cos(randomDegree * Mathf.Deg2Rad), Mathf.Sin(randomDegree * Mathf.Deg2Rad), 0);
                destination = destination.normalized * randomDist;
                destination.z = transform.position.z;
                SpawnMissile(missileSpeed, transform.position + destination.normalized * missileDistSpawn);
            }
            yield return null;
        }
    }

    public override Boss.State GetState() {
        return Boss.State.FIREMISSILE;
    }
}
