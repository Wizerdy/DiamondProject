using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNoctali : MonoBehaviour {
    public GameObject Evoli;
    public GameObject Noctali;
    public GameObject bluesc;
    public Vector2 blue;
    public Vector2 green;
    public Vector2 purple;
    public Vector2 orange;
    public int hit;
    public bool phaseuno;
    public bool phasedo;
    public bool phaseto;

    private void Start() {
        blue.y = blue.x;
        green.y = green.x;
        purple.y = purple.x;
        orange.y = orange.x;
    }
    public void AddFlower(string name) {
        switch (name) {
            case "Flower Green" :
                green.x++;
                break;
            case "Flower Orange":
                orange.x++;
                break;
            case "Flower Blue":
                blue.x++;
                break;
            case "Flower Purple":
                purple.x++;
                break;
        }
    }

    public void RemoveFlower(string name) {
        switch (name) {
            case "Flower Green":
                green.x--;
                break;
            case "Flower Orange":
                orange.x--;
                break;
            case "Flower Blue":
                blue.x--;
                
                break;
            case "Flower Purple":
                purple.x--;
                break;
        }
        if (blue.x <= 0 && purple.x <= 0 && orange.x == orange.y && green.x == green.y) {
            phaseto = true;
            Active();
        }
        Verif();
    }
    public void Active() {
        StartCoroutine(RedScreen(0.5f));

        IEnumerator RedScreen(float time) {
            bluesc.gameObject.SetActive(true);
            yield return new WaitForSeconds(time);
            bluesc.gameObject.SetActive(false);
        }
    }
    public void Verif() {
        if (phaseto && phasedo && phaseuno) {
            LaunchNoctali();
        }
    }

    void LaunchNoctali() {
        bluesc.gameObject.SetActive(false);
        Evoli.GetComponent<Boss>().Die();
        Noctali.SetActive(true);
    }


}
