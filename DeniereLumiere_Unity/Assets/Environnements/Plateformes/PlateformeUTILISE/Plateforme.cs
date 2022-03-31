using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Plateforme : MonoBehaviour
{
    
    private Collider2D c_Collider;
    
    // Start is called before the first frame update
    void Start()
    {
        c_Collider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name.Contains("Beepo"))
        {
            
            collision.gameObject.GetComponent<Joueur_Script>().voirSiJoueurSurPlateforme(collision, true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Beepo"))
        {
            collision.gameObject.GetComponent<Joueur_Script>().voirSiJoueurSurPlateforme(collision, false);
        }
    }
}
