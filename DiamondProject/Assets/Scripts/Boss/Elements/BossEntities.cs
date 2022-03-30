using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class BossEntities : MonoBehaviour {
    [SerializeField] protected BossReference bossRef;
    protected void Awake() {
        bossRef.Instance.todestroyondeath.Add(this.gameObject);
    }
}
