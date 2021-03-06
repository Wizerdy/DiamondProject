using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockShield : Shield {
    [SerializeField] BossReference _boss;
    [SerializeField] List<Rock> rocks = new List<Rock>();
    [SerializeField] BossAction bossAction = null;

    public void AddRock(Rock rock) {
        if (rocks.Count == 0) {
            Activate();
        }
        rocks.Add(rock);
    }

    public void BossActionOnDestroy(BossAction bossAction) {
        this.bossAction = bossAction;
    }

    public void RemoveRock(Rock rock) {
        rocks.Remove(rock);
        if (rocks.Count == 0) {
            Desactivate();
      //      _boss.Instance.NewWeightAction(bossAction, 1);
            Destroy(gameObject);
        }
    }
}
