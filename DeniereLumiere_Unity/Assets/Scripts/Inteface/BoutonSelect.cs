using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Librairie besoin pour utiliser les evenements unity

public class BoutonSelect : MonoBehaviour, ISelectHandler, IPointerEnterHandler // Interfaces requisent pour utiliser la fonction OnSelect() et onDeselect()
{
    public GameObject particules;

    // Fonction de Unity.EvenSystems qui permet de détecter lorsque le gameobject est selectionné
    public void OnSelect(BaseEventData eventData)
    {
        entrerSelect();
    }
    // Fonction de Unity.EvenSystems qui permet de détecter lorsque le gameobject est pointé par le curseur
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(gameObject);
        entrerSelect();
    }
    // Active les icones lorsque la fonction est appelée
    private void entrerSelect()
    {
        envoyerPositionParticules();
    }

    // Trouve les icones et les activent
    private void envoyerPositionParticules()
    {
        if (particules.activeInHierarchy) particules.GetComponent<ParticuleSystemeMenu>().setNouvellePosition(transform.position.y);
    }
}