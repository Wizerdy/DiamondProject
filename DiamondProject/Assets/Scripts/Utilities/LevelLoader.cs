using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
    [SerializeField] UnityEvent<Scene> _onExitScene;
    float _timeScale = 0f;
    AsyncOperation _sceneLoading;

    public event UnityAction<Scene> OnExitScene { add => _onExitScene.AddListener(value); remove => _onExitScene.RemoveListener(value); }

    public void Update() {
        _timeScale = Time.timeScale;
    }

    public IEnumerator LoasAsyncScene(string name, LoadSceneMode mode, bool autoTransition = true) {
        _sceneLoading = SceneManager.LoadSceneAsync(name, mode);
        if (!autoTransition) {
            _sceneLoading.allowSceneActivation = false;
        }

        while (!_sceneLoading.isDone) {
            yield return null;
        }
    }

    public void LoadSubLevel(string name) {
        StartCoroutine(LoasAsyncScene(name, LoadSceneMode.Additive));
    }

    public void LoadLevel(string name) {
        StartCoroutine(LoasAsyncScene(name, LoadSceneMode.Single));
    }

    public void LoadLevel(string name, bool autoLoadScene) {
        StartCoroutine(LoasAsyncScene(name, LoadSceneMode.Single, autoLoadScene));
    }

    public void ChangeToLoadedScene() {
        try {
            _onExitScene?.Invoke(SceneManager.GetActiveScene());
        } catch (Exception e) {
            Debug.LogException(e);
        }
        _sceneLoading.allowSceneActivation = true;
    }

    void OnEnable() {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    public void TimeScaleToOne() {
        Time.timeScale = 1f;
    }

    public void ChangeTimeScale(float target) {
        Time.timeScale = target;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
        Debug.Log("Level Loaded " + scene.name + " " + mode + " TimeScale : " + Time.timeScale);
        Time.timeScale = 1f;
    }
}
