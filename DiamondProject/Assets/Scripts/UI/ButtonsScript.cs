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


}
