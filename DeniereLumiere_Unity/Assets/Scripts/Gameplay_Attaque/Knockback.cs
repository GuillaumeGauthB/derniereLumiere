using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    /* Knockback de l'attaque physique
      Par : Guillaume Gauthier-Benoit
      Dernière modification : 14/03/2022
    */
     private float v_posXEvP, // La position du vecteur Ennemi vs Personnage en X
       v_posYEvP; // Meme chose mais en Y

    private GameObject g_parent; // Le parent du collider de l'attaque

    private void Start()
    {
        g_parent = gameObject.transform.parent.gameObject; // Mettre le gameObject du parent dans g_parent
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Si le collider entre en collision avec un objet avec le tag ennemi...
        if (other.tag == "ennemi")
        {
            //Vector2.Distance(gameObject.transform.position, other.gameObject.transform.position)
            v_posXEvP = Vector2.Distance(new Vector2(g_parent.transform.position.x, 0), new Vector2(other.gameObject.transform.position.x, 0));
            if(g_parent.transform.position.x  <= other.gameObject.transform.position.x)
            {
                other.GetComponent<Rigidbody2D>().AddForce(new Vector2(v_posXEvP * 10, 30f));
            }
            else
            {
                other.GetComponent<Rigidbody2D>().AddForce(new Vector2(-v_posXEvP * 10, 30f));
            }
            Debug.Log(v_posXEvP);
        }
    }
}
