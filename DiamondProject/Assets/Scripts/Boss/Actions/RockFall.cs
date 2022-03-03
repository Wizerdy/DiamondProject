using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFall : BossAction {

    [SerializeField] HealthReference HealthRef;
    [SerializeField] FallingObject fallingObject = null;
    [SerializeField] Rock rock;
    [SerializeField] RockShield rockShield;
    [SerializeField] List<Rock> rocks = new List<Rock>();
    [SerializeField] BossBodyReference body;
    [SerializeField] Vector2 radiusBounds;
    [SerializeField] Vector2Int rocksNumberBounds;
    [SerializeField] float fallingTime = 3f;
    [SerializeField] float apparitionHigh = 10;

    public override void StartAction() {
        Debug.Log("Rock");
        _boss.Instance.ChangeState(GetState());
        _boss.Instance.NewWaightAction(this, 0);
        WeWillRockYou(Random.Range(rocksNumberBounds.x, rocksNumberBounds.y), transform.position);
        Wait();
    }

    void WeWillRockYou(int rockNumbers, Vector3 position) {
        Duration = fallingTime;
        RockShield newRockShield = Instantiate(rockShield.gameObject, body.Instance.Transform).GetComponent<RockShield>();
        newRockShield.BossActionOnDestroy(this);
        for (int i = 0; i < rockNumbers; i++) {
            float randomDegree = Random.Range(0, 360);
            float randomDist = Random.Range(radiusBounds.x, radiusBounds.y);
            Vector3 destination = new Vector3(Mathf.Cos(randomDegree * Mathf.Deg2Rad), Mathf.Sin(randomDegree * Mathf.Deg2Rad), 0);
            destination = destination.normalized * randomDist;
            destination.z = position.z;

            FallingObject newFallingObject = Instantiate(fallingObject.gameObject, position + destination + new Vector3(0, apparitionHigh, 0), fallingObject.transform.rotation).GetComponent<FallingObject>();
            Rock newRock = Instantiate(rock.gameObject, newFallingObject.transform).GetComponent<Rock>();
            newRock.AddRockShield(newRockShield);
            newFallingObject.SetFallen(newRock.gameObject)
                .SetSprite(rock.sprite)
                .SetDestination(position + destination)
                .SetFallTime(fallingTime);
            newRock.gameObject.SetActive(false);
        }
    }

    public override Boss.State GetState() {
        return Boss.State.ROCKFALL;
    }
}
