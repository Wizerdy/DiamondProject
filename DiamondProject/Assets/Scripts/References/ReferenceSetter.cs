using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReferenceSetter<T> : MonoBehaviour {
    [SerializeField] T source;
    [SerializeField] Reference<T> target;

    private void Reset() {
        source = GetComponent<T>();
    }

    private void Awake() {
        (target as IReferenceSetter<T>).SetInstance(source);
    }
}
