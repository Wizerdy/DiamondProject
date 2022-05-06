using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrambleBallEntity : MonoBehaviour
{
    [SerializeField] private float lifeSpan = 20f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private int damage = 20;

    [SerializeField] delegate void OnBallPlayerHitEvent();
    OnBallPlayerHitEvent onBallPlayerHitEvent;
    [SerializeField] delegate void OnBallSpawnEvent();
    OnBallSpawnEvent onBallSpawnEvent;

    //private Player player;
    private Vector3 aimPosition;
    private Rigidbody2D rb;
    private float _lifeTimer;

    public void Init(float _speed, int _damage, Vector3 _aimPosition) {
        speed = _speed;
        damage = _damage;
        aimPosition = _aimPosition;
    }
    // Start is called before the first frame update
    void Start()
    {
        onBallPlayerHitEvent += OnPlayerHit;
        onBallSpawnEvent += OnSpawn;

        rb = GetComponent<Rigidbody2D>();


        onBallSpawnEvent?.Invoke();
    }

    private void FixedUpdate() {
        rb.velocity = aimPosition.normalized * speed;
    }

    private void Update() {
        _lifeTimer -= Time.deltaTime;
        if (_lifeTimer <= 0) {
            KillBall();
        }
    }

    private void KillBall() {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<IHealth>()?.TakeDamage(damage);
            onBallPlayerHitEvent?.Invoke();
            KillBall();
        }
    }

    private void OnSpawn() {

    }

    private void OnPlayerHit() {

    }

    private void OnDestroy() {
        onBallPlayerHitEvent -= OnPlayerHit;
        onBallSpawnEvent -= OnSpawn;
    }
}
