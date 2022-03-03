using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoObjectSelector<T> : MonoBehaviour, IObjectSelector<T> {
    public abstract T Get();

    public virtual void Reinitialize() { }
}
