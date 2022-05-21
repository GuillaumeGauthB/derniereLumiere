using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayonFeuScript : MonoBehaviour
{
    /** Script du rayon de feu du premier boss
     * Cree par Jonathan Mores
     * Derniere modification: 29/04/22
     */

    public GameObject cible; // la cible du rayon
    void Start()
    {
        // mettre le rayon sur la cible et le detruire apres 4 secondes
        transform.position = cible.transform.position;
        Destroy(gameObject, 4f);
    }
}
