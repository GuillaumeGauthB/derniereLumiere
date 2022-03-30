using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesCamera : MonoBehaviour
{
    /* Destruction des projectiles lorsqu'ils sortent de la cam�ra
      Par : Guillaume Gauthier-Benoit
      Derni�re modification : 14/03/2022
    */
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
