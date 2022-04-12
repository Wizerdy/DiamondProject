using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public PosterityObject obj;
    private void FadeIn(GameObject go, float duration) {
        var images = go.GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++) {
            var image = images[i];
            if (Mathf.Approximately(duration, 0f)) {

                Color tempColor = image.color;
                tempColor.a = 1;
                image.color = tempColor;
                break;
            } else {

                LeanTween.alpha(image.rectTransform, 1, duration).setEase(LeanTweenType.linear).setEase(LeanTweenType.linear);
            }
        }
    }
}
