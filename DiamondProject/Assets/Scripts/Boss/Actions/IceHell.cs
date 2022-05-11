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

public struct Range {
    public int min;
    public int max;
    public int range { get { return max - min + 1; } }
    public Range(int aMin, int aMax) {
        min = aMin; max = aMax;
    }
}

public class IceHell : BaseAttack {
    [SerializeField] private Reference<Transform> _target;
    [SerializeField] private Pattern[] firstPatterns;
    [SerializeField] private Pattern[] secondPatterns;

    [SerializeField] private float shardLifetime = 5f;
    [SerializeField] private int iceShardDamage = 10;
    [SerializeField] private float delayBetweenWaves = 1.5f;
    [SerializeField] private float delayBetweenShards = 0.5f;

    [SerializeField] private float angleOffSet = 10f;
    [SerializeField] private float spawnDistance = 0.5f;

    [SerializeField] private GameObject iceShard;

    int _shardCount = 0;

    private void SpawnIceShard(PatternType _patternType, float _speed) {
        GameObject shard = Instantiate(iceShard, transform.position, Quaternion.identity);
        Vector3 playerPosition = _target.Instance?.position ?? Vector3.zero;
        if (_patternType == PatternType.Focus) {
            Vector3 dir = _target?.Instance.position ?? Vector3.up;

            Vector3 shotDir = (dir - transform.position).normalized * _speed;
            Vector3 spawnPos = transform.position + shotDir * spawnDistance;
            shard.transform.position = spawnPos;

            IceShard iceShard = shard.GetComponent<IceShard>();
            iceShard.Init(_target?.Instance, _speed, iceShardDamage, dir, shardLifetime, true);
            _shardCount++;
            iceShard.OnShardDestroy += () => _shardCount--;
        } else if (_patternType == PatternType.Scatter) {
            var angleLimit = Mathf.Atan2(transform.position.x - playerPosition.x, transform.position.y - playerPosition.y) * 180 / Mathf.PI;
            float minLimit = angleLimit - angleOffSet;
            float maxLimit = angleLimit + angleOffSet;
            int val = RandomValueFromRanges(new Range(0, (int)minLimit), new Range((int)maxLimit, 360));

            float newX = Mathf.Cos(val);
            float newY = Mathf.Sin(val);
            Vector3 dir = new Vector3(newX, newY, 0);

            Vector3 shotDir = (dir - transform.position).normalized * _speed;
            Vector3 spawnPos = transform.position + shotDir * spawnDistance;
            shard.transform.position = spawnPos;

            float angle = (Mathf.Atan2(transform.position.x - dir.x, transform.position.y - dir.y) * 180 / Mathf.PI + 630) % 360;
            shard.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -angle);

            IceShard iceShard = shard.GetComponent<IceShard>();
            iceShard.Init(_target?.Instance, _speed, iceShardDamage, dir, shardLifetime);
            _shardCount++;
            iceShard.OnShardDestroy += () => _shardCount--;
        }
    }

    public int RandomValueFromRanges(params Range[] ranges) {
        if (ranges.Length == 0)
            return 0;
        int count = 0;
        foreach (Range r in ranges)
            count += r.range;
        int sel = Random.Range(0, count);
        foreach (Range r in ranges) {
            if (sel < r.range) {
                return r.min + sel;
            }
            sel -= r.range;
        }
        return 0;
    }

    protected override IEnumerator IExecute() {
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
        float shardsSpawnRate = 0;
        for (int i = 0; i < _pattern.Length; i++) {
            yield return new WaitForSeconds(_pattern[i].chargeTime);

            int numberOfWaves = 0;
            while (numberOfWaves < _pattern[i].numberOfWaves) {
                int numberOfShardsSpawned = 0;
                spawnRate -= Time.deltaTime;
                if (spawnRate <= 0) {
                    while (numberOfShardsSpawned < _pattern[i].numberOfIceShardsPerWave) {
                        shardsSpawnRate -= Time.deltaTime;
                        if (shardsSpawnRate <= 0) {
                            SpawnIceShard(_pattern[i].patternType, _pattern[i].speed);
                            ++numberOfShardsSpawned;
                            shardsSpawnRate = delayBetweenShards;
                        }
                        yield return null;
                    }
                    spawnRate = delayBetweenWaves;
                    ++numberOfWaves;
                    yield return null;

                }
                yield return null;
            }
        }
    }
}
