using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetHintFeedBack : MonoBehaviour
{
    [SerializeField] private Image imageFeedBack;
    [SerializeField] private float fadeSpeed = 1.2f;
    [SerializeField] private float duration = 5f;

    private bool isInvisible = false;
    private float transparancy = 1;
    private float time = 5f;

    private void OnEnable() {
        time = duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0) {
            if (!isInvisible) {
                transparancy -= Time.deltaTime * fadeSpeed;
                Color color = new Color(imageFeedBack.color.r, imageFeedBack.color.g, imageFeedBack.color.b, transparancy);
                imageFeedBack.color = color;
                if (imageFeedBack.color.a <= 0.2f)
                    isInvisible = true;
            } else {
                transparancy += Time.deltaTime * fadeSpeed;
                Color color = new Color(imageFeedBack.color.r, imageFeedBack.color.g, imageFeedBack.color.b, transparancy);
                imageFeedBack.color = color;
                if (imageFeedBack.color.a >= 1)
                    isInvisible = false;
            }
            time -= Time.deltaTime;
        } else {
            gameObject.SetActive(false);
        }

    }
}
