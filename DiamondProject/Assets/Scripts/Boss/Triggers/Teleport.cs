using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : BossAction {
    [SerializeField] float radiusSpawnPoint = 10f;
    [Header("For Prog: ")]
    [SerializeField] PlayerControllerReference _player;
    [SerializeField] List<Transform > _transformList = new List<Transform>();
    [SerializeField] Transform centralTransform;

    private void Start() {
        for (int i = 0; i < _transformList.Count; i++) {
            if (i ==0 ) {
                _transformList[i].position = centralTransform.position;
            } else {
                _transformList[i].position = centralTransform.position + (Quaternion.Euler(0,0,360 / (_transformList.Count - 1) * (i - 1)) * new Vector3(Mathf.Cos(360/_transformList.Count - 1) * Mathf.Deg2Rad, Mathf.Sin(360 / _transformList.Count - 1) * Mathf.Deg2Rad , 0)).normalized * radiusSpawnPoint;
            }
            _transformList[i].parent = null;
        }
    }
    public override void StartAction() {
        OnCast.Invoke();
        // Debug.Log("Tp");
        _boss.Instance.ChangeState(GetState());
        _boss.Instance.StopActions();
        int index = 0;
        float maxDistance = 0;
        for (int i = 0; i < _transformList.Count; i++) {
            if(Vector3.Distance(_transformList[i].position, _player.Instance.transform.position) > maxDistance) {
                maxDistance = Vector3.Distance(_transformList[i].position, _player.Instance.transform.position);
                index = i;
            }
        }
        _boss.Instance.transform.position = _transformList[index].transform.position;
        Wait();
    }

    public override Boss.State GetState() {
        return Boss.State.TELEPORT;
    }
}
