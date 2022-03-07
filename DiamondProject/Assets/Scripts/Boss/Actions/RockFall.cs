using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFall : BossAction {

    [SerializeField] float apparitionHigh = 10;
    [SerializeField] float radiusBounds;
    [Header("For Prog: ")]
    [SerializeField] HealthReference HealthRef;
    [SerializeField] FallingObject fallingObject = null;
    [SerializeField] Transform centralTransform;
    [SerializeField] Rock rock;
    [SerializeField] RockShield rockShield;
    [SerializeField] List<Rock> rocks = new List<Rock>();
    [SerializeField] BossBodyReference body;
    Vector2Int rocksNumberBounds;

    public override void StartAction() {
        // Debug.Log("Rock");
        _boss.Instance.ChangeState(GetState());
        _boss.Instance.NewWaightAction(this, 0);
        WeWillRockYou(Random.Range(rocksNumberBounds.x, rocksNumberBounds.y), transform.position);
        Wait();
    }

    void WeWillRockYou(int rockNumbers, Vector3 position) {
        RockShield newRockShield = Instantiate(rockShield.gameObject, body.Instance.Transform).GetComponent<RockShield>();
        newRockShield.BossActionOnDestroy(this);
        List<FallingObject> list = new List<FallingObject>();
        for (int i = 0; i < rockNumbers; i++) {
            Vector3 newDestinationFallingObject;
            newDestinationFallingObject = centralTransform.position + (Quaternion.Euler(0, 0, 360 / (rockNumbers) * (i - 1)) * new Vector3(Mathf.Cos(360 / rockNumbers) * Mathf.Deg2Rad, Mathf.Sin(360 / rockNumbers) * Mathf.Deg2Rad, 0)).normalized * radiusBounds;
            FallingObject newFallingObject = Instantiate(fallingObject.gameObject, newDestinationFallingObject + Vector3.up * apparitionHigh, fallingObject.transform.rotation).GetComponent<FallingObject>();
            Rock newRock = Instantiate(rock.gameObject, newFallingObject.transform).GetComponent<Rock>();
            newRock.AddRockShield(newRockShield);
            newFallingObject.SetFallen(newRock.gameObject)
                .SetSprite(rock.sprite)
                .SetDestination(newDestinationFallingObject)
                .SetFallTime(_duration);
            newRock.gameObject.SetActive(false);
        }
    }

    public override Boss.State GetState() {
        return Boss.State.ROCKFALL;
    }

}
