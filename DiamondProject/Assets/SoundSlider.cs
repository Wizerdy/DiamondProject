using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class SoundSlider : MonoBehaviour {
    [SerializeField] bool _isActive;
    [SerializeField] Button _onOff;
    [SerializeField] Image _onOffImage;
    [SerializeField] Sprite _onSprite;
    [SerializeField] Sprite _offSprite;
    [SerializeField] Slider _slider;
    [SerializeField] TMP_Text _valueLabel;
    [SerializeField] TMP_Text _titleLabel;
    [SerializeField] UnityEvent<float> _onUpdate;

    float oldValue = 100f;

    public event UnityAction<float> OnUpdate { add => _onUpdate.AddListener(value); remove => _onUpdate.RemoveListener(value); }

    public void Start() {
        _onOffImage = _onOff.GetComponent<Image>();
        _onOffImage.sprite = _onOffImage.sprite;
        if (!_isActive) {
            oldValue = _slider.value;
            _slider.value = 0;
            _slider.enabled = false;
        } else {
            _slider.value = oldValue;
            _slider.enabled = true;
        }
        UpdateSlider();
    }

    public void InverseState() {
        _isActive = !_isActive;
        if (!_isActive) {
            oldValue = _slider.value;
            _slider.value = 0;
            _slider.enabled = false;
        } else {
            _slider.value = oldValue;
            _slider.enabled = true;
        }
        UpdateSlider();
    }

    public void UpdateSlider() {
        _onOffImage.sprite = _isActive ? _onSprite : _offSprite;
        _valueLabel.text = _slider.value.ToString();
        _onUpdate?.Invoke(_slider.value);
    }

    public void SetRTPCValueGeneralVolume(float value) {
        AkSoundEngine.SetRTPCValue("RTPC_GeneralVolume", value);
    }

    public void SetRTPCValueSFXVolume(float value) {
        AkSoundEngine.SetRTPCValue("RTPC_SFXVolume", value);
    }

    public void SetRTPCValueMusicVolume(float value) {
        AkSoundEngine.SetRTPCValue("RTPC_MusicVolume", value);
    }
}
