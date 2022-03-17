using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Plateforme : MonoBehaviour
{
    public GameObject joueur;
    private Collider2D c_Collider;
    
    // Start is called before the first frame update
    void Start()
    {
        c_Collider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == joueur)
        {
            joueur.GetComponent<Joueur_Script>().voirSiJoueurSurPlateforme(collision, true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == joueur)
        {
            joueur.GetComponent<Joueur_Script>().voirSiJoueurSurPlateforme(collision, false);
        }
    }
}
