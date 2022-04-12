using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LogicGate {
    AND, OR, NOR, XOR, NOT
}

public class Trigger : MonoBehaviour {
    [SerializeField] protected List<Trigger> _childs;
    [SerializeField] protected LogicGate _childGate;

    public struct GameDatas {

    }

    public virtual bool IsSelfTrigger(GameDatas datas) {
        return true;
    }

    public bool IsTrigger(GameDatas datas) {
        if (_childs == null || _childs.Count == 0) { return true; } // ?
        int boolCount = 0;
        for (int i = 0; i < _childs.Count; i++) {
            if (_childs[i].IsTrigger(datas)) { ++boolCount; }
        }
        if (TrueCountWithGate(_childGate, _childs.Count).Contains(boolCount)) {
            return true && IsSelfTrigger(datas);
        }
        return false;
    }

    List<int> TrueCountWithGate(LogicGate gate, int count) {
        List<int> list = new List<int>();
        switch (gate) {
            case LogicGate.AND:
                list.Add(count);
                break;
            case LogicGate.OR:
                if (count <= 1) { return list; }
                for (int i = 1; i < count; i++) {
                    list.Add(i);
                }
                break;
            case LogicGate.NOR:
                list.Add(0);
                list.Add(count);
                break;
            case LogicGate.XOR:
                if (count <= 1) { return list; }
                for (int i = 1; i < count - 1; i++) {
                    list.Add(i);
                }
                break;
            case LogicGate.NOT:
                list.Add(0);
                break;
            default:
                break;
        }
        return list;
    }
}
