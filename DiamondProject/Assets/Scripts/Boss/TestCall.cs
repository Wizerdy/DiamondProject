using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCall : MonoBehaviour
{
    public BaseAttack baseAttack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            baseAttack.StartCoroutine("Launch");
        }
    }
}
