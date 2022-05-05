using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuciolesBoss : MonoBehaviour
{
    private float vitesse = 3;
    private GameObject beepo;
    // Start is called before the first frame update
    void Start()
    {
        beepo = GameObject.Find("Beepo");
        Invoke("Destruction", 8f);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, beepo.transform.position, vitesse * Time.deltaTime);
    }

    void Destruction()
    {
        Destroy(gameObject);
    }
}
