using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuciolesProjectiles : MonoBehaviour
{
    public Vector2 destination;
    public float vitesse;

    // Start is called before the first frame update
    void Start()
    {
        //destination = transform.parent.gameObject.GetComponent<Inputs_Guillaume>().
        
    }
    private void Awake()
    {
        //destination = Camera.main.ScreenToWorldPoint(GameObject.Find("Personnage").GetComponent<Inputs_Guillaume>().sourisPosition);
        destination = Camera.main.ScreenToWorldPoint(GameObject.Find("Personnage").GetComponent<Inputs_Guillaume>().sourisPosition);
        destination -= new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(destination);
        gameObject.GetComponent<Rigidbody2D>().velocity = destination;
    }
}
