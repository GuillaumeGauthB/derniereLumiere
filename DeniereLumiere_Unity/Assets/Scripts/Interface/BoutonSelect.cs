using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Librairie besoin pour utiliser les evenements unity

/**
     * Classe qui permet de gerer tout ce qui aux boutons de jeu
     * Codeurs : Jerome
     * Derniere modification : 25/04/2022
    */
public class BoutonSelect : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IMoveHandler // Interfaces requisent pour utiliser la fonction OnSelect() et onDeselect()
{
    public GameObject particules;
    public AudioSource audioCanvas;
    public AudioClip sonMove;
    public AudioClip sonSelect;

    public bool firstSelected = false;

    public void ActiverBoutons()
    {
        if (firstSelected)
        {
            GameObject eventSystem = GameObject.FindGameObjectWithTag("eventSystem");
            eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(gameObject);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        entrerSelect();
    }

    public void OnMove(AxisEventData eventData)
    {
        audioCanvas.PlayOneShot(sonMove);
    }

    // Fonction de Unity.EventSystems qui permet de détecter lorsque le gameobject est selectionné
    public void OnSelect(BaseEventData eventData)
    {
        audioCanvas.PlayOneShot(sonSelect);
        entrerSelect();
    }
    // Active les icones lorsque la fonction est appelée
    public void entrerSelect()
    {
        envoyerPositionParticules();
    }

    // Trouve les icones et les activent
    private void envoyerPositionParticules()
    {
        if (particules.activeInHierarchy) particules.GetComponent<ParticuleSystemeMenu>().setNouvellePosition(transform.position.y);
    }
}