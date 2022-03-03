using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class NPC : Entity {
    [SerializeField] GameObject flowchart;
    public int index = 0;

    public Tools.BasicDelegate<NPC> OnInteract;
    public Tools.BasicDelegate<NPC> OnStopInteracting;

    void Update() {

    }

    public void StartTalking(GameObject playerRef) {
        //player = playerRef;
        //player.GetComponent<OldPlayerController>().disableMovement();
        flowchart.SetActive(true);
        //flowchart.enabled = true;
        OnInteract?.Invoke(this);
    }

    public void StopTalking() {
        flowchart.SetActive(false);
        //player.GetComponent<OldPlayerController>().enableMovement();
        OnStopInteracting?.Invoke(this);
    }
}
