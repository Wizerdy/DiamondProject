using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gino : MonoBehaviour {
    public static Gino instance = null;

    public TempPlayerController player;
    public Boss boss;
    void Start() {
        instance = this; 
    }

}
