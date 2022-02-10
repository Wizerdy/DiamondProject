using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Entity
{
    public int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(index);
        Debug.Log(GameManager.Instance);
        index = GameManager.Instance.currentNpcIndex;
        GameManager.Instance.currentNpcIndex += 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
