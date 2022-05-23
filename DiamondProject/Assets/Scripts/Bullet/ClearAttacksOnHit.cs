using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class ClearAttacksOnHit : MonoBehaviour {
    [SerializeField] LiaAttackReference _liaAttack;
    [SerializeField] DamageHealth _damageHealth;
    [Header("Values")]
    [SerializeField] float _stuntTime;

    void Start() {
        _damageHealth.OnDamage += _ClearAttacks;
    }

    private void OnDestroy() {
        _damageHealth.OnDamage -= _ClearAttacks;
    }

    private void _ClearAttacks(GameObject obj, int damage) {
        if (!_liaAttack.IsValid()) { return; }
        if (_liaAttack.Instance.transform.FindElderlyByTag() == obj.transform.FindElderlyByTag()) {
            _liaAttack.Instance.Stunt(_stuntTime);
        }
    }
}
