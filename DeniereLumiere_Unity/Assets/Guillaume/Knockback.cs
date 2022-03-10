using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    private Vector3 direction;
    private float v_posXEvP,
       v_posYEvP;

    private GameObject g_parent;

    private void Start()
    {
        g_parent = gameObject.transform.parent.gameObject;
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.tag == "ennemi")
        {

            v_posXEvP = other.transform.position.x - g_parent.transform.localPosition.x;

            Debug.Log(v_posXEvP);
            if (v_posXEvP <= 0)
            {
                Debug.Log("i hate it here x");
                other.GetComponent<Rigidbody2D>().velocity += new Vector2(-5, 0);
            }
            else
            {
                Debug.Log("i hate it here y");
                other.GetComponent<Rigidbody2D>().velocity += new Vector2(5, 0);
            }

            v_posYEvP = other.transform.position.y - g_parent.transform.localPosition.y;

            if(v_posYEvP <= 0)
            {
                other.GetComponent<Rigidbody2D>().velocity += new Vector2(0, -5);
            }
            else
            {
                other.GetComponent<Rigidbody2D>().velocity += new Vector2(0, 5);
            }
           // position ennemi - position personnage

        }
    }
}
