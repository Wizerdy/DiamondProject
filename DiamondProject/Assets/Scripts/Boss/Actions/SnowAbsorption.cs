using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowAbsorption : BaseAttack {
    [Header("Spawner")]
    //[SerializeField] private int numberOfShardPerCircle = 20;
    //[SerializeField] private int numberOfShardToRemoveInCircle = 5;
    [SerializeField] private int numberOfCircleToSpawn = 10;
    [SerializeField] private float circleSpawnRatePerSecond = 1f;
    [SerializeField] private float delayBeforeLaunch = 1f;
    //[SerializeField] private float circleRadius = 15f;
    //[SerializeField] private float circleSpawnRangeRadius = 150f;

    [Header("IceWall")]
    //[SerializeField] private GameObject _iceShard;
    //[SerializeField] private float shardSpeed = 50f;
    //[SerializeField] private int shardDamage = 10;
    //[SerializeField] private float shardLifetime = 5f;
    [SerializeField] private GameObject iceWall;
    [SerializeField] private int segments = 10;
    [SerializeField] private float radius = 60f;
    [SerializeField] private float wallGapWidth = 40f;
    [SerializeField] private float wallSpeed = 5f;
    [SerializeField] private int wallDamage = 10;
    [SerializeField] private int circleOffSet = 10;

    //private float halfwidthEllipse = 50f;
    //private float halfheightEllipse = 30f;

    //private int _shardCount = 0;
    private int circleRotation;
    private void SpawnCircle() {
        GameObject _iceWall = Instantiate(iceWall, new Vector3 (0, 0, 0), Quaternion.Euler(0.0f, 0.0f, -circleRotation));
        IceWall wall = _iceWall.GetComponent<IceWall>();
        wall.Init(segments, radius, wallGapWidth, wallSpeed, wallDamage);
        
        //transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, -randomAngle);
        //Vector3 circleCenterPos = new Vector3(
        //    transform.position.x + halfwidthEllipse * Mathf.Cos(randomAngle * Mathf.PI / 180f),
        //    transform.position.x + halfheightEllipse * Mathf.Sin(randomAngle * Mathf.PI / 180f),
        //    0
        //    );

        //SpawnIceShardInCircle(circleCenterPos);

    }

    //private void SpawnIceShardInCircle(Vector3 circleCenter) {
    //    List<GameObject> listShard = new List<GameObject>();
    //    for (int i = 0; i < numberOfShardPerCircle; ++i) {
    //        float radians = 2 * Mathf.PI / numberOfShardPerCircle * i;
    //        float vertical = Mathf.Sin(radians);
    //        float horizontal = Mathf.Cos(radians);

    //        Vector3 spawnDir = new Vector3(horizontal, vertical, 0);
    //        Vector3 spawnPos = circleCenter + spawnDir * circleRadius;

    //        float angle = (Mathf.Atan2(spawnPos.x - transform.position.x, spawnPos.y - transform.position.y) * 180 / Mathf.PI + 630) % 360;
    //        GameObject shard = Instantiate(
    //            _iceShard, 
    //            spawnPos, 
    //            Quaternion.Euler(0.0f, 0.0f, -angle )
    //            );

    //        listShard.Add(shard);

    //        IceShard iceShard = shard.GetComponent<IceShard>();
    //        if (iceShard == null) { continue; }
    //        _shardCount++;
    //        iceShard.Init(transform, shardSpeed, shardDamage,  transform.position - spawnPos, shardLifetime);
    //        iceShard.OnShardDestroy += () => _shardCount--;
    //    }

    //    for (int i = 0; i < numberOfShardToRemoveInCircle; ++i) {
    //        int randomShard = Random.Range(0, listShard.Count);
    //        GameObject shardToRemove = listShard[randomShard];
    //        listShard.Remove(shardToRemove);
    //        shardToRemove.SetActive(false);
    //    }
    //}

    protected override IEnumerator IExecute() {
        isPlaying = true;
        float circlesSpawnRate = 0;
        int wavesSpawned = 0;
        circleRotation = Random.Range(0, 360);
        yield return new WaitForSeconds(delayBeforeLaunch);
        while (wavesSpawned < numberOfCircleToSpawn) {
            circlesSpawnRate -= Time.deltaTime;
            if (circlesSpawnRate <= 0) {
                SpawnCircle();
                if (Random.value < 0.5f)
                    circleRotation -= circleOffSet;
                else
                    circleRotation += circleOffSet;

                circlesSpawnRate = circleSpawnRatePerSecond;
                ++wavesSpawned;
                yield return null;
            }
            yield return null;
        }

        //while (_shardCount > 0) {
        //    yield return null;
        //}
    }
}
