using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableObjectSelectorListed<T> : ScriptableObjectSelector<T> {
    [SerializeField] List<T> _objects;
    int _index = 0;

    public T Object { get { return _objects[_index]; } }

    public override T Get() {
        if (_objects.Count == 0) { throw new System.NullReferenceException("List is empty : " + name); }

        int tmp = _index;
        Next();
        return _objects[tmp];
    }

    public override void Reinitialize() {
        SetIndex(0);
    }

    public void Next() {
        int tmp = _index;
        tmp++;
        tmp %= _objects.Count;
        SetIndex(tmp);
    }

    public void SetIndex(int index) {
        _index = index;
    }
}
