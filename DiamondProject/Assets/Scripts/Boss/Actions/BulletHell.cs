using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHell : BaseAttack {
    [Header("Spawner Parameter")]
    [SerializeField] private int numberOfWave = 9;
    [SerializeField] private int numberOfProjectilsPerWaves = 50;
    [SerializeField] private float delayBetweenWaves = 0.35f;
    [SerializeField] private float delayBetweenShards = 0.01f;
    [SerializeField] private float bulletPerRotation = 25f;
    [SerializeField] private float spawnDistance = 0.1f;
    [Range(0.0f, 0.99f)]
    [SerializeField] private float offSet = 0.15f;

    [Header("Shard Parameter")]
    [SerializeField] private int iceShardDamage = 5;
    [SerializeField] private float shardSpeed = 30f;
    [SerializeField] private float shardLifetime = 5f;

    [Header("Pas Touche")]
    [SerializeField] private GameObject _iceShard;
    [SerializeField] private Reference<Transform> _target;
    [SerializeField] private BossReference bossRef;
    private float acceleration = 10f;
    private float angle = 0f;
    private float offSetBase;

    int _shardCount = 0;

    private void SpawnIceShard(int numberOfShard) {

        float stepAngle = 360.0f - (360.0f / -bulletPerRotation);
        acceleration = -bulletPerRotation / 50.0f * offSetBase ;
        angle = Mathf.Sin(acceleration) * 360f;

        float shotDirX = transform.position.x + Mathf.Sin(((angle + stepAngle * numberOfShard) * Mathf.PI) / 180f);
        float shotDirY = transform.position.y + Mathf.Cos(((angle + stepAngle * numberOfShard) * Mathf.PI) / 180f);

        Vector3 shotMoveVector = new Vector3(shotDirX, shotDirY, 0f);
        Vector3 shotDir = (shotMoveVector - transform.position).normalized * shardSpeed;

        Vector3 spawnPos = bossRef.Instance.transform.position + shotDir * spawnDistance;
        GameObject shard = Instantiate(
            _iceShard,
            spawnPos, 
            Quaternion.Euler(0.0f, 0.0f, -(angle + stepAngle * numberOfShard) + 90)
            );
        IceShard iceShard = shard.GetComponent<IceShard>();
        if (iceShard == null) { return; }
        _shardCount++;
        iceShard.Init(ShardType.bulletHell, _target?.Instance, shardSpeed, iceShardDamage, -shotDir, shardLifetime);
        iceShard.OnShardDestroy += () => _shardCount--;
    }

    protected override IEnumerator IExecute() {
        float wavesSpawnRate = 0;
        float shardsSpawnRate = 0;
        int wavesSpawned = 0;

        while (wavesSpawned < numberOfWave) {
            int numberOfShardsSpawned = 0;
            wavesSpawnRate -= Time.deltaTime;
            if (wavesSpawnRate <= 0) {

                if (offSetBase < 1) 
                    offSetBase = offSetBase + offSet;
                else
                    offSetBase = offSetBase - offSet;

                while (numberOfShardsSpawned < numberOfProjectilsPerWaves) {
                    shardsSpawnRate -= Time.deltaTime;
                    if (shardsSpawnRate <= 0) {
                        SpawnIceShard(numberOfShardsSpawned);
                        ++numberOfShardsSpawned;
                        shardsSpawnRate = delayBetweenShards;
                    }
                    yield return null;
                }
                wavesSpawnRate = delayBetweenWaves;
                ++wavesSpawned;
                yield return null;
            }
            yield return null;
        }

        while(_shardCount > 0) {
            yield return null;
        }
    }
}
