using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoObjectSelectorUnique<T> : MonoObjectSelector<T> {
    [SerializeField] T obj;

    public override T Get() {
        return obj;
    }
}
