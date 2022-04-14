using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Librairie besoin pour utiliser les evenements unity

public class BoutonSelect : MonoBehaviour, ISelectHandler // Interfaces requisent pour utiliser la fonction OnSelect() et onDeselect()
{
    public GameObject particules;

    

    // Fonction de Unity.EvenSystems qui permet de détecter lorsque le gameobject est selectionné
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(gameObject.name);
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