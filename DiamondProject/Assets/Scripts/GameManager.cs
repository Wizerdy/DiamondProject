using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public QTEManager qteManager;
    public QTE qte;

    void Start() {
        Debug.Log(qte);
        qteManager.Launch(qte);
    }

    void Update() {

    }
}
