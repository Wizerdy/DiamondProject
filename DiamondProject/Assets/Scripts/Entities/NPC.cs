using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class NPC : Entity {
    [SerializeField] GameObject flowchart;
    public int index = 0;

    public Tools.BasicDelegate<NPC> OnInteract;
    public Tools.BasicDelegate<NPC> OnStopInteract;

    public string flowchartMessage;

    void Update() {

    }

    public void StartTalking(GameObject playerRef) {
        //player = playerRef;
        //player.GetComponent<PlayerController>().disableMovement();
        Debug.Log("START CONVERSATION");
        //flowchart.SetActive(true);
        Fungus.Flowchart.BroadcastFungusMessage(flowchartMessage);
        OnInteract?.Invoke(this);
    }

    public void StopTalking() {
        Debug.Log("STOP CONVERSATION");
        //player.GetComponent<PlayerController>().enableMovement();
        //flowchart.SetActive(false);
        OnStopInteract?.Invoke(this);
    }
}
