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
    public FallingObject(GameObject theFallen, Sprite sprite, Vector3 destination, float fallTime) {
        this.theFallen = theFallen;
        this.sprite = sprite;
        this.destination = destination;
        this.fallTime = fallTime;
    }

    #region Builder

    public FallingObject SetDestination(Vector3 destination) {
        this.destination = destination;
        return this;
    }

    public FallingObject SetFallTime(float fallTime) {
        this.fallTime = fallTime;
        return this;
    }

    public FallingObject SetSprite(Sprite sprite) {
        this.sprite = sprite;
        return this;
    }

    public FallingObject SetFallen(GameObject fallen) {
        theFallen = fallen;
        return this;
    }

    #endregion

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
        Instantiate(theFallen, position, theFallen.transform.rotation);
    }

    void Die() {
        Destroy(gameObject);
    }
}
