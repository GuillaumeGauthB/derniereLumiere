using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Joueur_Script : MonoBehaviour
{
    private Rigidbody2D rbJoueur;
    private PlayerInput piJoueur;
    private InputJoueur inputJoueur;
    public float jumpforce;
    public float speed;
    
    void Awake()
    {
        rbJoueur = GetComponent<Rigidbody2D>();
        piJoueur = GetComponent<PlayerInput>();


        inputJoueur = new InputJoueur();
        inputJoueur.Player.Enable();
        inputJoueur.Player.Jump.performed += Jump;
    }


    private void FixedUpdate()
    {
        Vector2 inputVector = inputJoueur.Player.Movement.ReadValue<Vector2>();
        rbJoueur.AddForce(new Vector2(inputVector.x, 0) * speed);
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rbJoueur.AddForce(new Vector2(0, 1) * jumpforce);
            Debug.Log("Jump was made " + context.phase);
        }
    }

   /*void Movement(InputAction.CallbackContext context)
    {
        
        
        
        
        Debug.Log("Jump was made " + inputVector);
    }*/
}
