using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    /** Script de gestion de changement de scenesd
     * Cree par Jerome Trottier
     * Derniere modification: 20/05/22
     */

    // Methode qui permet de changer de scene en lui passant le nom de la scene voulu
    public void changerScene(string nomScene = "MenuJeu")
    {
        // Reset du timescale a 1, dans le cas ou le joueur est dans un menu pause lorsquil change de scene
        Time.timeScale = 1;
        SceneManager.LoadScene(nomScene);
    }

    // Methode qui permet de fermer le jeu
    public void quitterJeu()
    {
        Application.Quit();
    }
}
