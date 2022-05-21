using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*Script créé par Jonathan Mores et Guillaume Gauthier-Benoit*/
/*Dernière modification 2022-03-17*/
public class Pouvoirs : MonoBehaviour
{
    /** Script backup
     * Cree par Jonathan Mores et guillaume gauthier benoit
     * Derniere modification: 29/04/22
     */

    // Backup pour joueur_script, pas pertinent


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

    /* ================================================================================= Modifications Guillaume =====================================*/
    public static bool b_doubleSautObtenu = true,
        b_dashObtenu = true,
        b_stunObtenu = true,
        b_tirObtenu;
    private bool b_doubleSautPossible,
        b_dashPossible = true,
        b_stunPossible;

    public float forceDash,
        commencerTimerDash,
        presentTimerDash;

    public bool estDash;

    private float f_movX,
        f_directionDash,
        f_cooldownDash = 1;

    private Vector3 checkpoint;
    /* ================================================================================= Fin Modifs Guillaume    =====================================*/

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

        /* ================================================================================= Modifications Guillaume =====================================*/
        // Si le personnage est au sol, permettre un double saut
        if (estAuSol)
        {
            b_doubleSautPossible = true;
        }
        /* ================================================================================= Fin Modifs Guillaume    =====================================*/
    }

    private void FixedUpdate()
    {
        /* ================================================================================= Modifications Guillaume =====================================*/

        /* permet de lire le input du new input system*/
        if (!estDash)
        {
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

        // Sauvegarder la valeur du mouvement sur l'axe des x
        f_movX = Input.GetAxis("Horizontal");

        // Si le personnage est en train de dash...
        if (estDash)
        {
            // Le déplacer et faire diminuer le temps
            rbJoueur.velocity = transform.right * f_directionDash * forceDash;
            presentTimerDash -= Time.deltaTime;
            // Si le temps est égal à zéro...
            if (presentTimerDash <= 0)
            {
                // Arrêter le dash et commencer le cooldown
                estDash = false;
                InvokeRepeating("Cooldown", 0, 1f);
            }
        }

        // Si le cooldown vaut moins que 0, que le personnage ne peut dash et a le pouvoir...
        if (f_cooldownDash <= 0 && !b_dashPossible && b_dashObtenu)
        {
            // Rendre le dash possible, réinitialiser le temps de cooldown et arrêter de le faire descendre
            b_dashPossible = true;
            f_cooldownDash = 1;
            CancelInvoke("Cooldown");
        }

        //Debug.Log(f_cooldownDash);
        /* ================================================================================= Fin Modifs Guillaume    =====================================*/

    }

    void Saut(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            /* ================================================================================= Modifications Guillaume =====================================*/
            if (estAuSol == true || b_doubleSautObtenu && b_doubleSautPossible || !b_dashPossible)
            {
               rbJoueur.AddForce(new Vector2(0, 1 * forceSaut));
                if (b_doubleSautPossible && !estAuSol)
                {
                    b_doubleSautPossible = false;
                }
                /* ================================================================================= Fin Modifs Guillaume    =====================================*/
            }
            //Debug.Log("Jump was made " + context.phase);
        }
    }

    // Fonction gérant les dash
    public void Dash(InputAction.CallbackContext context)
    {
        Debug.Log(f_movX);
        // Si le bouton est appuyé, qu'il peut dash, que le pouvoir est obtenu, et que son mouvement sur l'axe des x n'est pas 0...
        if (context.started && b_dashPossible && b_dashObtenu && f_movX != 0)
        {
            //le faire dash
            estDash = true;
            presentTimerDash = commencerTimerDash;
            rbJoueur.velocity = Vector2.zero;
            f_directionDash = f_movX;
            b_dashPossible = false;
        }
    }

    // Fonction gérant les cooldowns des pouvoirs (va devoir être retravaillée)
    void Cooldown()
    {
        if(!b_dashPossible && f_cooldownDash >= 0)
        {
            f_cooldownDash -= 1;
        }
        /*if (!b_dashPossible)
        {
            b_dashPossible = true;
        }*/
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

    // Fonction détectant les collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si le personnage entre en collision avec un checkpoint, sauvegarder sa position
        if(collision.tag == "checkpoint")
        {
            checkpoint = transform.position;
        }
        // Si le personnage entre en contact avec une zone de mort, on le place au checkpoint
        if(collision.tag == "mort")
        {
            transform.position = checkpoint;
        }
    }
}
