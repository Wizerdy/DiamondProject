using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeShield : Shield {
    [SerializeField] BossReference _boss;
    [SerializeField] List<BossTree> trees = new List<BossTree>();
    [SerializeField] BossAction bossAction = null;

    public void AddTree(BossTree tree) {
        if (trees.Count == 0) {
            Protect();
        }
        trees.Add(tree);
    }

    public void BossActionOnDestroy(BossAction bossAction) {
        this.bossAction = bossAction;
    }

    public void RemoveTree(BossTree tree) {
        trees.Remove(tree);
        if (trees.Count == 0) {
            StopProtect();
            _boss.Instance.NewWeightAction(bossAction, 1);
            Destroy(gameObject);
        }
    }
}
