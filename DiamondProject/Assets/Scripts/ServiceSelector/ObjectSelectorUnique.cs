using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectSelectorUnique<T> : ObjectSelector<T> {
    [SerializeField] T obj;

    public override T Get() {
        return obj;
    }
}
