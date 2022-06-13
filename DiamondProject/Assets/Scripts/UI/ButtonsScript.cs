using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsScript : MonoBehaviour {
    public void ChangeScene(string newScene) {
        SceneManager.LoadScene(newScene);
    }

    public void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StopTime() {
        Time.timeScale = 0;
    }

    public void StartTime() {
        Time.timeScale = 1;
    }

    public void Quit() {
        Application.Quit();
    }
}
