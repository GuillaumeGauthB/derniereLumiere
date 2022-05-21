using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesCamera : MonoBehaviour
{
    /* Destruction des projectiles lorsqu'ils sortent de la cam�ra
      Par : Guillaume Gauthier-Benoit
      Derni�re modification : 20/05/2022
    */

    private Vector2 v_sizeOriginal; // la taille originale du collider
    private void Start()
    {
        v_sizeOriginal = GetComponent<BoxCollider2D>().size; // initialiser sa tailler
    }

    private void Update()
    {
        // si la camera recule (boss fight niveau2), changer la taille du collider, sinon le remettre a ce qu'il etait avant
        if (GetComponent<Camera>().orthographicSize == 10) GetComponent<BoxCollider2D>().size = new Vector2(37.4f, 21.57f);
        else GetComponent<BoxCollider2D>().size = v_sizeOriginal;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Lorsqu'un gameObject avec le Projectile entre en collision avec l'objet...
        if (collision.name.Contains("Projectile"))
        {
            // D�truire le projectile
            Destroy(collision.gameObject);
        }
    }
}
