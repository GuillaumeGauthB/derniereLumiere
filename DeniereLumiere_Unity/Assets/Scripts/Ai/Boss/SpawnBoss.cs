using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    /** Script pour faire apparaitre le boss
     * Cree par Jonathan Mores
     * Derniere modification: 29/04/22
     */

    public GameObject Boss; // le boss
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // si l'objet entre en collision avec le personnage
        if(collision.gameObject.tag == "Perso")
        {
            //activer le boss et activer son script d'attaque
            Boss.SetActive(true);
            Boss.GetComponent<BossAttaque>().enabled = true;
        }
        // detruire le gameObject
        Destroy(gameObject);
    }
}
