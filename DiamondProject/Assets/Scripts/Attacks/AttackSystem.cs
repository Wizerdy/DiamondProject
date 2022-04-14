using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackSystem : MonoBehaviour {
    [SerializeField] UnityEvent<BaseAttack> _onAttackStart;
    [SerializeField] UnityEvent<BaseAttack> _onAttackEnd;

    public event UnityAction<BaseAttack> OnAttackStart { add => _onAttackStart.AddListener(value); remove => _onAttackStart.RemoveListener(value); }
    public event UnityAction<BaseAttack> OnAttackEnd { add => _onAttackEnd.AddListener(value); remove => _onAttackEnd.RemoveListener(value); }

    List<BaseAttack> _attacks = new List<BaseAttack>();
    public void Register(BaseAttack attack) {
        if(attack == null) return;
        _attacks.Add(attack);
        _onAttackStart?.Invoke(attack);
    }

    public void Unregister(BaseAttack attack) {
        if (attack == null || !_attacks.Contains(attack)) return;
        _attacks.Remove(attack);
        _onAttackEnd?.Invoke(attack);
    }

}
