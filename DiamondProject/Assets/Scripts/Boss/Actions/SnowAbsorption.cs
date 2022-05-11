using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowAbsorption : BaseAttack {

    [SerializeField] private int numberOfShardPerCircle = 20;
    [SerializeField] private int numberOfShardToRemoveInCircle = 5;
    [SerializeField] private int numberOfCircleToSpawn = 10;
    [SerializeField] private float circleSpawnRatePerSecond = 1f;
    [SerializeField] private float circleRadius = 15f;
    [SerializeField] private float circleSpawnRangeRadius = 150f;

    [Header("Shard")]
    [SerializeField] private GameObject _iceShard;
    [SerializeField] private float shardSpeed = 50f;
    [SerializeField] private int shardDamage = 10;
    [SerializeField] private float shardLifetime = 5f;

    private float halfwidthEllipse = 50f;
    private float halfheightEllipse = 30f;

    private int _shardCount = 0;

    private void SpawnCircle() {
        int randomAngle = Random.Range(0, 360);
        Vector3 circleCenterPos = new Vector3(
            transform.position.x + halfwidthEllipse * Mathf.Cos(randomAngle * Mathf.PI / 180f),
            transform.position.x + halfheightEllipse * Mathf.Sin(randomAngle * Mathf.PI / 180f),
            0
            );

        SpawnIceShardInCircle(circleCenterPos);
        
    }

    private void SpawnIceShardInCircle(Vector3 circleCenter) {
        List<GameObject> listShard = new List<GameObject>();
        for (int i = 0; i < numberOfShardPerCircle; ++i) {
            float radians = 2 * Mathf.PI / numberOfShardPerCircle * i;
            float vertical = Mathf.Sin(radians);
            float horizontal = Mathf.Cos(radians);

            Vector3 spawnDir = new Vector3(horizontal, vertical, 0);
            Vector3 spawnPos = circleCenter + spawnDir * circleRadius;

            //Vector3 dirToLookAt = transform.position - transform.position;
            //Quaternion lookRotation = Quaternion.LookRotation(dirToLookAt);
            //Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            //partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            //float a = Mathf.Atan2(transform.position.y - spawnPos.y, transform.position.x - spawnPos.y);
            //float b = Mathf.Atan2(spawnPos.y, spawnPos.x);
            //float angle = b - a;

            //float angle = Mathf.Atan2(transform.position.x - spawnPos.x, transform.position.y - spawnPos.y) * Mathf.Rad2Deg;
            float angle = (Mathf.Atan2(spawnPos.x - transform.position.x, spawnPos.y - transform.position.y) * 180 / Mathf.PI + 630) % 360;
            GameObject shard = Instantiate(
                _iceShard, 
                spawnPos, 
                Quaternion.Euler(0.0f, 0.0f, -angle )
                );

            listShard.Add(shard);

            IceShard iceShard = shard.GetComponent<IceShard>();
            if (iceShard == null) { continue; }
            _shardCount++;
            iceShard.Init(transform, shardSpeed, shardDamage,  transform.position - spawnPos, shardLifetime);
            iceShard.OnShardDestroy += () => _shardCount--;
        }

        for (int i = 0; i < numberOfShardToRemoveInCircle; ++i) {
            int randomShard = Random.Range(0, listShard.Count);
            GameObject shardToRemove = listShard[randomShard];
            listShard.Remove(shardToRemove);
            shardToRemove.SetActive(false);
        }
    }

    protected override IEnumerator IExecute() {
        isPlaying = true;
        float circlesSpawnRate = 0;
        int wavesSpawned = 0;

        while (wavesSpawned < numberOfCircleToSpawn) {
            circlesSpawnRate -= Time.deltaTime;
            if (circlesSpawnRate <= 0) {
                SpawnCircle();

                circlesSpawnRate = circleSpawnRatePerSecond;
                ++wavesSpawned;
                yield return null;
            }
            yield return null;
        }

        while (_shardCount > 0) {
            yield return null;
        }
    }
}
