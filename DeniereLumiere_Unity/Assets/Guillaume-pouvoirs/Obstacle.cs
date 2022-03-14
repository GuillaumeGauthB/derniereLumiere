using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Joueur_Script>().estDash)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Invoke("MettreCollider", 1f);
        }
    }

    void MettreCollider()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }


}
