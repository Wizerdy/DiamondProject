using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : BossAction {
    [SerializeField] Transform Body;
    [SerializeField] Reference<TempPlayerController> _player;
    [SerializeField] float fleeDetectionRadius = 2f;
    [SerializeField] float fleeDistance = 4f;

    private void Start() {
        if (Body.gameObject.GetComponentInParent<TeleportAwarness>() == null) {
            TeleportAwarness teleportAwarness = Body.gameObject.AddComponent<TeleportAwarness>();
            teleportAwarness.FleeDetectionRadius = fleeDetectionRadius;
        }
    }
    public override IEnumerator StartAction() {
        Debug.Log("Tp");
        _boss.Instance.ChangeState(GetState());
        _durationTimer = _duration;
        while (_durationTimer > 0) {
            yield return null;
            _durationTimer -= Time.deltaTime;
        }
        Vector3 playerPosition = _player.Instance.transform.position;
        Vector3 direction = (playerPosition - transform.position).normalized;
        Vector3 destination = direction * fleeDistance;
        transform.position = new Vector3(Body.position.x, Body.position.y, transform.position.z);
        _boss.Instance.EndState(_duration);
    }

    public override Boss.State GetState() {
        return Boss.State.TELEPORT;
    }
}
