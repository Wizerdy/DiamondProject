using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public abstract class ObjectSelectorRandom<T> : ObjectSelector<T> {
    [System.Serializable]
    public struct WeightT {
        public T value;
        public float weight;
    }

    public List<WeightT> objects;

    public override T Get() {
        int index = Tools.Ponder(objects.Select(o => o.weight).ToArray());
        if (index == -1) { return default(T); }
        return objects[index].value;
    }
}
