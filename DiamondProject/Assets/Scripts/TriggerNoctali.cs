using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNoctali : MonoBehaviour {
    public GameObject Evoli;
    public GameObject Noctali;
    public GameObject bluesc;
    public GameObject fond;
    public Sprite newFond;
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
            while (timer > 0) {
                if (!post.firstTimeTalking) {
                    cam.transform.position = normal + Random.insideUnitSphere;
                }
                timer -= Time.deltaTime;
                yield return null;
            }

            cam.transform.position = normal;
            bluesc.gameObject.SetActive(false);
        }
    }
    public void Verif() {
        if (phaseto && phasedo && phaseuno) {
            StartCoroutine(LaunchNoctali());
        }
    }

    IEnumerator LaunchNoctali() {
        float timer = 3f;
        Vector3 normal = cam.transform.position;
        Evoli.GetComponent<Boss>().enabled = false;
        Evoli.transform.GetChild(0).gameObject.SetActive(false);
        Evoli.transform.GetChild(1).gameObject.SetActive(false);
        Evoli.transform.GetChild(2).gameObject.SetActive(false);
        Evoli.transform.GetChild(3).gameObject.SetActive(false);
        Evoli.transform.GetChild(5).gameObject.SetActive(false);
        Evoli.transform.GetChild(4).gameObject.SetActive(false);
        while (timer > 0) {
            cam.transform.position = normal + Random.insideUnitSphere * 2;
            bluesc.gameObject.SetActive(false);
            timer -= Time.deltaTime;
            yield return null;
        }
        fond.GetComponent<SpriteRenderer>().sprite = newFond;
        Noctali.SetActive(true);
        post.gotToAnotherBoss = true;
        Evoli.GetComponent<Boss>().Die();
    }


}
