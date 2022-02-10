using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    private GameObject player;
    enum State { WAIT, TELEPORT, ROCKFALL, FIRE, }
    private State state;

    [Header("RockFall")]
    [SerializeField] FallingObject fallingObject = null;
    [SerializeField] public Rock rock;
    [SerializeField] public float fallingTime = 3f;
    [SerializeField] List<FallingObject> fallingRocks = new List<FallingObject>();
    [SerializeField] Vector2 radiusBounds;
    [SerializeField] float apparitionHigh = 10;
    [SerializeField] Vector2Int rocksNumberBounds;

    [Header("Spin")]
    [SerializeField] MagicBall magicBall = null;
    [SerializeField] float magicBallSpeed = 1f;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float magicBallRate = 1f;
    [SerializeField] float spinDuration = 1f;
    [SerializeField] float magicBallDistSpawn = 1f;

    [Header("Missile")]
    [SerializeField] Missile missile = null;
    [SerializeField] float missileSpeed = 1f;
    [SerializeField] float missileLifetime = 1f;
    [SerializeField] float missileDistSpawn = 1f;
    [SerializeField] float missileRate = 1f;
    [SerializeField] Vector2 missileBounds = Vector2.zero;

    [Header("Teleport")]
    [SerializeField] private float fleeRadius = 2f;
    [SerializeField] private float fleeDistance = 4f;


    private void Start() {
        StartCoroutine(MortalMissile());
    }
    void Update() {
        UpdateFlee();
    }

    void WeWillRockYou(int rockNumbers, Vector3 position) {
        for (int i = 0; i < rockNumbers; i++) {
            float randomDegree = Random.Range(0, 360);
            float randomDist = Random.Range(radiusBounds.x, radiusBounds.y);
            Vector3 destination = new Vector3(Mathf.Cos(randomDegree * Mathf.Deg2Rad), Mathf.Sin(randomDegree * Mathf.Deg2Rad), 0);
            destination = destination.normalized * randomDist;
            Debug.Log(destination);
            Debug.Log(randomDist);
            destination.z = position.z;

            FallingObject newFallingObject = Instantiate(fallingObject.gameObject, position + destination + new Vector3(0,apparitionHigh,0), fallingObject.transform.rotation).GetComponent<FallingObject>();
            newFallingObject.SetFallen(rock.gameObject)
                .SetSprite(rock.sprite)
                .SetDestination(destination)
                .SetFallTime(fallingTime);
            fallingRocks.Add(newFallingObject);
        }
    }
    
    IEnumerator DeathSpin(float duration) {
        float fireRateTimer = magicBallRate;
        float durationTimer = duration;
        while (durationTimer > 0) {
            durationTimer -= Time.deltaTime;
            transform.Rotate(new Vector3(0, 0, 360 * rotationSpeed * Time.deltaTime));
            fireRateTimer -= Time.deltaTime;
            if (fireRateTimer <= 0) {
                fireRateTimer = magicBallRate;
                FireMagicBall(transform.right, magicBallSpeed, transform.position + transform.right * magicBallDistSpawn, MagicBall.State.RED);
                FireMagicBall(transform.right * -1, magicBallSpeed, transform.position + transform.right * magicBallDistSpawn * -1, MagicBall.State.YELLOW);
            }
            yield return null;
        }
    }

    void FireMagicBall(Vector3 direction, float speed, Vector3 position, MagicBall.State state) { 
        MagicBall newMagicBall = Instantiate(magicBall.gameObject, position, Quaternion.identity).GetComponent<MagicBall>();
        newMagicBall.SetDirection(direction)
            .SetSpeed(speed)
            .SetState(state);
    }

    void FireMissile(float speed, Vector3 position) {
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
                FireMissile(missileSpeed, transform.position + transform.right * missileDistSpawn);
            }
            yield return null;
        }
    void UpdateFlee() {
        Vector3 playerPosition = player.transform.position;
        if ((playerPosition - transform.position).sqrMagnitude < fleeRadius) {
            FleeFrom(player);
        }
    }

    void FleeFrom(GameObject entity) {
        Vector3 entityPosition = entity.transform.position;
        Vector3 direction = (entityPosition - transform.position).normalized;
        Vector3 destination = direction * fleeDistance;
        Teleport(destination);
    }

    void Teleport(Vector3 position) {
        transform.position = position;
    }
}
