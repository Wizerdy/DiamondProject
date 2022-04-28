using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectorTrees : BaseAttack {
    [SerializeField] BossReference _bossRef;
    [SerializeField] ProtectorTree _protectorTree;
    [SerializeField] TreeShield _treeShield;
    [SerializeField] float _radius;
    [SerializeField] int _treeNumbers;
    [SerializeField] float _apparitionTime;
    void SpawnTree(Vector3 position, TreeShield treeShield) {

        ProtectorTree newBossTree = Instantiate(_protectorTree.gameObject).GetComponent<ProtectorTree>();
        newBossTree.SetDestination(position)
            .SetApparitionTime(_apparitionTime);
        newBossTree.AddTreeShield(treeShield);

    }
    protected override IEnumerator IExecute() {
        isPlaying = true;
        TreeShield newtreeShield = Instantiate(_treeShield.gameObject, _bossRef.Instance.transform).GetComponent<TreeShield>();
        newtreeShield.AttachToHealth(newtreeShield.transform.parent.GetComponentInChildren<Health>());
        for (int i = 0; i < _treeNumbers; i++) {
            SpawnTree(CirclePoint(Vector3.zero, _radius, _treeNumbers, i), newtreeShield);
        }
        isPlaying = false;
        yield return null;
    }

    public static Vector3 CirclePoint(Vector3 center, float radius, int numberPoints, int point, Vector3 axis = default) {
        Vector3 result;
        if (axis == default) {
            axis = Vector3.up;
        }
        Vector3 origin = Vector3.zero;
        switch (axis) {
            case Vector3 v when v.Equals(Vector3.up):
                origin = Vector3.forward;
                break;
            case Vector3 v when v.Equals(Vector3.right):
                origin = Vector3.up;
                break;
            case Vector3 v when v.Equals(Vector3.forward):
                origin = Vector3.up;
                break;
        }
        result = Quaternion.Euler(axis * 360 / numberPoints * point) * origin * radius;
        return center + result;
    }
}
