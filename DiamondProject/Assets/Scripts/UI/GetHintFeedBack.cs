using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetHintFeedBack : MonoBehaviour
{
    [SerializeField] private Image imageFeedBack;
    [SerializeField] private float fadeSpeed = 1.2f;

    // Update is called once per frame
    void Update()
    {
        float a = Mathf.PingPong(Time.deltaTime * fadeSpeed, 1);
        Color color = new Color(imageFeedBack.color.r, imageFeedBack.color.g, imageFeedBack.color.b, a);
        imageFeedBack.color = color;
    }
}
