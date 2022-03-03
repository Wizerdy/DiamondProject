using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : BossAction {
    [SerializeField] BossBody _body;
    [SerializeField] Reference<TempPlayerController> _player;
    [SerializeField] float fleeDistance = 4f;

    private void Start() {
    }
    public override void StartAction() {
        Debug.Log("Tp");
        _boss.Instance.ChangeState(GetState());
        Vector3 playerPosition = _player.Instance.transform.position;
        Vector3 direction = (playerPosition - transform.position).normalized;
        Vector3 destination = direction * fleeDistance;
        transform.position = new Vector3(_body.transform.position.x, _body.transform.position.y, transform.position.z);
        Wait();
    }

    public override Boss.State GetState() {
        return Boss.State.TELEPORT;
    }
}
