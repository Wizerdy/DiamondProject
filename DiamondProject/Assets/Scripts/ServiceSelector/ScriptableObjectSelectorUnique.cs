using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableObjectSelectorUnique<T> : ScriptableObjectSelector<T> {
    [SerializeField] T obj;

    public override T Get() {
        return obj;
    }
}
