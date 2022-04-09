using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class degatPerso : MonoBehaviour
{
    /** Script de d�g�t du joueur
     * Cr�� par Guillaume Gauthier-Beno�t
     * Derni�re modification: 07/04/22
     */

    public GameObject BarreDeVie;
    private float f_posXEvP; //position ennemi vs personnage en x
    public bool knockbackPerso, // valeurs boolean des effets
        invincible,
        mort;
    public float viePerso; // vie initiale du personnage
    public AudioClip sonDegats;

    // Update is called once per frame
    void Update()
    {
        // Lorsque la vie du personnage atteint 0 ou moins, le tuer
        if(viePerso <= 0)
        {
            mort = true;
            
        }
        if (mort == true)
        {
            SceneManager.LoadScene("MenuJeu");
        }
    }

    // Fonction de collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {   
        if(collision.gameObject.tag == "mort")
        {
            mort = true;
        }

        // Lorsque le personnage entre en collision avec un objet avec le tag boss ou ennemi...
        if(collision.gameObject.tag == "ennemi" || collision.gameObject.tag == "boss")
        {
            // Le rendre temporairement invincible
            if (!invincible)
            {
                viePerso--;
                GetComponent<AudioSource>().PlayOneShot(sonDegats);
                BarreDeVie.GetComponent<BarreDeVieController>().infligerDegatsBarreDeVie(10f);
                invincible = true;
                Invoke("Invincibilite", 2);
                
            }

            // Donner du knockback au personnage d�pendamment de la position du personnage vs l'ennemi, qui va se d�sactiver apr�s 0.5 secondes
            knockbackPerso = true;
            Invoke("FinKnockback", 0.5f);
            if (collision.gameObject.transform.position.x >= gameObject.transform.position.x)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(f_posXEvP * 100, 30f));
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-f_posXEvP * 100, 30f));
            }
        }
    }

    // Fonction qui d�sactive le knockback
    void FinKnockback()
    {
        // D�sactiver le knockback
        knockbackPerso = false;
    }

    void Invincibilite()
    {
        invincible = false;
    }
}
