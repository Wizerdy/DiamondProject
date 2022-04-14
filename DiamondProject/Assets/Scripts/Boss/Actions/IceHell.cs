using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PatternType {Scatter, Focus}
[System.Serializable]
struct Pattern {
    public float chargeTime;
    public float speed;
    public int numberOfWaves;
    public int numberOfIceShardsPerWave;
    public PatternType patternType;
}
public class IceHell : BaseAttack
{
    [SerializeField] private Pattern[] patterns;

    [SerializeField] private float iceShardSpeed = 25f;
    [SerializeField] private float iceShardDamage = 10f;

    [SerializeField] private GameObject iceShard;

    //protected override IEnumerator Launch(Player player, Boss boss, Vector3 aimPosition, float duration) {

    //SpawnTree(Player player, aimPosition) {
    private void SpawnTree() {
        //GameObject tree = Instantiate(fireTree, transform.position, Quaternion.identity);
        //tree.GetComponent<FireTree>().Init(player, treeHp, treeDamage, fireDamage, fireDamageFrequency, fireRadius);
    }

    protected override IEnumerator Launch() {
        isPlaying = true;
        //float spawnRate = spawnSpeed;
        //int numberOfTreesSpawned = 0;

        //while (numberOfTreesSpawned < numberOfTreesToSpawn) {
        //    spawnRate -= Time.deltaTime;
        //    if (spawnRate <= 0) {
        //        //SpawnTree(player, aimPosition);
        //        SpawnTree();

        //        spawnRate = spawnSpeed;
        //        ++numberOfTreesSpawned;
        //    }
            yield return null;
        //}

        UpdateIA();
        isPlaying = false;
    }
}
