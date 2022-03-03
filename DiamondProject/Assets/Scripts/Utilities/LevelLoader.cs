using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
    public IEnumerator LoasAsyncScene(string name, LoadSceneMode mode) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name, mode);

        while (!asyncLoad.isDone) {
            yield return null;
        }
    }

    public void LoadSubLevel(string name) {
        StartCoroutine(LoasAsyncScene(name, LoadSceneMode.Additive));
    }

    public void LoadLevel(string name) {
        StartCoroutine(LoasAsyncScene(name, LoadSceneMode.Single));
    }
}
