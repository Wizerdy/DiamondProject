using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploBush : BaseAttack {
    [SerializeField] float missileSpeed = 1f;
    [SerializeField] float missileRotationSpeed = 1f;
    [SerializeField] float missileLifetime = 1f;
    [SerializeField] Vector2 missileBounds = Vector2.zero;

    [Header("For Prog: ")]
    [SerializeField] Missile missile = null;
    [SerializeField] float upCollision = 0f;
    [SerializeField] float downCollision = 0f;
    [SerializeField] float rightCollision = 0f;
    [SerializeField] float leftCollision = 0f;
    [SerializeField] float radiusDetection = 2f;
    [SerializeField] float exploBushAlive = 2f;

    private void Awake() {
        exploBushAlive = 0;
        locked = false;
    }
    void SpawnMissile(float speed, Vector3 position) {
        Missile newMissile = Instantiate(missile.gameObject, position, Quaternion.identity).GetComponent<Missile>();
        newMissile.SetSpeed(speed)
            .SetLifeTime(missileLifetime)
            .SetRotationSpeed(missileRotationSpeed);
    }

    void SpawnMissile(float speed, Vector3 position, float number) {
        Missile newMissile = Instantiate(missile.gameObject, position, Quaternion.identity).GetComponent<Missile>();
        newMissile.SetSpeed(speed)
            .SetLifeTime(missileLifetime)
            .SetRotationSpeed(missileRotationSpeed);
        newMissile.name = number.ToString();
        newMissile.enabled = true;
    }

    protected override IEnumerator IExecute() {
        isPlaying = true;
        float randomMissiles = Random.Range(missileBounds.x, missileBounds.y);
        float numberMissilesFired = 0;
        while (randomMissiles > numberMissilesFired) {
            numberMissilesFired++;
            bool badDestination = false;
            Vector3 destination = Vector3.zero;
            do {
                destination = RandomOvalPosition();
                Collider[] hitColliders = Physics.OverlapSphere(destination, radiusDetection);
                for (int i = 0; i < hitColliders.Length; i++) {
                    if (hitColliders[i].tag == "Player" || hitColliders[i].tag == "Boss" || hitColliders[i].tag == "ExploBuisson") {
                        badDestination = true;
                    }
                }
            }
            while (badDestination);
            SpawnMissile(missileSpeed, destination, numberMissilesFired);

        }
        isPlaying = false;
        yield return null;
    }



    public Vector2 RandomOvalPosition() {
        float randomLenght = Random.Range(0f, 100f) / 100;
        float halfheightEllipse = Mathf.Abs(upCollision - downCollision) / 2 * randomLenght;
        float halfwidthEllipse = Mathf.Abs(rightCollision - leftCollision) / 2 * randomLenght;
        float randomAngle = Random.Range(0, 360);

        Vector2 PositionOnEllipse = new Vector2(
        transform.position.x + halfwidthEllipse * Mathf.Cos(randomAngle * Mathf.PI / 180f),
        transform.position.y + halfheightEllipse * Mathf.Sin(randomAngle * Mathf.PI / 180f)
        );
        return PositionOnEllipse;
    }

    public void AddExploBush() {
        exploBushAlive++;
        locked = true;
    }

    public void RemoveExploBush() {
        exploBushAlive--;
        if(exploBushAlive == 0)
            locked = false;
    }
}
