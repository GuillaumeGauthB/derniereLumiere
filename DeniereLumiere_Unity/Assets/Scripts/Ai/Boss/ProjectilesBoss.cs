using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesBoss : MonoBehaviour
{
    public float vitesse;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().AddForce(GetComponent<Rigidbody2D>().transform.right * vitesse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Perso" || collision.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }
        
    }
}
