using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    public int life = 10;

    private GameObject player;
    SpriteRenderer sr = null;
    enum State { WAIT, TELEPORT, ROCKFALL, FIREMISSILE, FIREBALL }
    private State state = State.WAIT;
    [SerializeField] float stateTime = 1f;
    float stateTimer = 1f;

    [Header("RockFall")]
    [SerializeField] FallingObject fallingObject = null;
    [SerializeField] public Rock rock;
    [SerializeField] public float fallingTime = 3f;
    [SerializeField] public List<Rock> rocks = new List<Rock>();
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
    [SerializeField] float fleeRadius = 2f;
    [SerializeField] float fleeDistance = 4f;

    [Header("Shield")]
    [SerializeField] bool isShield = false;
    [SerializeField] float damage = 0.3f;
    [SerializeField] bool damaged = false;


    private void Start() {
        sr = GetComponent<SpriteRenderer>();
        player = Gino.instance.player.gameObject;
    }

    void Update() {
        UpdateFlee();
        if (stateTimer <= 0)
            UpdateState();
        UpdateShield();
        stateTimer -= Time.deltaTime;
    }

    void UpdateShield() {
        if (rocks.Count == 0 && state != State.ROCKFALL) {
            isShield = false;
            if(!damaged)
            sr.color = Color.green;
        }
    }
    void UpdateState() {
        switch (state) {
            case State.WAIT:
                int attack = 0;
                if (isShield) {
                    attack = Random.Range(0, 2);
                } else {
                    attack = Random.Range(0, 3);
                }
                switch (attack) {
                    case 0:
                        NewState(State.FIREBALL);
                        StartCoroutine(DeathSpin(spinDuration));
                        break;
                    case 1:
                        NewState(State.FIREMISSILE);
                        StartCoroutine(MortalMissile());
                        break;
                    case 2:
                        NewState(State.ROCKFALL);
                        StartCoroutine(WeWillRockYou(Random.Range(rocksNumberBounds.x, rocksNumberBounds.y), transform.position));
                        break;
                }
                break;
            case State.TELEPORT:
                NewState(State.WAIT);
                break;
            case State.FIREBALL:
                break;
            case State.FIREMISSILE:
                break;
            case State.ROCKFALL:
                break;

        }
    }
    void NewState(State newState) {
        state = newState;
        if (state == State.WAIT)
            stateTimer = stateTime;
    }

    IEnumerator WeWillRockYou(int rockNumbers, Vector3 position) {
        for (int i = 0; i < rockNumbers; i++) {
            float randomDegree = Random.Range(0, 360);
            float randomDist = Random.Range(radiusBounds.x, radiusBounds.y);
            Vector3 destination = new Vector3(Mathf.Cos(randomDegree * Mathf.Deg2Rad), Mathf.Sin(randomDegree * Mathf.Deg2Rad), 0);
            destination = destination.normalized * randomDist;
            destination.z = position.z;

            FallingObject newFallingObject = Instantiate(fallingObject.gameObject, position + destination + new Vector3(0, apparitionHigh, 0), fallingObject.transform.rotation).GetComponent<FallingObject>();
            newFallingObject.SetFallen(rock.gameObject)
                .SetSprite(rock.sprite)
                .SetDestination(position + destination)
                .SetFallTime(fallingTime);
        }
        isShield = true;
        sr.color = Color.blue;
        yield return new WaitForSeconds(fallingTime);
        NewState(State.WAIT);

    }

    IEnumerator DeathSpin(float duration) {
        float fireRateTimer = magicBallRate;
        float durationTimer = duration;
        while (durationTimer > 0) {
            if (state == State.WAIT) {
                yield break;
            }
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
        NewState(State.WAIT);
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
            if (state == State.WAIT) {
                yield break;
            }
            fireRateTimer -= Time.deltaTime;
            if (fireRateTimer <= 0) {
                numberMissilesFired++;
                fireRateTimer = missileRate;
                float randomDegree = Random.Range(0, 360);
                float randomDist = Random.Range(radiusBounds.x, radiusBounds.y);
                Vector3 destination = new Vector3(Mathf.Cos(randomDegree * Mathf.Deg2Rad), Mathf.Sin(randomDegree * Mathf.Deg2Rad), 0);
                destination = destination.normalized * randomDist;
                destination.z = transform.position.z;
                FireMissile(missileSpeed, transform.position + destination.normalized * missileDistSpawn);
            }
            yield return null;
        }
        NewState(State.WAIT);
    }
    void UpdateFlee() {
        Vector3 playerPosition = player.transform.position;
        if ((playerPosition - transform.position).sqrMagnitude < fleeRadius && !isShield) {
            FleeFrom(player);
            NewState(State.TELEPORT);
        }
    }

    void FleeFrom(GameObject entity) {
        Vector3 entityPosition = entity.transform.position;
        Vector3 direction = (entityPosition - transform.position).normalized;
        Vector3 destination = direction * fleeDistance;
        Teleport(destination);
    }

    void Teleport(Vector3 position) {
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }

    IEnumerator Ouch() {
        damaged = true;
        sr.color = Color.red;
        Debug.Log("red");
        yield return new WaitForSeconds(damage);
        sr.color = isShield ? Color.blue : Color.green;
        damaged = false;
    }


    public void LoseLife(int life) {
        if (isShield) { return; }
        this.life -= life;
        StartCoroutine(Ouch());
        if (this.life <= 0) {
            Die();
        }
    }

    public void Die() {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Bullet") {
            LoseLife(1);
            Destroy(collision.gameObject);
        }
    }
}