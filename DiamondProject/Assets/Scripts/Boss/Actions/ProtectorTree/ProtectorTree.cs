using System.Collections;
using System.Collections.Generic;
using ToolsBoxEngine;
using UnityEngine;

public class ProtectorTree : MonoBehaviour {
    [SerializeField] TreeShield treeShield;
    [SerializeField] float health;
    [SerializeField] Vector3 destination;
    [SerializeField] float apparitionTime;
    [SerializeField] GameObject treeBase;

    public ProtectorTree SetDestination(Vector3 destination) {
        this.destination = destination;
        return this;
    }

    public ProtectorTree SetApparitionTime(float apparitionTime) {
        this.apparitionTime = apparitionTime;
        return this;
    }

    public ProtectorTree SetHealth(float health) {
        this.health = health;
        return this;
    }

    private void Start() {
        treeShield.AddTree(this);
        transform.position = destination;
        StartCoroutine(Spawning());
    }
    IEnumerator Spawning() {
        float timer = apparitionTime;
        yield return new WaitForSeconds(timer);
        Spawn();
    }
    void Spawn() {
        treeBase.SetActive(false);
    }
    void Die() {
        treeShield.RemoveTree(this);
        Destroy(gameObject);
    }

    public void AddTreeShield(TreeShield treeShield) {
        this.treeShield = treeShield;
    }
}
