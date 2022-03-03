using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Entity {
    public int index = 0;
    [SerializeField] private GameObject flowchart;
    private GameObject player;

    public bool inDiscussion;
    public GameObject interactPanel;

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

    public void SetActiveGameObject(GameObject obj, bool isActive)
    {
        obj.SetActive(isActive);
    }

    public void SetActiveInteractPanel(bool isActive)
    {
        SetActiveGameObject(interactPanel, isActive);
    }

    public void StartTalking(GameObject playerRef) {
        player = playerRef;
        player.GetComponent<OldPlayerController>().disableMovement();
        flowchart.SetActive(true);
        //flowchart.enabled = true;
    }

    public void StopTalking() {
        flowchart.SetActive(false);
        player.GetComponent<OldPlayerController>().enableMovement();
    }
}
