using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorteProchainNiv : MonoBehaviour
{
    /** Script pour aller au niveau2
     * Cree par Jerome Trottier
     * Derniere modification: 05/05/22
     */
    public GameObject boss; // le boss
    public GameObject SceneController; // le controller de scene
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // si le boss n'est pas sur la scene et que le perso entre en contact avec la porte, le changer de niveau
        if (collision.gameObject.CompareTag("Player") && boss == null)
        {
            SceneController.GetComponent<SceneController>().changerScene("Niveau2");
        }
    }
}
