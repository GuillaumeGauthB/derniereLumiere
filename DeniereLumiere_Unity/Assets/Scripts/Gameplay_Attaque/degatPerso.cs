using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class degatPerso : MonoBehaviour
{
    private float f_posXEvP;
    public bool knockbackPerso,
        invincible,
        mort;
    public float viePerso;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(viePerso <= 0)
        {
            mort = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ennemi" || collision.gameObject.tag == "boss")
        {
            f_posXEvP = Vector2.Distance(new Vector2(gameObject.transform.position.x, 0), new Vector2(collision.gameObject.transform.position.x, 0));
            knockbackPerso = true;
            Invoke("FinKnockback", 0.5f);
            if (!invincible)
            {
                viePerso--;
                invincible = true;
                Invoke("Invincibilite", 2);
            }

            if (collision.gameObject.transform.position.x >= gameObject.transform.position.x)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(f_posXEvP * 100, 30f));
                //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1500, 20));
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-f_posXEvP * 100, 30f));
                //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-10, 20));
            }
        }
    }

    void FinKnockback()
    {
        knockbackPerso = false;
    }

    void Invincibilite()
    {
        invincible = false;
    }
}
