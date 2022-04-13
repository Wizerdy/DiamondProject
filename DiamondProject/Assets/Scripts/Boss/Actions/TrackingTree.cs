using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingTree : BaseAttack
{
    [SerializeField] private int numberOfTreesToSpawn = 5;
    [SerializeField] private int treeHp = 10;
    [SerializeField] private float spawnSpeed = 1f;
    [SerializeField] private float treeDamage = 10f;
    [SerializeField] private float fireDamage = 10f;
    [SerializeField] private float fireDamageFrequency = 1f;
    [SerializeField] private float fireRadius = 5f;
    [SerializeField] private FireTree fireTree;

    //protected override IEnumerator Launch(Player player, Boss boss, Vector3 aimPosition, float duration) {

    //SpawnTree(Player player, aimPosition) {
    private void SpawnTree() {
        FireTree tree = Instantiate(fireTree, transform.position, Quaternion.identity);
        tree.Init(player, treeHp, treeDamage, fireDamage, fireDamageFrequency, fireRadius);
    }

    protected override IEnumerator Launch() {
        isPlaying = true;
        float spawnRate = spawnSpeed;
        int numberOfTreesSpawned = 0;

        while (numberOfTreesSpawned > numberOfTreesToSpawn) {
            spawnRate -= Time.deltaTime;
            if (spawnRate <= 0) {
                //SpawnTree(player, aimPosition);
                SpawnTree();

                spawnRate = spawnSpeed;
                ++numberOfTreesSpawned;
            }
            yield return null;
        }

        UpdateIA();
        isPlaying = false;
    }
}
