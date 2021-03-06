using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class MonoBossActionSelectorRandom : MonoObjectSelectorRandom<BossAction> {

    [System.Serializable] 
    public struct WeightObject {
        public Object value;
        public float weight;
        public WeightObject(Object value, float weight = 1) {
            this.value = value;
            this.weight = weight;
        }
        public static explicit operator WeightT(WeightObject value) {
            WeightT newWeight = new WeightT(null, value.weight);
            Tools.SerializeInterface(ref newWeight.value, ref value.value);
            return newWeight;
        }
    }
    public BossAction Get(string actionName) {
        for (int i = 0; i < objects.Count; i++) {
            if (actionName == objects[i].value.Name) {
                return objects[i].value;
            }
        }
        Debug.LogError("BossAction Not Found");
        return default(BossAction);
    }
}
