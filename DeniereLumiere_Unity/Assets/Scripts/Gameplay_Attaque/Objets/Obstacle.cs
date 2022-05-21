using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    /**
     * Le script qui desactive le collider du mur quand on est en dash
     * Par Guillaume
     * Dernier modification: 05/05/22
     */
    private void OnCollisionStay2D(Collision2D collision)
    {
        // Si le mur est en collision avec le joueur et qu'il est en dash,
        // il desactive son collider et le reactive dans 0.5 secondes
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Joueur_Script>().estDash)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Invoke("MettreCollider", 0.5f);
        }
    }
    //Fonction qui reactive le collider
    void MettreCollider()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }


}
