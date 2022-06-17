using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GraphismsSettings : MonoBehaviour {
    Resolution[] _resolutions;
    [SerializeField] int _selectedResolution = 0;
    //[SerializeField] int _selectedFramerate = 0;
    [SerializeField] Toggle _fullScreen, _vsync;
    [SerializeField] TMP_Text _resolutionLabel;
    //[SerializeField] TMP_Text _frameRateLabel;

    private void Start() {
        _resolutions = Screen.resolutions;
        _fullScreen.isOn = Screen.fullScreen;
        _vsync.isOn = QualitySettings.vSyncCount == 0 ? false : true;
        List<Resolution> res = new List<Resolution>();
        //int framerateMax = 0;
        for (int i = 0; i < _resolutions.Length; i++) {
            //Debug.Log(_resolutions[i].width);
            //Debug.Log(_resolutions[i].height);
            //Debug.Log(_resolutions[i].refreshRate);
            //if (framerateMax < _resolutions[i].refreshRate) {
            //    framerateMax = _resolutions[i].refreshRate;
            //    _selectedFramerate = i;
            //}
            //if (!_frameRate.Contains(_resolutions[i].refreshRate)) {
            //    _frameRate.Add(_resolutions[i].refreshRate);
            //}
            _resolutions[i].refreshRate = 0;
        }
        for (int i = 0; i < _resolutions.Length; i++) {
            if (!res.Contains(_resolutions[i])) {
                res.Add(_resolutions[i]);
            }
        }
        _resolutions = res.ToArray();
        for (int i = 0; i < _resolutions.Length; i++) {
            if (Screen.width == _resolutions[i].width && Screen.height == _resolutions[i].height) {
                _selectedResolution = i;
                break;
            }
        }
        UpdateLabelResolution();
        //UpdateLabelFramerate();
    }

    public void NextResolution() {
        _selectedResolution = ++_selectedResolution >= _resolutions.Length ? 0 : _selectedResolution;
        UpdateLabelResolution();
    }

    public void PreviousResolution() {
        _selectedResolution = --_selectedResolution < 0 ? _resolutions.Length - 1 : _selectedResolution;
        UpdateLabelResolution();
    }

    public void UpdateLabelResolution() {
        _resolutionLabel.text = _resolutions[_selectedResolution].width.ToString() + " x " + _resolutions[_selectedResolution].height.ToString();
    }

    //public void NextFramerate() {
    //    _selectedFramerate = ++_selectedFramerate >= _frameRate.Count ? 0 : _selectedFramerate;
    //    UpdateLabelFramerate();
    //}

    //public void PreviousFramerate() {
    //    _selectedFramerate = --_selectedFramerate < 0 ? _frameRate.Count - 1 : _selectedFramerate;
    //    UpdateLabelFramerate();
    //}

    //public void UpdateLabelFramerate() {
    //    _frameRateLabel.text = _frameRate[_selectedFramerate].ToString() + " FPS";
    //}
    public void ApplyGraphism() {
        QualitySettings.vSyncCount = _vsync.isOn ? 1 : 0;
        Screen.SetResolution(_resolutions[_selectedResolution].width, _resolutions[_selectedResolution].height, _fullScreen.isOn);
    }
}
