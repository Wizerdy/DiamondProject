using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchTextBox : MonoBehaviour
{
    [SerializeField] private Sprite imageWithName;
    [SerializeField] private Sprite imageWithout;

    [SerializeField] private GameObject textBoxObj;

    private Image currentImage;
    // Start is called before the first frame update
    void Start()
    {
        currentImage = textBoxObj.GetComponent<Image>();
    }

    public void SwitchToWithName() {
        currentImage.sprite = imageWithName;
    }

    public void SwitchToWithout() {
        currentImage.sprite = imageWithout;
    }
}
