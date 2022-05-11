using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTrigger : Trigger {
    [System.Serializable]
    enum TriggerType {
        NOT_HITTED, HITTED
    }

    [SerializeField] Reference<Health> _health;

    [Header("Sytsem")]
    [SerializeField] float _time;
    [SerializeField] TriggerType _triggerType = TriggerType.NOT_HITTED;

    int _hitCount = 0;

    private void Start() {
        if (_health != null) { _health.Instance.OnHit += GetHit; }
    }

    public override bool IsSelfTrigger() {
        switch (_triggerType) {
            case TriggerType.NOT_HITTED:
                if (_hitCount <= 0) {
                    return true;
                }
                break;
            case TriggerType.HITTED:
                if (_hitCount > 0) {
                    return true;
                }
                break;
            default:
                break;
        }
        return false;
    }

    public void GetHit(int amount) {
        StartCoroutine(IHit());
    }

    public void GetHit() {
        StartCoroutine(IHit());
    }

    IEnumerator IHit() {
        _hitCount++;
        yield return new WaitForSeconds(_time);
        _hitCount--;
    }
}
