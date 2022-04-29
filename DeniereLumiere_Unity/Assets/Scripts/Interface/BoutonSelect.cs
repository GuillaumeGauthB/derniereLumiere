using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Librairie besoin pour utiliser les evenements unity

public class BoutonSelect : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IMoveHandler // Interfaces requisent pour utiliser la fonction OnSelect() et onDeselect()
{
    public GameObject particules;

    public void OnPointerEnter(PointerEventData eventData)
    {
        entrerSelect();
    }

    public void OnMove(AxisEventData eventData)
    {
        Debug.Log("moved");
        entrerSelect();
    }

    // Fonction de Unity.EventSystems qui permet de détecter lorsque le gameobject est selectionné
    public void OnSelect(BaseEventData eventData)
    {
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