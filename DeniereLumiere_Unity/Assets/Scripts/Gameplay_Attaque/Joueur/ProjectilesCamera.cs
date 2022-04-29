using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesCamera : MonoBehaviour
{
    /* Destruction des projectiles lorsqu'ils sortent de la caméra
      Par : Guillaume Gauthier-Benoit
      Dernière modification : 14/03/2022
    */
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Lorsqu'un gameObject avec le Projectile entre en collision avec l'objet...
        if (collision.name.Contains("Projectile"))
        {
            // Détruire le projectile
            Destroy(collision.gameObject);
        }
    }
}
