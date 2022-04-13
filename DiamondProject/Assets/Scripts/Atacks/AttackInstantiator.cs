using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInstantiator : MonoBehaviour {
    public Transform parentInstance;
    public void InstantiateAttack(BaseAttack attack) {
        if(attack == null) return;
        BaseAttack newAttack = Instantiate(attack);
        newAttack.transform.parent = parentInstance;
    }
}
