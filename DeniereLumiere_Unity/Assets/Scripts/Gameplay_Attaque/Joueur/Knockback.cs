using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    /* Knockback de l'attaque physique
      Par : Guillaume Gauthier-Benoit
      Derni�re modification : 14/03/2022
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
            // D�terminer la position g�n�rale en x de l'ennemi
            v_posXEvP = other.transform.position.x - g_parent.transform.localPosition.x;

            // Si la valeur est plus petite que 0...
            if (v_posXEvP <= 0)
            {
                // Pousser l'ennemi vers l'arri�re
                other.GetComponent<Rigidbody2D>().velocity += new Vector2(-5, 0);
            }
            else
            {
                // Sinon, le pousser vers l'avant
                other.GetComponent<Rigidbody2D>().velocity += new Vector2(5, 0);
            }
            
            // D�terminer la position g�n�rale en y de l'ennemi
            v_posYEvP = other.transform.position.y - g_parent.transform.localPosition.y;

            // Si la valeur est plus petite que 0...
            if(v_posYEvP <= 0)
            {
                // Le pousser vers le bas
                other.GetComponent<Rigidbody2D>().velocity += new Vector2(0, -5);
            }
            else
            {
                // Sinon, le pousser vers le haut
                other.GetComponent<Rigidbody2D>().velocity += new Vector2(0, 5);
            }

        }
    }
}
