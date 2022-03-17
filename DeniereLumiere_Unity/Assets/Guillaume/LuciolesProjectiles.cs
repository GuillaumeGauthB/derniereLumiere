using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuciolesProjectiles : MonoBehaviour
{
    /* Projectiles de luciole
      Par : Guillaume Gauthier-Benoit
      Dernière modification : 14/03/2022
    */
    private Vector2 v_destination; // Le déplacement du projectile
    public float vitesse; // La vitesse du projectile

    private void Awake()
    {
        // Si le joueur joue avec un clavier...
        if (GameObject.Find("Personnage").gameObject.transform.GetComponent<Inputs_Guillaume>().playerInput.currentControlScheme == "Clavier")
        {
            // Multiplier la direction par la vitesse
            v_destination = GameObject.Find("Personnage").gameObject.transform.GetComponent<Inputs_Guillaume>().v_deplacementCible * vitesse;
        }
        else
        {
            // Sinon, le déplacer avec la direction
            v_destination = new Vector2(GameObject.Find("Personnage").gameObject.transform.GetComponent<Inputs_Guillaume>().v_deplacementCible.x - gameObject.transform.position.x, GameObject.Find("Personnage").gameObject.transform.GetComponent<Inputs_Guillaume>().v_deplacementCible.y - gameObject.transform.position.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        gameObject.GetComponent<Rigidbody2D>().velocity = v_destination;
    }
}
