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
    Transform cornerLeftTop; 
    [SerializeField]
    Transform cornerRightTop; 
    [SerializeField]
    Transform cornerLeftBot; 
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
        WeWillRockYou(Random.Range(rocksNumberBounds.x, rocksNumberBounds.y + 1), transform.position, 2);
        WeWillRockYou(Random.Range(rocksNumberBounds.x, rocksNumberBounds.y + 1), transform.position, 2);
        WeWillRockYou(Random.Range(rocksNumberBounds.x, rocksNumberBounds.y + 1), transform.position, 2);
    }
    void Update() {
        
    }

    void WeWillRockYou(int rockNumbers, Vector3 position, float radius) {
        for (int i = 0; i < rockNumbers; i++) {
            float randomX = Random.Range(cornerRightTop.position.x, cornerLeftTop.position.x);
            float randomZ = Random.Range(cornerRightTop.position.z, cornerLeftBot.position.z);
            Vector3 destination = new Vector3(randomX,transform.position.y, randomZ);
            FallingObject newFallingObject = Instantiate(fallingObject.gameObject, position + Random.insideUnitSphere * radius - Vector3.one * radius/2 + Vector3.up * 10, fallingObject.transform.rotation).GetComponent<FallingObject>();
            newFallingObject.SetFallen(rock.gameObject)
                .SetSprite(rock.sprite)
                .SetDestination(destination)
                .SetFallTime(fallingTime);
            fallingRocks.Add(newFallingObject);
        }
    }



}
