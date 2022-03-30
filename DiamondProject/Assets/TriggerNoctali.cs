using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNoctali : MonoBehaviour {
    public GameObject Evoli;
    public GameObject Noctali;
    public GameObject bluesc;
    public PosterityObject post;
    public Camera cam;
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
            case "Flower Green":
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
            post.numberOfTriggerActivate++;
            Active();
        }
        Verif();
    }
    public void Active() {
        StartCoroutine(RedScreen(0.5f));

        IEnumerator RedScreen(float time) {
            float timer = time;
            Vector3 normal = cam.transform.position;
            bluesc.gameObject.SetActive(true);
            if (!post.firstTimeTalking) {
                while (timer > 0) {
                    cam.transform.position = normal + Random.insideUnitSphere;
                    timer -= Time.deltaTime;
                    yield return null;
                }
            }
            cam.transform.position = normal;
            bluesc.gameObject.SetActive(false);
        }
    }
    public void Verif() {
        if (phaseto && phasedo && phaseuno) {
            LaunchNoctali();
        }
    }

    void LaunchNoctali() {
        post.gotToAnotherBoss = true;
        bluesc.gameObject.SetActive(false);
        Evoli.GetComponent<Boss>().Die();
        Noctali.SetActive(true);
    }


}
