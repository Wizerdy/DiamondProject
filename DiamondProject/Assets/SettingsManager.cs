using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour {
    [SerializeField] SoundSlider _generalSlider;
    [SerializeField] SoundSlider _musicSlider;
    [SerializeField] SoundSlider _sfxSlider;
    private void Start() {
        _generalSlider.Start();
        _musicSlider.Start();
        _sfxSlider.Start();
        gameObject.SetActive(false);
    }
}
