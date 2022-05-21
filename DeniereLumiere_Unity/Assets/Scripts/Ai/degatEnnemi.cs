using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class degatEnnemi : MonoBehaviour
{
    /** Script de degat des ennemis
     * Cree par Guillaume Gauthier-Beno?t
     * Derniere modification: 20/05/22
     */

    public float ennemiVie; // La vie de l'ennemi
    public bool ennemiMort; // Le status de vie de l'ennemi
    public AudioClip sonMort; // Le son de mort de l'ennemi
    public GameObject ObjetAApparaitre;
    public bool animationMort = false;

    [Header("BOSS SETTINGS")]
    public GameObject barreDeVieUI; // la barre de vie du boss

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Lorsque l'ennemi entre en collision avec un projectile du perso...
        if (collision.gameObject.tag.Contains("projectilePerso"))
        {
            // lui enlever de la vie et detruire le projectile
            ennemiVie -= 1;
            // Si l'ennemi possede une barre de vie...
            if (barreDeVieUI != null)
            {
                // La faire baisser
                barreDeVieUI.GetComponent<BarreDeVieController>().infligerDegatsBarreDeVie(100f/20f);
            }
            // Detruire le projectile
            Destroy(collision.gameObject);


            // Lorsque l'ennemi n'a plus de vie, le tuer
            if (ennemiVie <= 0)
            {
                
                // changer le status de l'ennemi
                ennemiMort = true;
            

            // Lorsqu'il est mort, faire les actions necessasire avant de le detruire
            
                // S'il possede un son de mort, le jouer
                if (sonMort)
                {
                    GetComponent<AudioSource>().PlayOneShot(sonMort);
                }
                
                // Desactiver tous ses colliders 2d
                var collidersEnnemi = gameObject.GetComponents<Collider2D>();
                foreach (Collider2D mon in collidersEnnemi)
                {
                    mon.enabled = false;
                }

                if (ObjetAApparaitre)
                {
                    ObjetAApparaitre.SetActive(true);
                }

                // joueur son animation de mort
                if (animationMort == true)
                {
                    Animator animationBoss = GetComponent<Animator>();
                    animationBoss.SetTrigger("Mort");
                    Destroy(gameObject, 2f);
                }
                else
                {
                    // Detruire l'ennemi tout de suite
                    Destroy(gameObject);
                }
                
            }
        }

    }
}