using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeShield : Shield {
    [SerializeField] BossReference _boss;
    [SerializeField] List<ProtectorTree> trees = new List<ProtectorTree>();

    public void AddTree(ProtectorTree tree) {
        if (trees.Count == 0) {
            Protect();
        }
        trees.Add(tree);
    }

    public void RemoveTree(ProtectorTree tree) {
        trees.Remove(tree);
        if (trees.Count == 0) {
            StopProtect();
            Destroy(gameObject);
        }
    }
}
