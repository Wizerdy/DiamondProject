using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableObjectSelector<T> : ScriptableObject, IObjectSelector<T> {
    public abstract T Get();

    public virtual void Reinitialize() { }
}
