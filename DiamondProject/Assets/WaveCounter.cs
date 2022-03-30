using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCounter : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "ShockWave") {
            collision.GetComponent<Shockwave>().Change();
        }
    }

}
