using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*Script créé par Jonathan Mores*/
/*Dernière modification 2022-03-09*/
public class Joueur_Script : MonoBehaviour
{
    /***********GameObject***********/
    public GameObject sprite;

   

    /***********Bool***********/
    private bool b_estAuSol;
    private bool course;
    public bool accroupir;
    private bool b_JoueurSurPlateforme;

    /********Raccourcis********/
    private Rigidbody2D rb_Joueur;
    private InputJoueur i_inputJoueur;

    /********Transform********/
    public Transform checkSol;

    /********Layer Mask********/
    public LayerMask solLayer;

    /********Float********/
    public float forceSaut;
    public float vitesseAcceleration;
    public float vitesseMaximaleMarche;
    public float vitesseMaximaleCourrir;
    private float f_vitesseMaximale;

    /********Variabe a Guillaume********/
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
   

    void Awake()
    {
        rb_Joueur = GetComponent<Rigidbody2D>();
        f_vitesseMaximale = vitesseMaximaleMarche;
        i_inputJoueur = new InputJoueur();
        i_inputJoueur.Player.Enable();
        i_inputJoueur.Player.Saut.performed += Saut;
        i_inputJoueur.Player.Courrir.performed += Courrir;
        i_inputJoueur.Player.Accroupir.performed += Accroupir;
    }
    private void Update()
    {
        b_estAuSol = Physics2D.OverlapCircle(checkSol.position, 0.5f, solLayer);
        /* ================================================================================= Modifications Guillaume =====================================*/
        // Si le personnage est au sol, permettre un double saut
        if (b_estAuSol)
        {
            b_doubleSautPossible = true;
        }
        /* ================================================================================= Fin Modifs Guillaume    =====================================*/
    }

    private void FixedUpdate()
        {
        if (!estDash) { 
            /* permet de lire le input du new input system*/
            Vector2 inputVector = i_inputJoueur.Player.Mouvement.ReadValue<Vector2>();
            rb_Joueur.AddRelativeForce(new Vector2(inputVector.x * vitesseAcceleration, 0f), ForceMode2D.Impulse);
            /* permet au personnage de ne pas dépasser sa vitesse maximale*/
            if (rb_Joueur.velocity.x > f_vitesseMaximale || rb_Joueur.velocity.x < -f_vitesseMaximale)
            {
                rb_Joueur.velocity = new Vector2(inputVector.x * f_vitesseMaximale, rb_Joueur.velocity.y);
                sprite.GetComponent<Animator>().SetBool("Course", true);
            }
            else
            {
                sprite.GetComponent<Animator>().SetBool("Course", false);
            }
            if (rb_Joueur.velocity.x < 0)
            {
                sprite.GetComponent<SpriteRenderer>().flipX = true;
            } else if(rb_Joueur.velocity.x > 0)
            {
                sprite.GetComponent<SpriteRenderer>().flipX = false;
            }
            /*ligne pour voir si le personnage ce déplace*/
            if (rb_Joueur.velocity.magnitude > 0)
            {
                /*Debug.Log(rb_Joueur.velocity);*/
            }
        }
        // Sauvegarder la valeur du mouvement sur l'axe des x
        f_movX = Input.GetAxis("Horizontal");

        // Si le personnage est en train de dash...
        if (estDash)
        {
            // Le déplacer et faire diminuer le temps
            rb_Joueur.velocity = transform.right * f_directionDash * forceDash;
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
            if (b_estAuSol == true && !accroupir)
            {
                rb_Joueur.AddForce(new Vector2(0, 1 * forceSaut));
            }
            Debug.Log("Jump was made " + context.phase);
        }
    }
   
    void Courrir(InputAction.CallbackContext context)
    {
        Debug.Log(context.phase);
        if (context.performed)
        {
            if (course == false)
            {
                f_vitesseMaximale = vitesseMaximaleCourrir;
                course = true;
            }
            else
            {
                f_vitesseMaximale = vitesseMaximaleMarche;
                course = false;
            }
        }
    }

    void Accroupir(InputAction.CallbackContext context)
    {
        Debug.Log(context.phase);
        if (context.performed)
        {
            if (accroupir == false)
            {
                accroupir = true;
            }
            else
            {
                accroupir = false;
            }
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
            rb_Joueur.velocity = Vector2.zero;
            f_directionDash = f_movX;
            b_dashPossible = false;
        }
    }

    // Fonction gérant les cooldowns des pouvoirs (va devoir être retravaillée)
    void Cooldown()
    {
        if (!b_dashPossible && f_cooldownDash >= 0)
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
        if (collision.tag == "checkpoint")
        {
            checkpoint = transform.position;
        }
        // Si le personnage entre en contact avec une zone de mort, on le place au checkpoint
        if (collision.tag == "mort")
        {
            transform.position = checkpoint;
        }
    }
    public void voirSiJoueurSurPlateforme(Collision2D collision, bool value)
    {
        var player = collision.gameObject.GetComponent<Joueur_Script>();
        if (player != null)
        {
            b_JoueurSurPlateforme = value;
        }
    }
}
