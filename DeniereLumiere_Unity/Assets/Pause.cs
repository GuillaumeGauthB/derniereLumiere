using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    /**
     * Classe qui permet de gerer tout ce qui touche au menu pause du jeu
     * Codeurs : Jerome
     * Derniere modification : 25/04/2022
    */
    //Particules du menu pause
    public GameObject particules;

    private void Update()
    {
        // Quand on appuie sur Escape, le jeu est mit en pause
        if (Input.GetKey(KeyCode.Escape))
        {
            PauserJeu();
        }
    }

    // Fonction qui permet de pauser le jeu et d'afficher le menu pause
    public void PauserJeu()
    {
        GetComponent<Animator>().SetBool("EnPause", true);
        Time.timeScale = 0;
        Cursor.visible = true;
    }
    // Fonction qui permet de depauser le jeu et d'enlever le menu pause
    public void DepauserJeu()
    {
        GetComponent<Animator>().SetBool("EnPause", false);
        Time.timeScale = 1;
    }
    // Fonction qui permet d'activer les particules du menu pause (particules de hover)
    public void activerParticules()
    {
        particules.SetActive(true);
    }
    // Fonction qui permet de desactiver les particules du menu pause
    public void desactiverParticules()
    {
        particules.SetActive(false);
    }
}