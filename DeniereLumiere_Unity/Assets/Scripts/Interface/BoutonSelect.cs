using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Librairie besoin pour utiliser les evenements unity

/**
     * Classe qui permet de gerer tout ce qui touche aux boutons de jeu
     * Codeurs : Jerome
     * Derniere modification : 19/05/2022
    */
// La classe herite de plusieurs interfaces requisent pour detecter quand le joueur select un bouton, pointe un bouton, change de bouton et clique sur un bouton
public class BoutonSelect : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IMoveHandler, ISubmitHandler 
{
    [Header("Particules menu")]
    public GameObject particules;
    [Header("Sons")]
    public AudioSource audioCanvas;
    public AudioClip sonMove;
    public AudioClip sonSelect;

    // Variable qui determine si le bouton va etre select quand le menu sera ouvert pour la premiere fois
    public bool firstSelected = false;

    //Methode qui selectionne le bouton avec firstSelected de cocher dans l'inspecteur
    public void ActiverBoutons()
    {
        if (firstSelected)
        {
            setBoutonSelect(gameObject);
        }
    }

    // Methode qui selectionne un bouton automatiquement a la place du joueur
    public void setBoutonSelect(GameObject boutonParDefaut)
    {
        GameObject eventSystem = GameObject.FindGameObjectWithTag("eventSystem");
        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(boutonParDefaut);
    }

    // Methode qui herite de IPointerEnterHandler pour detecter quand le joueur pointe avec le curseur un bouton 
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Selectionner le bouton pointé
        setBoutonSelect(gameObject);
        // On change la position des particules
        envoyerPositionParticules();
        // Jouer le son de selection
        audioCanvas.PlayOneShot(sonMove);
    }

    // Quand le joueur navigue avec les touches de clavier ou boutons manettes, la methode OnMove est appelee
    public void OnMove(AxisEventData eventData)
    {
        // Jouer le son de navigation
        audioCanvas.PlayOneShot(sonMove);
    }

    // Fonction de Unity.EventSystems qui permet de détecter lorsque le gameobject est selectionné
    public void OnSelect(BaseEventData eventData)
    {
        // Quand un bouton est selectionne, on change la position des particules
        envoyerPositionParticules();
    }

    public void OnSubmit(BaseEventData eventData)
    {
        // Quand le joueur appuie sur un bouton, on fait jouer le son de selection du bouton
        audioCanvas.PlayOneShot(sonSelect);
    }

    // Fonction qui permet de changer la position des particules a celle du bouton
    private void envoyerPositionParticules()
    {
        if (particules.activeInHierarchy) particules.GetComponent<ParticuleSystemeMenu>().setNouvellePosition(transform.position.y);
    }
   
}