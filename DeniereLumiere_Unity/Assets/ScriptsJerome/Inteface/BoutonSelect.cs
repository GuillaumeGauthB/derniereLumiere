using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Librairie besoin pour utiliser les evenements unity

public class BoutonSelect : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler // Interfaces requisent pour utiliser la fonction OnSelect() et onDeselect()
{
    private List<GameObject> iconesHover = new List<GameObject>();

    private void Start()
    {
        obtenirIconesHover();
    }

    private void obtenirIconesHover()
    {
        //Boucle qui rempli la liste iconeHover des icones à activer et de désactiver autour des boutons
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.name == "IconeHoverRacine")
            {
                iconesHover.Add(transform.GetChild(i).gameObject);
            }
        }
    }

    // Fonction de Unity.EvenSystems qui permet de détecter lorsque le gameobject est selectionné
    public void OnSelect(BaseEventData eventData)
    {
        entrerSelect();
    }
    // Fonction de Unity.EvenSystems qui permet de détecter lorsque le gameobject est déselectionné
    public void OnDeselect(BaseEventData eventData)
    { 
        quitterSelect();
    }
    // Fonction de Unity.EvenSystems qui permet de détecter lorsque le gameobject est pointé par le curseur
    public void OnPointerEnter(PointerEventData eventData)
    {
        entrerSelect();
    }
    // Fonction de Unity.EvenSystems qui permet de détecter lorsque le curseur quitte le gameobject pointé
    public void OnPointerExit(PointerEventData eventData)
    {
        quitterSelect();
    }

    // Active les icones lorsque la fonction est appelée
    private void entrerSelect()
    {
        activerIcones();
    }

    // Trouve les icones et les activent
    private void activerIcones()
    {
        foreach (GameObject icone in iconesHover)
        {
            icone.gameObject.SetActive(true);
        }
    }

    // Désactive les icones lorsque la fonction est appelée
    private void quitterSelect()
    {
        desactiverIcones();
    }

    // Trouve les icones et les désactivent
    private void desactiverIcones()
    {
        foreach (GameObject icone in iconesHover)
        {
            icone.gameObject.SetActive(false);
        }
    }
}