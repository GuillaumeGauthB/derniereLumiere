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
     * Derniere modification : 20/05/2022
    */
 
    private bool b_enPause = false; // bool qui verifie si le jeu est pause ou pas

    private GameObject e_eventSystem; // EventSystem de la scene
    private GameObject joueur; // Beepo
    public GameObject particules; // les particules du menu pause
    public GameObject UIJeu; // Le canvas generale du jeu

    public GameObject controleManette; // Image des contoles manette
    public GameObject controleClavier; // Images des contoles clavier et souris

    public List<GameObject> boutons = new List<GameObject>(); // La liste des bouton du menu pause

    [HideInInspector]
    public bool peutDeplacer = true; // Si le joueur peut se deplacer

    private void Awake()
    {
        joueur = GameObject.FindGameObjectWithTag("Player");
        e_eventSystem = GameObject.FindGameObjectWithTag("eventSystem");
        InputJoueur i_inputJoueur = new InputJoueur();
        i_inputJoueur.Player.Enable();
        i_inputJoueur.Player.Pauser.performed += Pauser; // Activer les inputs pour pauser le jeu
    }
    void Pauser(InputAction.CallbackContext context)
    {
        if (!b_enPause) PauserJeu(); // si le jeu n'est pas en pause, pauser le jeu
        else DepauserJeu(); // sinon le depauser
    }
    // Fonction qui permet de pauser le jeu et d'afficher le menu pause
    public void PauserJeu()
    {
        peutDeplacer = false; // Le joueur ne peut plus se deplacer
        UIJeu.SetActive(false); // Le UI du jeu ne s'affiche plus
        e_eventSystem.GetComponent<InputSystemUIInputModule>().enabled = true; // On active les controles du UI
        GetComponent<Animator>().SetBool("EnPause", true); // Faire l'animation du menu pause
        Time.timeScale = 0; // On met en pause tous les elements du jeu
        foreach (var bouton in boutons)
        {
            bouton.GetComponent<BoutonSelect>().ActiverBoutons();
        }
        Cursor.visible = true; // On peut voir le curseur
        b_enPause = true; // On dit que le jeu est en pause
    }
    // Fonction qui permet de depauser le jeu et d'enlever le menu pause
    public void DepauserJeu()
    {
        peutDeplacer = true; // Le joueur peut se deplacer a nouveau
        UIJeu.SetActive(true); // On reactive le UI du jeu
        e_eventSystem.GetComponent<InputSystemUIInputModule>().enabled = false; // On desactive les controles du UI
        GetComponent<Animator>().SetBool("EnPause", false); // On fait l'animation de fermeture du menu pause
        Time.timeScale = 1; // On remet le jeu en vitesse normale
        b_enPause = false; // Le jeu n'est plus en pause
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

    // Fonction qui affiche les controles que le joueur utilise
    public void AfficherControles()
    {
       if (joueur.GetComponent<Joueur_Script>().modeSouris) // Si le joueur sur sur clavier, on affiche les controles clavier
       {
            controleClavier.SetActive(true);
            controleManette.SetActive(false);
        } else // On affiche les contoles manette
       {
            controleManette.SetActive(true);
            controleClavier.SetActive(false);
        }
    }
}