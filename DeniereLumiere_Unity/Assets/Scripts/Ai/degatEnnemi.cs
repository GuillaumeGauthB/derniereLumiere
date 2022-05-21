using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class degatEnnemi : MonoBehaviour
{
    /** Script de d?g?t des ennemis
     * Cr?? par Guillaume Gauthier-Beno?t
     * Derni?re modification: 07/04/22
     */

    public float ennemiVie; // La vie de l'ennemi
    public bool ennemiMort; // Le status de vie de l'ennemi
    public AudioClip sonMort;
    public bool CheckAnimMort = false;

    [Header("BOSS SETTINGS")]
    public GameObject barreDeVieUI;

    private void Start()
    {
        // Si on veut "automatiser" la vie des ennemis

        // if(gameObject.tag == "ennemi")
        // {
        //     f_ennemiVie = 2;
        // }
        // else if(gameObject.tag == "boss")
        // {
        //     f_ennemiVie = 100;
        // }
    }

    private void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Lorsque l'ennemi entre en collision avec un projectile du perso...
        if (collision.gameObject.tag.Contains("projectilePerso"))
        {
            // lui enlever de la vie et d?truire le projectile
            ennemiVie -= 1;
            if (barreDeVieUI != null)
            {
                barreDeVieUI.GetComponent<BarreDeVieController>().infligerDegatsBarreDeVie(100f/20f);
            }
            Destroy(collision.gameObject);
            // Lorsque l'ennemi n'a plus de vie, le tuer
            if (ennemiVie <= 0)
            {
                Animator animationBoss = GetComponent<Animator>();
                animationBoss.SetBool("Mort", true);
                ennemiMort = true;
            

            // Lorsqu'il est mort, faire les actions n?cessasire avant de le d?truire
            
                // desactiver les mouvements et le UI (?)
                // desactiver les animations
                // desactiver les colliders
                // detruire gameObject apres l'animation
                // joue le son de mort de l'ennemi si il existe
                if (sonMort)
                {
                    GetComponent<AudioSource>().PlayOneShot(sonMort);
                }
                
                var collidersEnnemi = gameObject.GetComponents<Collider2D>();
                foreach (Collider2D mon in collidersEnnemi)
                {
                    mon.enabled = false;
                }
                
                
                if (CheckAnimMort == true)
                {
                    gameObject.GetComponent<Animator>().SetTrigger("Mort");
                }
                else
                {
                    gameObject.GetComponent<Animator>().enabled = false;
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }
                Destroy(gameObject, 5f);
            }
        }

    }
}