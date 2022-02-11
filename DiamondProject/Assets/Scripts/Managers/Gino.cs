using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gino : MonoBehaviour {
    public static Gino instance = null;

    public PlayerController player;
    void Start() {
        instance = this; 
    }

}
