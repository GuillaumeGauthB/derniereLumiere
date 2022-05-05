using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class degatPerso : MonoBehaviour
{
    /** Script de dégât du joueur
     * Créé par Guillaume Gauthier-Benoît
     * Dernière modification: 07/04/22
     */

    public GameObject BarreDeVie;
    private float f_posXEvP; //position ennemi vs personnage en x
    public bool knockbackPerso, // valeurs boolean des effets
        invincible,
        mort;
    public float viePerso; // vie initiale du personnage
    public AudioClip sonDegats;
    public Color couleurDegat,
        couleurOri;

    private void Start()
    {
        couleurOri = gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>().color;
    }
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
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Lorsque le personnage entre en collision avec un objet avec le tag boss ou ennemi...
        if (collision.gameObject.tag == "ennemi" || collision.gameObject.tag == "boss")
        {
            // Le rendre temporairement invincible
            if (!invincible)
            {
                viePerso--;
                GetComponent<AudioSource>().PlayOneShot(sonDegats);
                BarreDeVie.GetComponent<BarreDeVieController>().infligerDegatsBarreDeVie(10f);
                invincible = true;
                ChangerCouleurRouge();
                Invoke("Invincibilite", 2);
            }

            // Donner du knockback au personnage dépendamment de la position du personnage vs l'ennemi, qui va se désactiver après 0.5 secondes
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

    // Fonction de collisions
    private void OnCollisionStay2D(Collision2D collision)
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
                ChangerCouleurRouge();
                Invoke("Invincibilite", 2);
                
            }

            // Donner du knockback au personnage dépendamment de la position du personnage vs l'ennemi, qui va se désactiver après 0.5 secondes
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "coeur")
        {
            BarreDeVie.GetComponent<BarreDeVieController>().augmenterMaxBarreDeVie(10f);
            BarreDeVie.GetComponent<BarreDeVieController>().soignerBarreDeVie(1000f);
            Destroy(collision.gameObject);
        }
    }

    // Fonction qui désactive le knockback
    void FinKnockback()
    {
        // Désactiver le knockback
        knockbackPerso = false;
    }

    void Invincibilite()
    {
        invincible = false;
    }

    void ChangerCouleurRouge()
    {
        gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>().color = couleurDegat;
        Invoke("ChangerCouleurOri", 0.35f);
    }

    void ChangerCouleurOri()
    {
        gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>().color = couleurOri;
        if (invincible)
        {
            Invoke("ChangerCouleurRouge", 0.35f);
        }
    }
}
