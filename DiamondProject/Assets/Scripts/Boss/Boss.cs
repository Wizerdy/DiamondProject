using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    [SerializeField]
    GameObject missile;
    [SerializeField]
    GameObject magicBalls;

    [Header("RockFall")]
    [SerializeField]
    FallingObject fallingObject = null;
    [SerializeField]
    public Rock rock;
    [SerializeField]
    public float fallingTime = 3f;
    [SerializeField]
    List<FallingObject> fallingRocks = new List<FallingObject>();
    [SerializeField]
    Vector2 radiusBounds;
    [SerializeField]
    float apparitionHigh = 10;
    [SerializeField]
    Vector2Int rocksNumberBounds;


    enum State {
        WAIT,
        TELEPORT,
        ROCKFALL,
        FIRE,
    }

    State state;

    private void Start() {
        WeWillRockYou(Random.Range(rocksNumberBounds.x, rocksNumberBounds.y + 1), transform.position);
    }
    void Update() {
        
    }

    void WeWillRockYou(int rockNumbers, Vector3 position) {
        for (int i = 0; i < rockNumbers; i++) {
            float randomDegree = Random.Range(0, 360);
            float randomDist = Random.Range(radiusBounds.x, radiusBounds.y);
            Vector3 destination = new Vector3(Mathf.Cos(randomDegree * Mathf.Deg2Rad), Mathf.Sin(randomDegree * Mathf.Deg2Rad), 0);
            destination = destination.normalized * randomDist;
            Debug.Log(destination);
            Debug.Log(randomDist);
            destination.z = position.z;

            FallingObject newFallingObject = Instantiate(fallingObject.gameObject, position + destination + new Vector3(0,apparitionHigh,0), fallingObject.transform.rotation).GetComponent<FallingObject>();
            newFallingObject.SetFallen(rock.gameObject)
                .SetSprite(rock.sprite)
                .SetDestination(destination)
                .SetFallTime(fallingTime);
            fallingRocks.Add(newFallingObject);
        }
    }
}
