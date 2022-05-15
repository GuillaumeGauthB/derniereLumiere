using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorteProchainNiv : MonoBehaviour
{
    public GameObject boss;
    public GameObject SceneController;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && boss == null)
        {
            SceneController.GetComponent<SceneController>().changerScene("Niveau2");
        }
    }
}
