using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    [SerializeField]
    GameObject missile;

    [Header("RockFall")]
    [SerializeField] FallingObject fallingObject = null;
    [SerializeField] public Rock rock;
    [SerializeField] public float fallingTime = 3f;
    [SerializeField] List<FallingObject> fallingRocks = new List<FallingObject>();
    [SerializeField] Vector2 radiusBounds;
    [SerializeField] float apparitionHigh = 10;
    [SerializeField] Vector2Int rocksNumberBounds;

    [Header("Spin")]
    [SerializeField] MagicBall magicBall;
    [SerializeField] float magicBallSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float fireRate;
    [SerializeField] float spinDuration;
    [SerializeField] float distSpawn;

    enum State {
        WAIT,
        TELEPORT,
        ROCKFALL,
        FIRE,
    }

    State state;

    private void Start() {
        StartCoroutine(DeathSpin(spinDuration));
    }
    void Update() {
        
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
        float fireRateTimer = fireRate;
        float durationTimer = duration;
        while (durationTimer > 0) {
            durationTimer -= Time.deltaTime;
            transform.Rotate(new Vector3(0, 0, 360 * rotationSpeed * Time.deltaTime));
            fireRateTimer -= Time.deltaTime;
            if (fireRateTimer <= 0) {
                fireRateTimer = fireRate;
                FireMagicBall(transform.right, magicBallSpeed, transform.position + transform.right * distSpawn, MagicBall.State.RED);
                FireMagicBall(transform.right * -1, magicBallSpeed, transform.position + transform.right * distSpawn * -1, MagicBall.State.YELLOW);
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
}
