using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesCamera : MonoBehaviour
{
    /* Destruction des projectiles lorsqu'ils sortent de la caméra
      Par : Guillaume Gauthier-Benoit
      Dernière modification : 14/03/2022
    */

    private Vector2 v_offsetOriginal,
        v_sizeOriginal;
    private void Start()
    {
        v_offsetOriginal = GetComponent<BoxCollider2D>().offset;
        v_sizeOriginal = GetComponent<BoxCollider2D>().size;
    }

    private void Update()
    {
        if (GetComponent<Camera>().orthographicSize == 10) GetComponent<BoxCollider2D>().size = new Vector2(37.4f, 21.57f);
        else GetComponent<BoxCollider2D>().size = v_sizeOriginal;
    }
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
