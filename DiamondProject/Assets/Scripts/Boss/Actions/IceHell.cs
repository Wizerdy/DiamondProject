using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PatternType { Scatter, Focus }
[System.Serializable]
struct Pattern {
    public float chargeTime;
    public float speed;
    public int numberOfWaves;
    public int numberOfIceShardsPerWave;
    public PatternType patternType;
}

//public struct Range {
//    public int min;
//    public int max;
//    public int range { get { return max - min + 1; } }
//    public Range(int aMin, int aMax) {
//        min = aMin; max = aMax;
//    }
//}

public class IceHell : BaseAttack {
    [Header("Pattern")]
    [SerializeField] private Pattern[] firstPatterns;
    [SerializeField] private Pattern[] secondPatterns;

    [Header("Spawner")]
    [SerializeField] private float _delayBetweenWaves = 0.1f;
    [SerializeField] private float _spawnDistanceFromBoss = 1.5f;
    [SerializeField] private int _gapWidth = 15;

    [Header("Shard")]
    [SerializeField] private float _shardLifetime = 5f;
    [SerializeField] private int _iceShardDamage = 10;

    [Header("Pas touche")]
    [SerializeField] private GameObject _iceShard;
    //[SerializeField] private BossReference _bossRef;
    [SerializeField] private Reference<Transform> _target;

    List<IceShard> _shards = new List<IceShard>();

    int _shardCount = 0;
    float _upperLimit;
    float _lowerLimit;
    float _currentShardAngle;
    float _offSetBetweenShard;

    Pattern[] _currentPattern;

    private void SpawnIceShard(PatternType _patternType, float _speed) {
        float rad = _currentShardAngle * Mathf.Deg2Rad;
        float X = Mathf.Cos(rad);
        float Y = Mathf.Sin(rad);

        Vector3 spawnDir = new Vector3(X, Y, 0);
        Vector3 shotDir = (spawnDir - transform.position).normalized;

        Vector3 spawnPos = _bossRef.Instance.transform.position + spawnDir * _spawnDistanceFromBoss;

        GameObject shard = Instantiate(_iceShard, spawnPos, Quaternion.Euler(0.0f, 0.0f, _currentShardAngle));
        IceShard iceShard = shard.GetComponent<IceShard>();

        iceShard.Init(ShardType.iceHell, _target?.Instance, _speed, _iceShardDamage, shotDir * _speed, _shardLifetime);

        _shards.Add(iceShard);
        _shardCount++;
        iceShard.OnShardDestroy += () => _shardCount--;

        if (_patternType == PatternType.Focus)
            _currentShardAngle += _offSetBetweenShard;
        else
            _currentShardAngle -= _offSetBetweenShard;
    }

    protected override IEnumerator ICast() {
        int randomPattern = Random.Range(1, 3);
        _currentPattern = (randomPattern == 1 ? firstPatterns : secondPatterns);

        for (int i = 0; i < _currentPattern.Length; i++) {
            float numberOfShardsSpawned = 0;
            while (numberOfShardsSpawned < _currentPattern[i].numberOfIceShardsPerWave) {
                SpawnIceShard(_currentPattern[i].patternType, _currentPattern[i].speed);
                ++numberOfShardsSpawned;
            }
        }
        yield return null;
    }

    protected override IEnumerator IExecute() {
        Vector3 playerPosition = _target.Instance?.position ?? Vector3.zero;
        Vector3 dirToPlayer = (Vector3)(playerPosition - _bossRef.Instance?.transform.position);
        dirToPlayer = _target.Instance.transform.InverseTransformDirection(dirToPlayer);
        float angleToPlayer = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x) * Mathf.Rad2Deg;

        _upperLimit = angleToPlayer + _gapWidth / 2;
        _lowerLimit = angleToPlayer - _gapWidth / 2;

        int randomPattern = Random.Range(1, 3);
        switch (randomPattern) {
            case 1:
                yield return StartCoroutine(UseShardPattern(firstPatterns));
                break;
            case 2:
                yield return StartCoroutine(UseShardPattern(secondPatterns));
                break;
            default:
                Debug.Log("ICE HELL PATTERN ERROR");
                break;
        }

        while (_shardCount > 0) {
            yield return null;
        }
    }

    IEnumerator UseShardPattern(Pattern[] _pattern) {
        float spawnRate = 0;

        for (int i = 0; i < _pattern.Length; i++) {
            CalculateCurrentShardAngle(_pattern, i);
            yield return new WaitForSeconds(_pattern[i].chargeTime);

            int numberOfWaves = 0;
            while (numberOfWaves < _pattern[i].numberOfWaves) {

                int numberOfShardsSpawned = 0;
                spawnRate -= Time.deltaTime;
                if (spawnRate <= 0) {
                    while (numberOfShardsSpawned < _pattern[i].numberOfIceShardsPerWave) {
                        SpawnIceShard(_pattern[i].patternType, _pattern[i].speed);
                        ++numberOfShardsSpawned;
                        yield return null;
                    }
                    CalculateCurrentShardAngle(_pattern, i);

                    spawnRate = _delayBetweenWaves;
                    ++numberOfWaves;
                    yield return null;

                }
                yield return null;
            }

        }
    }

    private void CalculateCurrentShardAngle(Pattern[] _patterns, int currentPattern) {
        if (_patterns[currentPattern].patternType == PatternType.Focus) {
            _offSetBetweenShard = _gapWidth / _patterns[currentPattern].numberOfIceShardsPerWave;
            _currentShardAngle = _lowerLimit + _offSetBetweenShard * 3;
        } else {
            _offSetBetweenShard = (360 - _gapWidth) / _patterns[currentPattern].numberOfIceShardsPerWave;
            _currentShardAngle = _upperLimit - _offSetBetweenShard;
        }
    }
}
