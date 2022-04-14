using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShard : MonoBehaviour
{
    [SerializeField] private float shardSpeed = 10f;
    [SerializeField] private float shardDamage = 5f;

    [SerializeField] private Vector3 aimDir = new Vector3(0, 0, 0);

    [SerializeField] delegate void OnShardPlayerHitEvent();
    OnShardPlayerHitEvent onShardPlayerHitEvent;
    [SerializeField] delegate void OnShardSpawnEvent();
    OnShardSpawnEvent onShardSpawnEvent;

    private Transform target;
    private Rigidbody2D rb;

    public void Init(Transform _target, float _ShardSpeed, float _ShardDamage, Vector3 _aimDir) {
        target = _target;
        shardSpeed = _ShardSpeed;
        shardDamage = _ShardDamage;
        aimDir = _aimDir;
    }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        onShardPlayerHitEvent += OnSpawn;
        onShardSpawnEvent += OnShardHitPlayer;

        onShardSpawnEvent?.Invoke();
    }

    private void FixedUpdate() {
        rb.velocity = aimDir.normalized * shardSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            //player.TakeDamage(damage);
            Debug.Log("Player took " + shardDamage + " damage");
            onShardPlayerHitEvent?.Invoke();
            Destroy(gameObject);
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
