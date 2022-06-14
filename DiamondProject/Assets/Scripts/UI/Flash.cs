using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Flash : MonoBehaviour {
    public float flashTimelength = .2f;
    public bool doCameraFlash = false;
    private Image flashImage;
    private float startTime;
    private bool flashing = false;

    void Start() {
        flashImage = GetComponent<Image>();
        Color col = flashImage.color;
        col.a = 0.0f;
        flashImage.color = col;
    }

    void Update() {
        if (doCameraFlash && !flashing) {
            CameraFlash();
        } else {
            doCameraFlash = false;
        }
    }

    public void CameraFlash() {
        Color col = flashImage.color;
        startTime = Time.time;
        doCameraFlash = false;
        col.a = 1.0f;
        flashImage.color = col;
        flashing = true;

        StartCoroutine(FlashCoroutine());
    }

    IEnumerator FlashCoroutine() {
        bool done = false;

        while (!done) {
            float perc;
            Color col = flashImage.color;

            perc = Time.time - startTime;
            perc = perc / flashTimelength;

            if (perc > 1.0f) {
                perc = 1.0f;
                done = true;
            }

            col.a = Mathf.Lerp(1.0f, 0.0f, perc);
            flashImage.color = col;
            flashing = true;

            yield return null;
        }

        flashing = false;

        yield break;
    }
}