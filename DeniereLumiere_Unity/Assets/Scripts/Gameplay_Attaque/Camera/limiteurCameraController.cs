using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limiteurCameraController : MonoBehaviour
{
    /**
     * Classe qui permet de gerer quoi faire quand le joueur entre dans une nouvelle zone de camera
     * Codeurs : Jerome
     * Derniere modification : 20/05/2022
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Quand il entre dans une nouvelle zone, changer les limites de la camera
        if (collision.gameObject.tag == "limiteurCamera")
        {
            collision.GetComponent<limiteurCamera>().setNouvellesLimitesGlobales();
        }
    }
}
