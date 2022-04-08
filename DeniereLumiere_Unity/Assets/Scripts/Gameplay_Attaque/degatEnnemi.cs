using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class degatEnnemi : MonoBehaviour
{
    public float ennemiVie;
    public bool ennemiMort;

    private void Start()
    {
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
        if (ennemiVie <= 0)
        {
            ennemiMort = true;
        }

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
        if (collision.gameObject.tag.Contains("projectilePerso"))
        {
            ennemiVie -= 1;
            //gameObject
            Debug.Log(collision.gameObject.tag);
            Debug.Log(ennemiVie);
            Destroy(collision.gameObject);
        }

    }
}