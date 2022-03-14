using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesCamera : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Contains("Projectile"))
        {
            Destroy(collision.gameObject);
        }
    }
}
