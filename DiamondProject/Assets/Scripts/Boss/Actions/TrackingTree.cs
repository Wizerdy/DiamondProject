using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingTree : BaseAttack
{
    [SerializeField] Reference<Transform> _target;
    [SerializeField] private int numberOfTreesToSpawn = 5;
    [SerializeField] private int treeHp = 10;
    [SerializeField] private float spawnSpeed = 1f;
    [SerializeField] private int treeDamage = 10;
    [SerializeField] private int fireDamage = 10;
    [SerializeField] private float fireDamageFrequency = 1f;
    [SerializeField] private float fireRadius = 5f;
    [SerializeField] private GameObject fireTree;

    //protected override IEnumerator Launch(Player player, Boss boss, Vector3 aimPosition, float duration) {

    //SpawnTree(Player player, aimPosition) {
    private void SpawnTree() {
        GameObject tree = Instantiate(fireTree, transform.position, Quaternion.identity);
        tree.GetComponent<FireTree>().Init(_target?.Instance, treeHp, treeDamage, fireDamage, fireDamageFrequency, fireRadius);
    }

    protected override IEnumerator IExecute() {
        isPlaying = true;
        float spawnRate = spawnSpeed;
        int numberOfTreesSpawned = 0;

        while (numberOfTreesSpawned < numberOfTreesToSpawn) {
            spawnRate -= Time.deltaTime;
            if (spawnRate <= 0) {
                //SpawnTree(player, aimPosition);
                SpawnTree();

                spawnRate = spawnSpeed;
                ++numberOfTreesSpawned;
            }
            yield return null;
        }

        //UpdateIA();
        isPlaying = false;
    }
}
