using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LuciolesProjectiles : MonoBehaviour
{
    /* Projectiles de luciole
      Par : Guillaume Gauthier-Benoit
      Dernière modification : 19/05/2022
    */
    private Vector2 v_destination; // Le déplacement du projectile
    public float vitesse; // La vitesse du projectile

    private void Start()
    {
        // si le personnage est sur le niveau2, gerer la vitesse d'une maniere, dependamment du souris ou manette
        if (SceneManager.GetActiveScene().name == "Niveau2")
        {
            if (GameObject.Find("Beepo").gameObject.GetComponent<Joueur_Script>().modeSouris)
            {
                // Multiplier la direction par la vitesse
                v_destination = GameObject.Find("Beepo").gameObject.transform.GetComponent<Inputs_Guillaume>().v_deplacementCible * (vitesse * 10);
            }
            else
            {
                v_destination = GameObject.Find("Beepo").gameObject.transform.GetComponent<Inputs_Guillaume>().v_deplacementCible * vitesse;
            }
        }
        // mm chose si il est dans niveau1
        else
        {
            if (GameObject.Find("Beepo").gameObject.GetComponent<Joueur_Script>().modeSouris)
            {
                // Multiplier la direction par la vitesse
                v_destination = GameObject.Find("Beepo").gameObject.transform.GetComponent<Inputs_Guillaume>().v_deplacementCible * vitesse;
            }
            else
            {
                v_destination = GameObject.Find("Beepo").gameObject.transform.GetComponent<Inputs_Guillaume>().v_deplacementCible * (vitesse / 10);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Deplacer le projectile dans la direction du tir
        gameObject.GetComponent<Rigidbody2D>().velocity = v_destination;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Lorsque le projectile entre en contact avec la layer du sol, detruire le projectile
        if (collision.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }
    }
}
