using System.Collections;
using System.Collections.Generic;
using ToolsBoxEngine;
using UnityEngine;
using UnityEngine.Events;

public class ProtectorTree : MonoBehaviour {
    [SerializeField] int health;
    [SerializeField] Vector3 destination;
    [SerializeField] float apparitionTime;
    [SerializeField] GameObject treeBase;
    [SerializeField] public UnityEvent<ProtectorTree> onDeath;

    public ProtectorTree SetDestination(Vector3 destination) {
        this.destination = destination;
        return this;
    }

    public ProtectorTree SetApparitionTime(float apparitionTime) {
        this.apparitionTime = apparitionTime;
        return this;
    }

    public ProtectorTree SetHealth(int health) {
        this.health = health;
        return this;
    }

    private void Start() {
        transform.position = destination;
        StartCoroutine(Spawning());
        GetComponent<Health>().TakeHeal(health);
        GetComponent<Health>().SetMaxHealth(health);
    }
    IEnumerator Spawning() {
        float timer = apparitionTime;
        yield return new WaitForSeconds(timer);
        Spawn();
    }
    void Spawn() {
        treeBase.SetActive(true);
    }
    public void Die() {
        onDeath?.Invoke(this);
        Destroy(gameObject);
    }
}
