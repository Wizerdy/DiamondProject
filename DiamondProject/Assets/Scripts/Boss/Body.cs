using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour {
    public Transform Transform { get; private set; }
    public HealthBar Health;

    private void Awake() {
        Transform = GetComponent<Transform>();
    }
}
