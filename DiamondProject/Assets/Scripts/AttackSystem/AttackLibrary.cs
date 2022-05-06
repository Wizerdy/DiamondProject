using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLibrary : MonoBehaviour {
    [SerializeField] List<BaseAttack> attacks;

    public BaseAttack GetAttack(string attackName) {
        for (int i = 0; i < attacks.Count; i++) {
            if(attackName == attacks[i].id) {
                return attacks[i];
            }
        }
        return null;
    }
}
