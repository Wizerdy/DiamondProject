using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCall : MonoBehaviour
{
    public BaseAttack brambleBall;
    public BaseAttack trackingTree;
    public BaseAttack leafBeam;
    public BaseAttack iceHell;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            brambleBall.StartCoroutine("Launch");
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            trackingTree.StartCoroutine("Launch");
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            leafBeam.StartCoroutine("Launch");
        }

        if (Input.GetKeyDown(KeyCode.T)) {
            iceHell.StartCoroutine("Launch");
        }
    }
}
