using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIA : MonoBehaviour {
    [System.Serializable]
    struct ShapeTrigger {
        public Trigger _trigger;
        public BossShape _shape;
    }

    [SerializeField] List<ShapeTrigger> _shapeTriggers;
    [SerializeField] Dictionary<Trigger, BossShape> _shapeChangement;
    [SerializeField] BossShapeSystem _shapeSystem;

    void OnValidate() {
        _shapeChangement = new Dictionary<Trigger, BossShape>();
        for (int i = 0; i < _shapeTriggers.Count; i++) {
            _shapeChangement.Add(_shapeTriggers[i]._trigger, _shapeTriggers[i]._shape);
        }
    }

    void Update() {
        foreach (KeyValuePair<Trigger, BossShape> item in _shapeChangement) {
            if (item.Key.IsTrigger()) {
                _shapeSystem.ChangeShape(item.Value);
            }
        }
    }
}
