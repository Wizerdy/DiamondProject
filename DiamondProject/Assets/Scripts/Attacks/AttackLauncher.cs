using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLauncher : MonoBehaviour {
    [SerializeField] AttackInstantiator attackInstantiator;
    [SerializeField] AttackLibrary attackLibrary;

    public void LaunchAttack(string attackName) {
        attackInstantiator.InstantiateAttack(attackLibrary.GetAttack(attackName));
    }
}
