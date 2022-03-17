using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Plateforme : MonoBehaviour
{
    public GameObject joueur;
    private Collider2D c_Collider;
    private bool b_JoueurSurPlateforme;
    private InputJoueur i_inputJoueur;

    // Start is called before the first frame update
    void Start()
    {
        c_Collider = GetComponent<Collider2D>();
        i_inputJoueur.Player.Saut.performed += passerSousPlateforme;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == joueur)
        {
            voirSiJoueurSurPlateforme(collision, true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == joueur)
        {
            voirSiJoueurSurPlateforme(collision, false);
        }
    }
    void voirSiJoueurSurPlateforme(Collision2D collision, bool value)
    {
        var player = collision.gameObject.GetComponent<Joueur_Script>();
        if (player != null)
        {
            b_JoueurSurPlateforme = value;
        }
    }
    void passerSousPlateforme(InputAction.CallbackContext context)
    {

    }

}
