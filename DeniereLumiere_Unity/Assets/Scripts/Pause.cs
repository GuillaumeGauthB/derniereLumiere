using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    /**
     * Classe qui permet de gerer tout ce qui touche au menu pause du jeu
     * Codeurs : Jerome
     * Derniere modification : 09/05/2022
    */
    //Particules du menu pause
    private bool b_enPause = false;
    public GameObject particules;

    public List<GameObject> boutons = new List<GameObject>();

    private void Update()
    {
        // Quand on appuie sur Escape, le jeu est mit en pause
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!b_enPause) PauserJeu();
            else DepauserJeu();
        }
    }

    // Fonction qui permet de pauser le jeu et d'afficher le menu pause
    public void PauserJeu()
    {
        GetComponent<Animator>().SetBool("EnPause", true);
        Time.timeScale = 0;
        foreach (var bouton in boutons)
        {
            bouton.GetComponent<BoutonSelect>().ActiverBoutons();
        }
        Cursor.visible = true;
        b_enPause = true;
    }
    // Fonction qui permet de depauser le jeu et d'enlever le menu pause
    public void DepauserJeu()
    {
        GetComponent<Animator>().SetBool("EnPause", false);
        Time.timeScale = 1;
        b_enPause = false;
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