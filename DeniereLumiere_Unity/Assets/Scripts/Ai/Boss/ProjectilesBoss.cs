using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesBoss : MonoBehaviour
{
    /** Script de projectiles du boss
     * Cree par Jonathan Mores
     * Derniere modification: 29/04/22
     */
    public float vitesse; // la vitesse du projectile
    
    // Start is called before the first frame update
    void Start()
    {
        // Detruire le gameobject apres 5 secondes
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        // le deplacer vers la droite
        GetComponent<Rigidbody2D>().AddForce(GetComponent<Rigidbody2D>().transform.right * vitesse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // detruire le projectile quand il entre en contact avec le personnage ou le sol
        if (collision.gameObject.tag == "Perso" || collision.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }
        
    }
}
