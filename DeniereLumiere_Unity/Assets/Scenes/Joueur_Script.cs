using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*Script créé par Jonathan Mores*/
/*Dernière modification 2022-03-09*/
public class Joueur_Script : MonoBehaviour
{
    private Rigidbody2D rbJoueur;
    private PlayerInput piJoueur;
    private InputJoueur inputJoueur;
    public Transform checkSol;
    public LayerMask solLayer;
    private bool estAuSol;
    public float forceSaut;
    public float vitesse;
    private float vitesseMaximale;
    public float vitesseMaximaleMarche;
    public float vitesseMaximaleCourrir;
    
    void Awake()
    {
        rbJoueur = GetComponent<Rigidbody2D>();
        piJoueur = GetComponent<PlayerInput>();

        vitesseMaximale = vitesseMaximaleMarche;
        inputJoueur = new InputJoueur();
        inputJoueur.Player.Enable();
        inputJoueur.Player.Saut.performed += Saut;
        /*inputJoueur.Player.Courrir.performed += Courrir;*/

    }
    private void Update()
    {
        estAuSol = Physics2D.OverlapCircle(checkSol.position, 0.5f, solLayer);
    }

    private void FixedUpdate()
    {
        /* permet de lire le input du new input system*/
        Vector2 inputVector = inputJoueur.Player.Mouvement.ReadValue<Vector2>();
        rbJoueur.AddRelativeForce(new Vector2(inputVector.x * vitesse, 0f), ForceMode2D.Impulse);
        /* permet au personnage de ne pas dépasser sa vitesse maximale*/
        if (rbJoueur.velocity.x > vitesseMaximale || rbJoueur.velocity.x < -vitesseMaximale)
        {
            rbJoueur.velocity = new Vector2(inputVector.x * vitesseMaximale, rbJoueur.velocity.y);
        }
        /*ligne pour voir si le personnage ce déplace*/
        if (rbJoueur.velocity.magnitude > 0)
        {
            /*Debug.Log(rbJoueur.velocity);*/
        }
        
    }

    void Saut(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(estAuSol == true)
            {
               rbJoueur.AddForce(new Vector2(0, 1 * forceSaut));
            }   
            Debug.Log("Jump was made " + context.phase);
        }
    }

    /*void Courrir(InputAction.CallbackContext context)
    {
        Debug.Log(context.phase);
        if (context.performed)
        {
            vitesseMaximale = vitesseMaximaleCourrir;
            Debug.Log("course:True");
        } else if (context.canceled)
        {
            Debug.Log("course:False");
            vitesseMaximale = vitesseMaximaleMarche;
        }
    }*/
}
