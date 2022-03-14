using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouvoirsCollection : MonoBehaviour
{


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "obtentionDoubleSaut" && Joueur_Script.b_doubleSautObtenu)
        {
            // Faire apparaitre le texte d'interaction
            // Changer la valeure de la variable
            Joueur_Script.b_doubleSautObtenu = true;
        }
    }
}
