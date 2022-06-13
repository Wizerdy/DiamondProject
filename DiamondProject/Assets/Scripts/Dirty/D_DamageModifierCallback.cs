using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class D_DamageModifierCallback : MonoBehaviour {
    [SerializeField] DamageModifier _damageModifier;

    [SerializeField] bool _useTargetRes = false;
    [SerializeField] DamageModifier.ResistanceType _targetResistance = DamageModifier.ResistanceType.NOMODIFIER;

    [SerializeField] UnityEvent<DamageModifier, int> _onUse;

    void Awake() {
        if (_damageModifier != null) {
            _damageModifier.OnUse += _InvokeOnUse;
        }
    }

    private void _InvokeOnUse(DamageModifier modif, int damage) {
        if (_useTargetRes && modif.Resistance != _targetResistance) { return; }

        _onUse.Invoke(modif, damage);
    }
}
