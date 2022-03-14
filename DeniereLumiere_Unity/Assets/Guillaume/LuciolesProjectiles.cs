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
        if (GameObject.Find("Personnage").gameObject.transform.GetComponent<Inputs_Guillaume>().playerInput.currentControlScheme == "Clavier")
        {
            destination = GameObject.Find("Personnage").gameObject.transform.GetComponent<Inputs_Guillaume>().v_deplacementCible * 10f;
        }
        else
        {
            destination = new Vector2(GameObject.Find("Personnage").gameObject.transform.GetComponent<Inputs_Guillaume>().v_deplacementCible.x - gameObject.transform.position.x, GameObject.Find("Personnage").gameObject.transform.GetComponent<Inputs_Guillaume>().v_deplacementCible.y - gameObject.transform.position.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        gameObject.GetComponent<Rigidbody2D>().velocity = destination;
    }
}
