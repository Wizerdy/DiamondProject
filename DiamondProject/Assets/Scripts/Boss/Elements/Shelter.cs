using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelter : MonoBehaviour {
    [SerializeField] protected BossReference bossRef;
    [SerializeField] string resistance;
    private void OnTriggerEnter2D(Collider2D collision) {
        collision.GetComponent<Health>()?.AddResistance(resistance);
    }
    private void OnTriggerExit2D(Collider2D collision) {
        collision.GetComponent<Health>()?.RemoveResistance(resistance);
    }
    void Start() {
        bossRef.Instance.todestroyondeath.Add(this.gameObject);
    }
}
