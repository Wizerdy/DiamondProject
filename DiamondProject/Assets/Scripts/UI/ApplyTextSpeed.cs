using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class ApplyTextSpeed : MonoBehaviour
{
    [SerializeField] private PosterityObject posterity;
    [SerializeField] private Writer defaultSayDialog;
    [SerializeField] private Writer smallSayDialog;
    [SerializeField] private Writer FlavorSaydialog;
    // Start is called before the first frame update
    void Start()
    {
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
