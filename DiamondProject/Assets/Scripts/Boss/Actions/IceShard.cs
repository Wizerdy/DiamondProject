using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShard : MonoBehaviour
{
    [SerializeField] private float lifeSpan = 15f;
    [SerializeField] private float maxSize = 3f;
    [SerializeField] private float growthSpeed = 1f;

    private Vector3 aimDir = new Vector3(0, 0, 0);

    [SerializeField] delegate void OnShardPlayerHitEvent();
    OnShardPlayerHitEvent onShardPlayerHitEvent;
    [SerializeField] delegate void OnShardSpawnEvent();
    OnShardSpawnEvent onShardSpawnEvent;

    private float shardSpeed = 10f;
    private int shardDamage = 5;
    private Transform target;
    private Rigidbody2D rb;
    private float _lifeTimer;
    private Vector3 size;
    private bool isTargetingPlayer = false;
    private bool canMove = false;

    public void Init(Transform _target, float _ShardSpeed, int _ShardDamage, Vector3 _aimDir) {
        target = _target;
        shardSpeed = _ShardSpeed;
        shardDamage = _ShardDamage;
        aimDir = _aimDir;
    }

    //method overload
    public void Init(Transform _target, float _ShardSpeed, int _ShardDamage, Vector3 _aimDir, bool _isTargetingPlayer) {
        target = _target;
        shardSpeed = _ShardSpeed;
        shardDamage = _ShardDamage;
        aimDir = _aimDir;
        isTargetingPlayer = _isTargetingPlayer;
    }

    private void Start() {
        canMove = false;
        rb = GetComponent<Rigidbody2D>();

        //transform.localScale = new Vector3(0,0,1);
        //StartCoroutine(Growth());
        onShardPlayerHitEvent += OnSpawn;
        onShardSpawnEvent += OnShardHitPlayer;

        onShardSpawnEvent?.Invoke();

        _lifeTimer = lifeSpan;
        size = new Vector3(maxSize, maxSize, 1);
    }

    private void FixedUpdate() {
        if (canMove)
            rb.velocity = aimDir.normalized * shardSpeed;
    }

    private void Update() {
        if (isTargetingPlayer && !canMove) {
            float angle = (Mathf.Atan2(transform.position.x - target.transform.position.x, transform.position.y - target.transform.position.y) * 180 / Mathf.PI + 630) % 360;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, -angle);

            Vector3 dir = new Vector3(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y, 0);
            aimDir = dir;
        }

        transform.localScale = Vector3.Lerp(transform.localScale, size, growthSpeed * Time.deltaTime);
        if (transform.localScale.x > maxSize - 0.25f) {
            canMove = true;

            _lifeTimer -= Time.deltaTime;
            if (_lifeTimer <= 0) {
                KillShard();
            }
        }

    }
    //private IEnumerator Growth() {
    //    while(transform.localScale.x < maxSize) {
    //        Vector3 growth = new Vector3(transform.localScale.x + growthSpeed * Time.deltaTime, transform.localScale.y + growthSpeed * Time.deltaTime, 1
    //            );
    //        transform.localScale = growth;
    //        yield return null;
    //    }
    //    canMove = true;
    //}

    private void KillShard() {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<IHealth>()?.TakeDamage(shardDamage);
            onShardPlayerHitEvent?.Invoke();
            KillShard();
        }
    }

    private void OnSpawn() {

    }

    private void OnShardHitPlayer() {
        
    }


    private void OnDestroy() {
        onShardSpawnEvent -= OnSpawn;
        onShardPlayerHitEvent -= OnShardHitPlayer;

    }
}
