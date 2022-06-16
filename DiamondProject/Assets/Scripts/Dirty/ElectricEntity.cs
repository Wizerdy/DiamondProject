using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricEntity : MonoBehaviour {
    [Header("DamageHealth")]
    [SerializeField] DamageHealth _damageHealth;
    [SerializeField] int _bonusDamage = 10;
    [SerializeField] string _name = "Lightning";

    [Header("Sprite")]
    [SerializeField] SpriteRenderer _sprite;
    [SerializeField] float _waveLength = 0.4f;
    [SerializeField] float _waveSpeed = -1;

    private void OnEnable() {
        _damageHealth.Damage += _bonusDamage;
        _damageHealth.DamageType = _name;

        if (_sprite == null) { return; }
        _sprite.material.SetFloat("_WaveLength", _waveLength);
        _sprite.material.SetFloat("_WaveSpeed", _waveSpeed);
        _sprite.material.SetInteger("_Active", 1);
    }
}
