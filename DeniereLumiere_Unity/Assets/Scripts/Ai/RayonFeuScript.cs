using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayonFeuScript : MonoBehaviour
{
    public GameObject cible;
    void Start()
    {
        transform.position = cible.transform.position;
        Destroy(gameObject, 4f);
    }
}
