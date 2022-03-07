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

        InputJoueur inputJoueur = new InputJoueur();
        inputJoueur.Player.Enable();
        inputJoueur.Player.Jump.performed += Jump;
        //inputJoueur.Player.Movement.performed += Movement();
    }

    
    
    
    void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Jump was made " + context.phase);
        }
    }

    void Movement(InputAction.CallbackContext context)
    {
        
        Vector2 inputVector = context.ReadValue<Vector2>();
        rbJoueur.AddForce(new Vector2(inputVector.x, inputVector.y) * speed);
        
        Debug.Log("Jump was made " + inputVector);
    }
}
