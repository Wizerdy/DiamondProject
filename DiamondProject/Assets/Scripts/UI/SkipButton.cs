using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;
using UnityEngine.EventSystems;

public class SkipButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Button btn;
    [SerializeField] private DialogInput dialogInput;
    [SerializeField] private float cooldDownBeforeNextLine;
    private bool isPressed;
    private float timer;

    void Update()
    {
        if (isPressed) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                dialogInput.SetNextLineFlag();
                timer = cooldDownBeforeNextLine;
            }
        }

    }

    public void OnPointerDown(PointerEventData eventData) {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData) {
        isPressed = false;
    }
}
