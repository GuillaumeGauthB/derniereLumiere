using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Joueur_Script : MonoBehaviour
{
    private Rigidbody2D rbJoueur;
    private PlayerInput piJoueur;
    public float speed;
    
    void Awake()
    {
        rbJoueur = GetComponent<Rigidbody2D>();
        piJoueur = GetComponent<PlayerInput>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Jump was made " + context.phase);
        }
    }

    public void Mouvement(InputAction.CallbackContext context)
    {
        
        Vector2 inputVector = context.ReadValue<Vector2>();
        rbJoueur.AddForce(new Vector2(inputVector.x, inputVector.y) * speed);
        Debug.Log("Jump was made " + inputVector);
    }
}
