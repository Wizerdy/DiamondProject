using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSpeed : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private PosterityObject posterity;

    // Start is called before the first frame update
    void Start()
    {
        slider.minValue = 1;
        slider.maxValue = 100;
        slider.wholeNumbers = true;
        slider.value = 50;
    }

    public void OnValueChange(float value) {
        posterity.textSpeed = value * 10;
        ApplyTextSpeed.instance?.ChangeTexteSpeed();
    }
}
