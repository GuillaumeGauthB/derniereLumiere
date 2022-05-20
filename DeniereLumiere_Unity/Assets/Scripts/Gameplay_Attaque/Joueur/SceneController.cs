using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{ 
    public void changerScene(string nomScene = "MenuJeu")
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nomScene);
    }
    public void quitterJeu()
    {
        Application.Quit();
    }
}
