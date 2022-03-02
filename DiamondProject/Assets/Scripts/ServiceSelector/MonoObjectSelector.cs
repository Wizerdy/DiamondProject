using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoObjectSelector<T> : MonoBehaviour, IObjectSelector<T> {
    public abstract T Get();

    public bool IsInside(List<T> inside, T needToCheck) {
        for (int i = 0; i < inside.Count; i++) {
            if (EqualityComparer<T>.Default.Equals(inside[i], needToCheck)) { return true; }
        }
        return false;
    }

    public virtual void Reinitialize() { }
}
