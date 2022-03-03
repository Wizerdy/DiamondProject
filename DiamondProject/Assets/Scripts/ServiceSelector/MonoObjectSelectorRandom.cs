using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public abstract class MonoObjectSelectorRandom<T> : MonoObjectSelector<T> {
    [System.Serializable]
    public struct WeightT {
        public T value;
        public float weight;
        public WeightT(T value, float weight) {
            this.value = value;
            this.weight = weight;
        }
    }

    public List<WeightT> objects;

    public override T Get() {
        int index = Tools.Ponder(objects.Select(o => o.weight).ToArray());
        if (index == -1) { return default(T); }
        return objects[index].value;
    }

    public void NewWeight(T value, float weight) {
        for (int i = 0; i < objects.Count; i++) {
            if (EqualityComparer<T>.Default.Equals(objects[i].value, value)) {
                WeightT weightT = objects[i];
                weightT.weight = weight;
                objects[i] = weightT;
            }
        }
    }

}
