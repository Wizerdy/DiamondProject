using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject missile;
    public GameObject magicBalls;
    public GameObject rocks;

    enum State {
        WAITING,
        TELEPORTING,
        ROCKFALL,
        FIRING,
    }

    State state;

    void Update()
    {
        
    }
}
