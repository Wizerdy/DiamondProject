using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianSeeds : BossAction {
    [Header("For GD: ")]
    [SerializeField] int _life;
    [SerializeField] float _apparitionTime;
    [SerializeField] Vector2Int treesNumberBounds;
    [Header("For Prog: ")]
    [SerializeField] PlayerControllerReference _playerRef;
    [SerializeField] HealthReference HealthRef;
    [SerializeField] BossTree bossTree = null;
    [SerializeField] BossBodyReference _body;
    [SerializeField] TreeShield treeShield;
    [SerializeField] List<BossTree> trees = new List<BossTree>();
    public override void StartAction() {
        _boss.Instance.ChangeState(GetState());
        _boss.Instance.NewWeightAction(this, 0);
        SpawnTree(Random.Range(treesNumberBounds.x, treesNumberBounds.y + 1), _body.Instance.transform.position);
        Wait();
    }

    void SpawnTree(int treeNumbers, Vector3 position) {
        TreeShield newtreeShield = Instantiate(treeShield.gameObject, _body.Instance.Transform).GetComponent<TreeShield>();
        newtreeShield.BossActionOnDestroy(this);
        Vector3 playerPosition = _playerRef.Instance.transform.position - _body.Instance.transform.position; 
        for (int i = 0; i < treeNumbers; i++) {
            BossTree newBossTree = Instantiate(bossTree.gameObject).GetComponent<BossTree>();
            Vector3 newPosition = (Quaternion.Euler(0, 0, 360 * i / treeNumbers) * playerPosition) + _body.Instance.transform.position; 
            newBossTree.SetDestination(newPosition)
                .SetApparitionTime(_apparitionTime)
                .SetLife(_life);
            newBossTree.AddTreeShield(newtreeShield);
        }
    }

    public override Boss.State GetState() {
        return Boss.State.GUARDIANSEED;
    }

}
