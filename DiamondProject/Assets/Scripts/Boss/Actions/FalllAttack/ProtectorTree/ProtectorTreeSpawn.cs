using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;
using UnityEngine.Events;

public class ProtectorTreeSpawn : BaseAttack {
    [SerializeField] Shield shieldPrefab; 
    [SerializeField] ProtectorTree protectorTreePrefab; 
    [SerializeField] List<ProtectorTree> trees = new List<ProtectorTree>();
    [SerializeField] Shield currentShield;
    [SerializeField] float _radius;
    [SerializeField] int _treeNumbers;
    [SerializeField] float _apparitionTime;
    [SerializeField] float _timeBeforeGrownAgain;
    [SerializeField] UnityAction<ProtectorTree> onTreeDeath;

    void SpawnTree(Vector3 position) {
        ProtectorTree newBossTree = Instantiate(protectorTreePrefab.gameObject).GetComponent<ProtectorTree>();
        newBossTree.SetDestination(position)
            .SetApparitionTime(_apparitionTime);
        newBossTree.gameObject.SetActive(true);
        AddTree(newBossTree);
        if(onTreeDeath == null) {
            onTreeDeath += RemoveTree;
        }
        newBossTree.onDeath.AddListener(onTreeDeath);
    }
    protected override IEnumerator IExecute() {
        currentShield = Instantiate(shieldPrefab.gameObject, _bossRef.Instance.transform).GetComponent<Shield>();
        currentShield.AttachToHealth(currentShield.transform.parent.GetComponentInChildren<Health>());
        for (int i = 0; i < _treeNumbers; i++) {
            SpawnTree(CirclePoint(Vector3.zero, _radius, _treeNumbers, i, Vector3.forward));
        }
        yield return null;
    }
    public void AddTree(ProtectorTree tree) {
        if (trees.Count == 0) {
            currentShield.Activate();
        }
        trees.Add(tree);
    }

    public void RemoveTree(ProtectorTree tree) {
        trees.Remove(tree);
        if (trees.Count == 0) {
            currentShield.Desactivate();
            Destroy(currentShield.gameObject);
            StartCoroutine(Tools.Delay(ExecuteAgain,_timeBeforeGrownAgain));
        }
    }

    void ExecuteAgain() {
        StartCoroutine(IExecute());
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