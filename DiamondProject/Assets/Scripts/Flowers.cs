using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Flowers : BossEntities {
    public TriggerNoctali triggers;

    private void Start() {
        UnityAction die = () => triggers.RemoveFlower(gameObject.name);
        gameObject.GetComponent<Health>().OnDeath += die;
    }
    private void Awake() {
        base.Awake();
        triggers.AddFlower(gameObject.name);
    }
}
