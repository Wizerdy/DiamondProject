using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour {
    public GameObject theFallen = null;
    public GameObject _shadow = null;
    public Sprite sprite = null;
    public Vector3 destination = Vector3.zero;
    public float fallTime = 2f;
    float fallTimer = 0f;
    Vector3 initialPosition = Vector3.zero;

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
        _shadow.transform.position = destination;
        _shadow.transform.parent = null;
    }

    void Update() {
        Falling();
    }

    void Falling() {
        fallTimer -= Time.deltaTime;
        transform.position = Vector3.Lerp(destination, initialPosition, fallTimer/ fallTime);
        if (fallTimer/ fallTime <= 0.33f) {
            _shadow.transform.localScale = Vector3.one * Mathf.Lerp(0,1,Mathf.InverseLerp(0.33f, 0, fallTimer / fallTime));
        }
        if (fallTimer < 0f) {
            SpawnFallen(destination);
            Die();
        }
    }

    void SpawnFallen(Vector3 position) {
        theFallen.SetActive(true);
        theFallen.transform.parent = null;
        theFallen.transform.position = position;
    }

    void Die() {
        Destroy(gameObject);
    }
}
