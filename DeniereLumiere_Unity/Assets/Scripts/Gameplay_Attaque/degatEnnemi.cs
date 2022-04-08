using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class degatEnnemi : MonoBehaviour
{
    /** Script de d�g�t des ennemis
     * Cr�� par Guillaume Gauthier-Beno�t
     * Derni�re modification: 07/04/22
     */

    public float ennemiVie; // La vie de l'ennemi
    public bool ennemiMort; // Le status de vie de l'ennemi

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
        // Lorsque l'ennemi n'a plus de vie, le tuer
        if (ennemiVie <= 0)
        {
            ennemiMort = true;
        }

        // Lorsqu'il est mort, faire les actions n�cessasire avant de le d�truire
        if (ennemiMort)
        {
            // desactiver les mouvements et le UI (?)
            // desactiver les animations
            // desactiver les colliders
            // detruire gameObject apres l'animation
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Lorsque l'ennemi entre en collision avec un projectile du perso...
        if (collision.gameObject.tag.Contains("projectilePerso"))
        {
            // lui enlever de la vie et d�truire le projectile
            ennemiVie -= 1;
            Destroy(collision.gameObject);
        }

    }
}