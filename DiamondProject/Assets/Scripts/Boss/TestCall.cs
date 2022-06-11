using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCall : MonoBehaviour
{
    //[Header("Pattern")]
    //[SerializeField] private Pattern[] firstPatterns;
    //[SerializeField] private Pattern[] secondPatterns;

    //[Header("Spawner")]
    //[SerializeField] private float delayBetweenWaves = 1.5f;
    //[SerializeField] private float spawnDistanceFromBoss = 1.5f;
    //[SerializeField] private int gapWidth = 30;

    //[Header("Shard")]
    //[SerializeField] private float shardLifetime = 5f;
    //[SerializeField] private int iceShardDamage = 10;

    //[Header("Pas touche")]
    //[SerializeField] private GameObject iceShard;
    //[SerializeField] private BossReference bossRef;
    //[SerializeField] private Reference<Transform> _target;

    //int _shardCount = 0;
    //float upperLimit;
    //float lowerLimit;
    //float currentShardAngle;
    //float offSetBetweenShard;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    gameObject.SetActive(true);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.A)) {
    //        StartCoroutine(IExecute());
    //    }
    //}

    //private void SpawnIceShard(PatternType _patternType, float _speed) {
    //    float rad = currentShardAngle * Mathf.Deg2Rad;
    //    float X = Mathf.Cos(rad);
    //    float Y = Mathf.Sin(rad);

    //    Vector3 spawnDir = new Vector3(X, Y, 0);
    //    Vector3 shotDir = (spawnDir - transform.position).normalized;

    //    Vector3 spawnPos = bossRef.Instance.transform.position + spawnDir * spawnDistanceFromBoss;

    //    GameObject shard = Instantiate(iceShard, spawnPos, Quaternion.Euler(0.0f, 0.0f, currentShardAngle));
    //    IceShard _iceShard = shard.GetComponent<IceShard>();

    //    _iceShard.Init(_target?.Instance, _speed, iceShardDamage, shotDir * _speed, shardLifetime);

    //    _shardCount++;
    //    _iceShard.OnShardDestroy += () => _shardCount--;

    //    if (_patternType == PatternType.Focus)
    //        currentShardAngle += offSetBetweenShard;
    //    else
    //        currentShardAngle -= offSetBetweenShard;
    //}


    //public IEnumerator IExecute() {
    //    Debug.Log("START");
    //    Vector3 playerPosition = _target.Instance?.position ?? Vector3.zero;
    //    Vector3 dirToPlayer = (Vector3)(playerPosition - bossRef.Instance?.transform.position);
    //    dirToPlayer = _target.Instance.transform.InverseTransformDirection(dirToPlayer);
    //    float angleToPlayer = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x) * Mathf.Rad2Deg;

    //    upperLimit = angleToPlayer + gapWidth / 2;
    //    lowerLimit = angleToPlayer - gapWidth / 2;

    //    int randomPattern = Random.Range(1, 3);
    //    switch (randomPattern) {
    //        case 1:
    //            yield return StartCoroutine(UseShardPattern(firstPatterns));
    //            break;
    //        case 2:
    //            yield return StartCoroutine(UseShardPattern(secondPatterns));
    //            break;
    //        default:
    //            Debug.Log("ICE HELL PATTERN ERROR");
    //            break;
    //    }

    //    while (_shardCount > 0) {
    //        yield return null;
    //    }
    //}

    //IEnumerator UseShardPattern(Pattern[] _pattern) {
    //    float spawnRate = 0;

    //    for (int i = 0; i < _pattern.Length; i++) {
    //        CalculateCurrentShardAngle(_pattern, i);
    //        yield return new WaitForSeconds(_pattern[i].chargeTime);

    //        int numberOfWaves = 0;
    //        while (numberOfWaves < _pattern[i].numberOfWaves) {

    //            int numberOfShardsSpawned = 0;
    //            spawnRate -= Time.deltaTime;
    //            if (spawnRate <= 0) {
    //                while (numberOfShardsSpawned < _pattern[i].numberOfIceShardsPerWave) {
    //                    SpawnIceShard(_pattern[i].patternType, _pattern[i].speed);
    //                    ++numberOfShardsSpawned;
    //                    yield return null;
    //                }
    //                CalculateCurrentShardAngle(_pattern, i);

    //                spawnRate = delayBetweenWaves;
    //                ++numberOfWaves;
    //                yield return null;

    //            }
    //            yield return null;
    //        }

    //    }
    //}

    //private void CalculateCurrentShardAngle(Pattern[] _patterns, int currentPattern) {
    //    if (_patterns[currentPattern].patternType == PatternType.Focus) {
    //        offSetBetweenShard = gapWidth / _patterns[currentPattern].numberOfIceShardsPerWave;
    //        currentShardAngle = lowerLimit + offSetBetweenShard * 3;
    //    } else {
    //        offSetBetweenShard = (360 - gapWidth) / _patterns[currentPattern].numberOfIceShardsPerWave;
    //        currentShardAngle = upperLimit - offSetBetweenShard;
    //    }
    //}
}
