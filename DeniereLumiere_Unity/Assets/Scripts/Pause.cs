using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class Pause : MonoBehaviour
{
    /**
     * Classe qui permet de gerer tout ce qui touche au menu pause du jeu
     * Codeurs : Jerome
     * Derniere modification : 09/05/2022
    */
    //Particules du menu pause
    private bool b_enPause = false;

    private GameObject e_eventSystem;
    private GameObject joueur;
    public GameObject particules;
    public GameObject UIJeu;

    public GameObject controleManette;
    public GameObject controleClavier;

    public List<GameObject> boutons = new List<GameObject>();

    [HideInInspector]
    public bool peutDeplacer = true;

    private void Awake()
    {
        joueur = GameObject.FindGameObjectWithTag("Player");
        e_eventSystem = GameObject.FindGameObjectWithTag("eventSystem");
        InputJoueur i_inputJoueur = new InputJoueur();
        i_inputJoueur.Player.Enable();
        i_inputJoueur.Player.Pauser.performed += Pauser;
    }
    void Pauser(InputAction.CallbackContext context)
    {
        if (!b_enPause) PauserJeu();
        else DepauserJeu();
    }
    // Fonction qui permet de pauser le jeu et d'afficher le menu pause
    public void PauserJeu()
    {
        peutDeplacer = false;
        UIJeu.SetActive(false);
        e_eventSystem.GetComponent<InputSystemUIInputModule>().enabled = true;
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
        peutDeplacer = true;
        UIJeu.SetActive(true);
        e_eventSystem.GetComponent<InputSystemUIInputModule>().enabled = false;
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
    public void AfficherControles()
    {
       if (joueur.GetComponent<Joueur_Script>().modeSouris)
       {
            controleClavier.SetActive(true);
            controleManette.SetActive(false);
        } else
       {
            controleManette.SetActive(true);
            controleClavier.SetActive(false);
        }
    }
}