using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour {
    public GameObject theFallen = null;
    public Sprite sprite = null;
    public Vector3 destination = Vector3.zero;
    public float fallTime = 2f;
    float fallTimer = 0f;
    Vector3 initialPosition = Vector3.zero;


    void Start() {
        fallTimer = fallTime;
        initialPosition = transform.position;
    }

    void Update() {
        Falling();
    }

    void Falling() {
        if(fallTime == 0) {
            SpawnFallen(destination);
            Die();
            return;
        }
        fallTimer -= Time.deltaTime;
        transform.position = Vector3.Lerp(destination, initialPosition, fallTimer/ fallTime);
        if (fallTimer < 0f) {
            SpawnFallen(destination);
            Die();
        }
    }

    void SpawnFallen(Vector3 position) {
        Instantiate(theFallen, position, Quaternion.identity);
    }

    void Die() {
        Destroy(gameObject);
    }
}
