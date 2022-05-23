using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifier : MonoBehaviour {
    enum ResistanceType {
        RESISTANCE,
        WEAKNESS,
        FIX,
        IMMUNITY,
        NOMODIFIER
    }
    [SerializeField] ResistanceType type;
    [SerializeField] string damageType;
    [SerializeField] int value;

    public string DamageType { get { return damageType; } }

    public int Modify(int amount) {
        switch (type) {
            case ResistanceType.WEAKNESS:
                return amount + value;
            case ResistanceType.FIX:
                return value;
            case ResistanceType.RESISTANCE:
                return amount - value < 0 ? 0 : amount - value;
            case ResistanceType.IMMUNITY:
                return 0;
            default:
            case ResistanceType.NOMODIFIER: 
                return amount;
        }
    }
}

public static class DMMethod {
    public static DamageModifier Get(this IEnumerable<DamageModifier> dms, string damageType) {
        foreach (var dm in dms) {
            if (dm.DamageType == damageType) {
                return dm;
            }
        }
        return null;
    }
    public static bool Contains(this IEnumerable<DamageModifier> dms, string damageType) {
        foreach (var dm in dms) {
            if (dm.DamageType == damageType) {
                return true;
            }
        }
        return false;
    }
}
