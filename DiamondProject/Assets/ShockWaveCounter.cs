using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveCounter : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            collision.transform.GetChild(3).GetChild(0).GetChild(0).gameObject.AddComponent<WaveCounter>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.transform.GetChild(3).GetChild(0).GetChild(0).gameObject.GetComponent<WaveCounter>().enabled = false;
        }
    }
}
