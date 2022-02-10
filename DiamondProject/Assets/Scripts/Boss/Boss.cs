using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    enum State { WAIT, TELEPORT, ROCKFALL, FIRE, }

    [SerializeField]
    GameObject missile;
    [SerializeField]
    GameObject magicBalls;

    private GameObject player;

    [Header("RockFall")]
    [SerializeField] FallingObject fallingObject = null;
    public Rock rock;
    public float fallingTime = 3f;
    [SerializeField] List<FallingObject> fallingRocks = new List<FallingObject>();
    [SerializeField] Transform cornerLeftTop;
    [SerializeField] Transform cornerRightTop;
    [SerializeField] Transform cornerLeftBot;
    [SerializeField] Vector2Int rocksNumberBounds;

    [Header("Teleport")]
    [SerializeField] private float fleeRadius = 2f;
    [SerializeField] private float fleeDistance = 4f;

    private State state;

    private void Start() {
        WeWillRockYou(Random.Range(rocksNumberBounds.x, rocksNumberBounds.y + 1), transform.position, 2);
        WeWillRockYou(Random.Range(rocksNumberBounds.x, rocksNumberBounds.y + 1), transform.position, 2);
        WeWillRockYou(Random.Range(rocksNumberBounds.x, rocksNumberBounds.y + 1), transform.position, 2);
    }
    void Update() {
        UpdateFlee();
    }

    void WeWillRockYou(int rockNumbers, Vector3 position, float radius) {
        for (int i = 0; i < rockNumbers; i++) {
            float randomX = Random.Range(cornerRightTop.position.x, cornerLeftTop.position.x);
            float randomY = Random.Range(cornerRightTop.position.y, cornerLeftBot.position.y);
            Vector3 destination = new Vector3(randomX, randomY, transform.position.z);
            FallingObject newFallingObject = Instantiate(fallingObject.gameObject, position + Random.insideUnitSphere * radius - Vector3.one * radius / 2 + Vector3.up * 10, fallingObject.transform.rotation).GetComponent<FallingObject>();
            newFallingObject.SetFallen(rock.gameObject)
                .SetSprite(rock.sprite)
                .SetDestination(destination)
                .SetFallTime(fallingTime);
            fallingRocks.Add(newFallingObject);
        }
    }

    void UpdateFlee() {
        Vector3 playerPosition = player.transform.position;
        if ((playerPosition - transform.position).sqrMagnitude < fleeRadius) {
            FleeFrom(player);
        }
    }

    void FleeFrom(GameObject entity) {
        Vector3 entityPosition = entity.transform.position;
        Vector3 direction = (entityPosition - transform.position).normalized;
        Vector3 destination = direction * fleeDistance;
        Teleport(destination);
    }

    void Teleport(Vector3 position) {
        transform.position = position;
    }
}
