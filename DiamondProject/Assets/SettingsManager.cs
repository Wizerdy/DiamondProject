using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour {
    [SerializeField] SoundSlider _generalSlider;
    [SerializeField] SoundSlider _musicSlider;
    [SerializeField] SoundSlider _sfxSlider;
    [SerializeField] bool _mustclose;
    private void Start() {
        _generalSlider.GetRTPCValueGeneralVolume();
        _musicSlider.GetRTPCValueMusicVolume();
        _sfxSlider.GetRTPCValueSFXVolume();
        if (_mustclose)
            gameObject.SetActive(false);
    }
}
