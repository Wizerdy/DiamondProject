using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackersTrees : BaseAttack {
    [SerializeField] protected PlayerControllerReference _player;
    [SerializeField] protected float spawnRate = 1f;
    [SerializeField] protected float spawnTime = 1f;
    [SerializeField] protected float lifetimeTree = 1f;
    [SerializeField] protected float treeLife = 1f;
    [SerializeField] protected int treeDamage = 1;
    [SerializeField] Vector2 treeBounds = Vector2.zero;
    [Header("For Prog: ")]
    [SerializeField] BossTree bossTree;
    void SpawnTree(float spawnTime, float zoneDamageTime, Vector3 position, int life) {
        BossTree newTree = Instantiate(bossTree.gameObject, position, Quaternion.identity).GetComponent<BossTree>();
        newTree.SetDestination(position)
            .SetApparitionTime(spawnTime)
            .SetZoneDamageTime(zoneDamageTime)
            .SetLife(life);
        newTree.enabled = true;
    }
    protected override IEnumerator IExecute() {
        isPlaying = true;
        float randomTrees = Random.Range(treeBounds.x, treeBounds.y);
        float numberTreesFired = 0;
        while (randomTrees > numberTreesFired) {
            numberTreesFired++;
            SpawnTree(spawnTime, 0, _player.Instance.transform.position, treeDamage);
            yield return new WaitForSeconds(spawnRate);
        }
        isPlaying = false;
        yield return null;
    }
}
