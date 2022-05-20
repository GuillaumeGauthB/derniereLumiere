using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class MenuJeu : MonoBehaviour
{
    /** Script de gestion du menu principal du jeu
     * Cree par Jerome Trottier
     * Derniere modification: 19/05/22
     */

    [Header("Cadre du menu")]
    public GameObject cadre;

    [Header("Particules du menu")]
    public GameObject particules;

    [Header("Texte temporaire du menu titre")]
    public GameObject texteTitre;

    [Header("EventSystem du canvas")]
    public GameObject eventSystem;

    private Animator a_animator;
    private void Start()
    {
        a_animator = GetComponent<Animator>();
    }
    private void Update()
    {
        // Changement de l'affichage du Titre a l'affichage du UI pour naviguer dans les menus
        if (Input.anyKey)
        {
            ouvrirMenu();
        }
    }
    // Methode qui permet d'ouvrir le menu
    private void ouvrirMenu()
    {
        // Permettre la navigation dans le menu
        eventSystem.GetComponent<InputSystemUIInputModule>().enabled = true;
        // Faire l'animation du menu
        a_animator.SetBool("afficherMenu", true);
    }

    // Methodes utilisee en Event dans l'animation afficherMenu 
    public void activerCadre()
    {
        cadre.SetActive(true);
    }
    public void activerParticules()
    {
        particules.SetActive(true);
    }
    public void desactiverTexteTitre()
    {
        texteTitre.SetActive(false);
    }
}
