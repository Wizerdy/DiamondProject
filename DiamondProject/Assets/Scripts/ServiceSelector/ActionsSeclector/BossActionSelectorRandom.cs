using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ServiceSelector/BossActionSelector/Random")]
public class BossActionSelectorRandom : ObjectSelectorRandom<BossAction>, IAction {
    public IEnumerator StartAction() {
        return Get().StartAction();
    }
}
