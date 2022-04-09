using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class degatEnnemi : MonoBehaviour
{
    /** Script de dégât des ennemis
     * Créé par Guillaume Gauthier-Benoît
     * Dernière modification: 07/04/22
     */

    public float ennemiVie; // La vie de l'ennemi
    public bool ennemiMort; // Le status de vie de l'ennemi
    public AudioClip sonMort;

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
            // lui enlever de la vie et détruire le projectile
            ennemiVie -= 1;
            Destroy(collision.gameObject);
            // Lorsque l'ennemi n'a plus de vie, le tuer
            if (ennemiVie <= 0)
            {
                ennemiMort = true;
            

            // Lorsqu'il est mort, faire les actions nécessasire avant de le détruire
            
                // desactiver les mouvements et le UI (?)
                // desactiver les animations
                // desactiver les colliders
                // detruire gameObject apres l'animation
                // joue le son de mort de l'ennemi si il existe
                if (sonMort)
                {
                    GetComponent<AudioSource>().PlayOneShot(sonMort);
                }
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<Collider2D>().enabled = false;
                gameObject.GetComponent<Animator>().enabled = false;
                Destroy(gameObject, 2f);
            }
        }

    }
}