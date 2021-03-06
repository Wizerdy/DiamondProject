using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMissile : BossAction {
    [SerializeField] float missileSpeed = 1f;
    [SerializeField] float missileRotationSpeed = 1f;
    [SerializeField] float missileLifetime = 1f;
    [SerializeField] float missileDistSpawn = 1f;
    [SerializeField] float missileRate = 1f;
    [SerializeField] Vector2 missileBounds = Vector2.zero;
    [Header("For Prog: ")]
    [SerializeField] Missile missile = null;
    public int counter = 0;
    public override void StartAction() {
        counter = 0;
        OnCast?.Invoke();
        _boss.Instance.ChangeState(GetState());
        //Debug.Log("Missile");
        StartCoroutine(MortalMissile());
    }

    void SpawnMissile(float speed, Vector3 position) {
        Missile newMissile = Instantiate(missile.gameObject, position, Quaternion.identity).GetComponent<Missile>();
        newMissile.SetSpeed(speed)
            .SetLifeTime(missileLifetime)
            .SetRotationSpeed(missileRotationSpeed);
        newMissile.firemissile = this;
    }

    void SpawnMissile(float speed, Vector3 position, float number) {
        Missile newMissile = Instantiate(missile.gameObject, position, Quaternion.identity).GetComponent<Missile>();
        newMissile.SetSpeed(speed)
            .SetLifeTime(missileLifetime)
            .SetRotationSpeed(missileRotationSpeed);
        newMissile.firemissile = this;
        newMissile.name = number.ToString();
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
                Vector3 destination = new Vector3(Mathf.Cos(randomDegree * Mathf.Deg2Rad), Mathf.Sin(randomDegree * Mathf.Deg2Rad), 0);
                destination = destination.normalized * missileDistSpawn;
                destination.z = transform.position.z;
                SpawnMissile(missileSpeed, transform.position + destination, numberMissilesFired);
            }
            yield return null;
        }
        Wait();
    }

    public override Boss.State GetState() {
        return Boss.State.FIREMISSILE;
    }
}
