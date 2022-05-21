using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Plateforme : MonoBehaviour
{
    /** Script des plateformes
     * Cree par Jonathan Mores
     * Derniere modification: 05/05/22
     */
    private GameObject g_Joueur; // personnage
    private Collider2D c_Collider; // collider de la plateforme
    private bool b_JoueurSurPlateforme; // savoir si joueur sur plateforme
    private InputJoueur i_inputJoueur; // player input

    // Start is called before the first frame update
    void Start()
    {
        // initialiser les variables
        i_inputJoueur = new InputJoueur();
        c_Collider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // si on entre en collision avec le perso
        if (collision.gameObject.name.Contains("Beepo"))
        {
            // et qu'il est en train de sauter, appeler la fonction
            g_Joueur = collision.gameObject;
            i_inputJoueur.Player.Enable();
            i_inputJoueur.Player.Saut.performed += passerSousPlateforme;
            voirSiJoueurSurPlateforme(collision, true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // si on sort du contact avec le perso, rapeller la fonction, parametres diff
        if (collision.gameObject.name.Contains("Beepo"))
        {
            i_inputJoueur.Player.Disable();
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
        var collidersJoueur = g_Joueur.GetComponents<Collider2D>();
        if (b_JoueurSurPlateforme && g_Joueur.GetComponent<Joueur_Script>().accroupir)
        {
            StartCoroutine(ReactiveCollider());
            foreach (Collider2D mon in collidersJoueur)
            {
                Physics2D.IgnoreCollision(mon, GetComponent<Collider2D>());
            }
        }

    }
    private IEnumerator ReactiveCollider()
    {
        yield return new WaitForSeconds(0.5f);
        var collidersJoueur = g_Joueur.GetComponents<Collider2D>();
        foreach (Collider2D mon in collidersJoueur)
        {
            Physics2D.IgnoreCollision(mon, GetComponent<Collider2D>(), false);
        }
    }

}