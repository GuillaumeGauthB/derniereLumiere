using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limiteurCameraController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "limiteurCamera")
        {
            
            collision.GetComponent<limiteurCamera>().setNouvellesLimitesGlobales();
        }
    }
}
