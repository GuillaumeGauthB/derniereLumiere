using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class degatPerso : MonoBehaviour
{
    /** Script de dégât du joueur
     * Créé par Guillaume Gauthier-Benoît
     * Dernière modification: 20/05/22
     */

    public GameObject BarreDeVie; // barre de vie du personnage
    private float f_posXEvP; //position ennemi vs personnage en x
    public bool knockbackPerso, // valeurs boolean des effets
        invincible;
    public float viePerso; // vie initiale du personnage
    public AudioClip sonDegats; // son de degat
    public Color couleurDegat, //couleurs d'invincibilite
        couleurOri;
    public AudioClip sonMort; // le son de mort du personnage
    private bool faireUneFois; // variable boolean pour
    static public float vieTotale = 10; // la vie totale du personnage

    private void Start()
    {
        // initialiser la couleur initiale du personnage
        couleurOri = gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        // Lorsque la vie du personnage atteint 0 ou moins, le tuer
        if (viePerso <= 0 && !faireUneFois)
        {
            GetComponent<AudioSource>().PlayOneShot(sonMort);
            Joueur_Script.mort = true;
            faireUneFois = true;

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
                // lui enlever de la vie, jouer le son, et le faire changer de couleur
                viePerso--;
                GetComponent<AudioSource>().PlayOneShot(sonDegats);
                BarreDeVie.GetComponent<BarreDeVieController>().infligerDegatsBarreDeVie(10f);
                invincible = true;
                ChangerCouleurRouge();
                Invoke("Invincibilite", 2);
            }

            // Donner du knockback au personnage dépendamment de la position du personnage vs l'ennemi, qui va se désactiver après 0.5 secondes
            // ne fonctionne pas je crois
            knockbackPerso = true;
            Invoke("FinKnockback", 0.5f);
            // le faire reculer dans la direction oppose de l'ennemi
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
        // si le personnage entre en collision avec un object contenant le tag mort, le tuer
        if (collision.gameObject.tag == "mort")
        {
            Joueur_Script.mort = true;
        }

        // Lorsque le personnage entre en collision avec un objet avec le tag boss ou ennemi...
        if (collision.gameObject.tag == "ennemi" || collision.gameObject.tag == "boss")
        {
            // Le rendre temporairement invincible
            if (!invincible)
            {
                // faire comme avant
                //viePerso--;
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
        // si le personnage entre en contact avec un coueur, le heal et augmenter sa vie totale
        if (collision.gameObject.tag == "coeur")
        {
            vieTotale += 3;
            viePerso = vieTotale;
            BarreDeVie.GetComponent<BarreDeVieController>().augmenterMaxBarreDeVie(30f);
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

    // fonction enlevant l'invincibilite
    void Invincibilite()
    {
        invincible = false;
    }

    // Fonctions gerant le changement de couleur de rouge  a normal
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