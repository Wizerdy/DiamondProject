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

    [SerializeField] private float iceShardDamage = 10f;
    [SerializeField] private float delayBetweenWaves = 1.5f;
    [SerializeField] private float delayBetweenShards = 0.5f;

    [SerializeField] private float angleOffSet = 10f;

    [SerializeField] private GameObject iceShard;

    //protected override IEnumerator Launch(Player player, Boss boss, Vector3 aimPosition, float duration) {

    //SpawnTree(Player player, aimPosition) {
    private void SpawnIceShard(PatternType _patternType, float _speed) {
        //GameObject tree = Instantiate(fireTree, transform.position, Quaternion.identity);
        //tree.GetComponent<FireTree>().Init(player, treeHp, treeDamage, fireDamage, fireDamageFrequency, fireRadius);
        GameObject shard = Instantiate(iceShard, transform.position, Quaternion.identity);
        Vector3 playerPosition = _target.Instance?.position ?? Vector3.zero;
        if (_patternType == PatternType.Focus) {
            Vector3 dir = _target?.Instance.position ?? Vector3.up;
            shard.GetComponent<IceShard>().Init(_target?.Instance, _speed, iceShardDamage, dir);
        }

        if (_patternType == PatternType.Scatter) {
            var angleLimit = Mathf.Atan2(transform.position.x - playerPosition.x, transform.position.y - playerPosition.y) * 180 / Mathf.PI;
            float minLimit = angleLimit - angleOffSet;
            float maxLimit = angleLimit + angleOffSet;
            int val = RandomValueFromRanges(new Range(0, (int)minLimit), new Range((int)maxLimit, 360));

            float newX = Mathf.Cos(val);
            float newY = Mathf.Sin(val);
            Vector3 dir = new Vector3(newX, newY, 0);

            shard.GetComponent<IceShard>().Init(_target?.Instance, _speed, iceShardDamage, dir);
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
        isPlaying = true;
        float spawnRate = 0;
        float shardsSpawnRate = 0;
        int randomPattern = Random.Range(1, 3);
        switch (randomPattern) {
            case 1:
                for (int i = 0; i < firstPatterns.Length; i++) {
                    yield return new WaitForSeconds(firstPatterns[i].chargeTime);

                    int numberOfWaves = 0;
                    while (numberOfWaves < firstPatterns[i].numberOfWaves) {
                        int numberOfShardsSpawned = 0;
                        spawnRate -= Time.deltaTime;
                        if (spawnRate <= 0) {
                            while (numberOfShardsSpawned < firstPatterns[i].numberOfIceShardsPerWave) {
                                shardsSpawnRate -= Time.deltaTime;
                                if (shardsSpawnRate <= 0) {
                                    SpawnIceShard(firstPatterns[i].patternType, firstPatterns[i].speed);
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
                break;

            case 2:
                for (int i = 0; i < secondPatterns.Length; i++) {
                    yield return new WaitForSeconds(secondPatterns[i].chargeTime);

                    int numberOfWaves = 0;
                    while (numberOfWaves < secondPatterns[i].numberOfWaves) {
                        int numberOfShardsSpawned = 0;
                        spawnRate -= Time.deltaTime;
                        if (spawnRate <= 0) {
                            while (numberOfShardsSpawned < secondPatterns[i].numberOfIceShardsPerWave) {
                                shardsSpawnRate -= Time.deltaTime;
                                if (shardsSpawnRate <= 0) {
                                    SpawnIceShard(secondPatterns[i].patternType, secondPatterns[i].speed);
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
                break;
        }


        //UpdateIA();
        isPlaying = false;
        yield return null;

    }
}
