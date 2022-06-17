using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class OrderInLayerManagerEditor : Editor {
    
    public static void PutScript() {
        string[] temp = AssetDatabase.GetAllAssetPaths();
        List<string> result = new List<string>();
        foreach (string s in temp) {
            if (s.Contains(".prefab")) result.Add(s);
        }
        for (int i = 0; i < result.Count; i++) {
            GameObject asset = AssetDatabase.LoadAssetAtPath<GameObject>(result[i]);
            SpriteRenderer[] sr = asset.GetComponentsInChildren<SpriteRenderer>();
            MeshRenderer[] sk = asset.GetComponentsInChildren<MeshRenderer>();
            for (int j = 0; j < sr.Length; j++) {
                if (sr[i].GetComponent<OrderInLayerManager>() == null) {
                    sr[i].gameObject.AddComponent<OrderInLayerManager>();
                }
            }
            for (int j = 0; j < sk.Length; j++) {
                if (sr[i].GetComponent<OrderInLayerManager>() == null) {
                    sr[i].gameObject.AddComponent<OrderInLayerManager>();
                }
            }
        }
    }
}
