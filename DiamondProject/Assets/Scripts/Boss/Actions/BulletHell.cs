using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHell : BaseAttack {
    [SerializeField] private Reference<Transform> _target;

    [SerializeField] private int iceShardDamage = 5;
    [SerializeField] private int numberOfWave = 12;
    [SerializeField] private int numberOfProjectilsPerWaves = 20;
    [SerializeField] private float delayBetweenWaves = 1.5f;
    [SerializeField] private float delayBetweenShards = 0.5f;
    [SerializeField] private float rotationSpeed = 15f;
    //[SerializeField] private float spinDuration = 5f;
    [SerializeField] private float shardSpeed = 10f;
    [SerializeField] private float spawnDistance = 3f;
    private float acceleration = 10f;
    private float angle = 0f;

    [SerializeField] private GameObject iceShard;

    private void SpawnIceShard(int numberOfShard) {
        float stepAngle = 360.0f - (360.0f / -rotationSpeed);
        acceleration = -rotationSpeed / 50.0f;
        angle = Mathf.Sin(acceleration) * 360f;
        //angle = Mathf.Sin(acceleration) * 360f * spinDuration;
        //angle += acceleration * 360f * spinDuration;

        float shotDirX = transform.position.x + Mathf.Sin(((angle + stepAngle * numberOfShard) * Mathf.PI) / 180f);
        float shotDirY = transform.position.y + Mathf.Cos(((angle + stepAngle * numberOfShard) * Mathf.PI) / 180f);

        Vector3 shotMoveVector = new Vector3(shotDirX, shotDirY, 0f);
        Vector3 shotDir = (shotMoveVector - transform.position).normalized * shardSpeed;

        Vector3 spawnPos = transform.position + shotDir * spawnDistance;
        GameObject shard = Instantiate(
            iceShard,
            -spawnPos, 
            Quaternion.Euler(0.0f, 0.0f, -(angle + stepAngle * numberOfShard) + 90)
            );

        shard.GetComponent<IceShard>().Init(_target?.Instance, shardSpeed, iceShardDamage, -shotDir);
    }

    protected override IEnumerator IExecute() {
        isPlaying = true;
        float wavesSpawnRate = 0;
        float shardsSpawnRate = 0;
        int wavesSpawned = 0;

        while (wavesSpawned < numberOfWave) {
            int numberOfShardsSpawned = 0;
            wavesSpawnRate -= Time.deltaTime;
            if (wavesSpawnRate <= 0) {
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

        //UpdateIA();
        isPlaying = false;
        yield return null;

    }
}
