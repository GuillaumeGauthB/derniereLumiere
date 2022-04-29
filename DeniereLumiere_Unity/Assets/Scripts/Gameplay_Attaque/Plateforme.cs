using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Plateforme : MonoBehaviour
{
    private GameObject g_Joueur;
    private Collider2D c_Collider;
    private bool b_JoueurSurPlateforme;
    private InputJoueur i_inputJoueur;

    // Start is called before the first frame update
    void Start()
    {
        i_inputJoueur = new InputJoueur();
        c_Collider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Beepo"))
        {
            g_Joueur = collision.gameObject;
            i_inputJoueur.Player.Enable();
            i_inputJoueur.Player.Saut.performed += passerSousPlateforme;
            voirSiJoueurSurPlateforme(collision, true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
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