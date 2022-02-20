using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectSelector<T> {
    void Reinitialize();
    T Get();
}
