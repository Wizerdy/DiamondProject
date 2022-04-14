using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLauncher : MonoBehaviour {
    AttackInstantiator attackInstantiator;
    AttackLibrary attackLibrary;

    public void LaunchAttack(string attackName) {
        attackInstantiator.InstantiateAttack(attackLibrary.GetAttack(attackName));
    }
}
