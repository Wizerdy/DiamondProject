using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    public GameObject missile;
    public GameObject magicBalls;

    [Header("RockFall")]
    public FallingObject fallingRock;
    [SerializeField]
    Transform groundZone1;
    [SerializeField]
    Transform groundZone2;
    [SerializeField]
    Transform groundZone3;
    [SerializeField]
    Transform groundZone4;
    [SerializeField]
    Vector2 rocksNumberBounds;


    enum State {
        WAIT,
        TELEPORT,
        ROCKFALL,
        FIRE,
    }

    State state;

    void Update() {
        
    }

    void WeWillRockYou(int rockNumbers, Vector3 position, float radius) {
        
    }



}
