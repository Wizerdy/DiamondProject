using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class ApplyTextSpeed : MonoBehaviour
{
    public static ApplyTextSpeed instance;

    [SerializeField] private PosterityObject posterity;
    [SerializeField] private Writer defaultSayDialog;
    [SerializeField] private Writer smallSayDialog;
    [SerializeField] private Writer FlavorSaydialog;

    void Start()
    {
        if (instance != null) {
            Destroy(gameObject); 
            return; 
        }

        instance = this;
        DontDestroyOnLoad(this);

        defaultSayDialog.SetTextSpeed(posterity.textSpeed);
        smallSayDialog.SetTextSpeed(posterity.textSpeed);
        FlavorSaydialog.SetTextSpeed(posterity.textSpeed);
    }

    public void ChangeTexteSpeed() {
        defaultSayDialog.SetTextSpeed(posterity.textSpeed);
        smallSayDialog.SetTextSpeed(posterity.textSpeed);
        FlavorSaydialog.SetTextSpeed(posterity.textSpeed);
    }
}
