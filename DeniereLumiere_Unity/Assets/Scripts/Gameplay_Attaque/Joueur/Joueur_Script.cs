using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*Script cr?? par Jonathan Mores*/
/*Derni?re modification 2022-03-09*/
public class Joueur_Script : MonoBehaviour
{
    /* Script principal du personnage
      Par : Jonathan Mores et Guillaume Gauthier-Benoit
      Derni?re modification : 03/04/2022
    */

    /***********GameObject***********/
    public GameObject sprite; // Le sprite du personnage

    /***********Bool***********/
    private bool b_estAuSol; // Variable boolean pour determiner si le personnage est au sol
    private bool course; // Variable pour determiner si le personnage est en mode de course
    public bool accroupir; // Variable pour determiner si le personnage est en mode accoupit
    private bool b_JoueurSurPlateforme; // Variable pour determiner si le personnage est sur une plateforme

    /********Raccourcis********/

    private Rigidbody2D rb_Joueur; // le rigidbody du joueur
    private InputJoueur i_inputJoueur; // Le player input du joueur
    private Animator a_Joueur; // L'Animator du Sprite du joueur


    /********Transform********/
    public Transform checkSol; // La position pour determinee si le joueur est au sol

    /********Layer Mask********/
    public LayerMask solLayer; // La layer du sol111

    /********Float********/
    public float forceSaut; // La force du saut
    public float vitesseAcceleration; // La vitesse d'acceleration du deplacement 
    public float vitesseMaximaleMarche; // La vitesse maximale de la marche
    public float vitesseMaximaleCourrir; // La vitesse maximale de la course
    private float f_vitesseMaximale; // La vitesse maximale de la marche ou de la cours

    /********Variables a Guillaume********/
    public static bool doubleSautObtenu, // Variables determinant quels pouvoirs sont obtenus
        dashObtenu,
        stunObtenu,
        tirObtenu;
    private bool b_doubleSautPossible, // Variables determinant si les differents pouvoirs peuvent etre utilises
        b_dashPossible = true,
        b_stunPossible;

    public float forceDash, // Variables publics pertinentes pour l'utilisation du dash
        commencerTimerDash,
        presentTimerDash;

    public bool estDash; // Variable determinant si le personnage est en cours de dash ou pas

    private float f_movX, // Variables privees pertinentes pour l'utilisation du dash
        f_directionDash,
        f_cooldownDash = 1;

    private Vector3 checkpoint; // La position du checkpoint

    public GameObject doubleSautUIPouvoir;
    public GameObject dashUIPouvoir;

    public bool modeSouris;

    static public bool mort;

    void Awake()
    {
        // Assignation des variables et des c# events
        a_Joueur = sprite.GetComponent<Animator>();
        rb_Joueur = GetComponent<Rigidbody2D>();
        f_vitesseMaximale = vitesseMaximaleMarche;
        i_inputJoueur = new InputJoueur();
        i_inputJoueur.Player.Enable();
        i_inputJoueur.Player.Saut.performed += Saut;
        i_inputJoueur.Player.Courrir.performed += Courrir;
        i_inputJoueur.Player.Accroupir.performed += Accroupir;
        i_inputJoueur.Player.Dash.performed += Dash;

    }
    private void Update()
    {

        Cursor.visible = false;

        // Verifier si le personnage est sur le sol
        b_estAuSol = Physics2D.OverlapCircle(checkSol.position, 0.3f, solLayer);

        /* ================================================================================= Modifications Guillaume =====================================*/

        // Si le personnage est au sol, permettre un double saut
        if (b_estAuSol && doubleSautUIPouvoir.GetComponent<PouvoirUI>().peutUtiliserPouvoir)
        {
            a_Joueur.SetBool("estAuSol", true);
            b_doubleSautPossible = true;
        }

    }

    private void FixedUpdate()
    {
        //Debug.Log(f_movX);
        if (!estDash && !GetComponent<dialogues>().texteActivee && !mort)
        {
            /* permet de lire le input du new input system*/
            Vector2 inputVector = i_inputJoueur.Player.Mouvement.ReadValue<Vector2>();
            rb_Joueur.AddRelativeForce(new Vector2(inputVector.x * vitesseAcceleration, 0f), ForceMode2D.Impulse);
            /* permet au personnage de ne pas d?passer sa vitesse maximale*/
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
            }
            else if (rb_Joueur.velocity.x > 0)
            {
                sprite.GetComponent<SpriteRenderer>().flipX = false;
            }
            /*ligne pour voir si le personnage ce d?place*/
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
            // Le d?placer et faire diminuer le temps
            rb_Joueur.velocity = transform.right * f_directionDash * forceDash;
            presentTimerDash -= Time.deltaTime;
            // Si le temps est ?gal ? z?ro...
            if (presentTimerDash <= 0)
            {
                // Arr?ter le dash et commencer le cooldown
                estDash = false;
                InvokeRepeating("Cooldown", 0, 1f);
            }
        }
        // Si le cooldown vaut moins que 0, que le personnage ne peut dash et a le pouvoir...
        if (f_cooldownDash <= 0 && !b_dashPossible && dashObtenu && dashUIPouvoir.GetComponent<PouvoirUI>().peutUtiliserPouvoir)
        {
            // Rendre le dash possible, r?initialiser le temps de cooldown et arr?ter de le faire descendre
            b_dashPossible = true;
            dashUIPouvoir.GetComponent<PouvoirUI>().peutUtiliserPouvoir = true;

            f_cooldownDash = 1;
            CancelInvoke("Cooldown");
        }
    }

    // La fonction gerant l'activation du saut
    void Saut(InputAction.CallbackContext context)
    {
        // Lorsque l'action est performee...
        if (context.performed)
        {
            // si le personnage est au sol et n'est pas accroupit...
            if (b_estAuSol == true && !accroupir && !GetComponent<Inputs_Guillaume>().declencherTir && !GetComponent<dialogues>().texteActivee)
            {
                // Faire sauter le personnage
                rb_Joueur.AddForce(new Vector2(0, 1 * forceSaut));
                a_Joueur.SetTrigger("Saut");
                a_Joueur.SetBool("Atteri", false);
            }
            // Si le personnage n'est pas sur le sol et peut faire un double saut...
            else if (!b_estAuSol && b_doubleSautPossible && doubleSautObtenu && !GetComponent<dialogues>().texteActivee)
            {
                //Utiliser le double saut dans le UI
                doubleSautUIPouvoir.GetComponent<PouvoirUI>().utiliserPouvoir();
                // Faire sauter le personnage et empecher de faire un autre saut
                rb_Joueur.AddForce(new Vector2(0, 1 * forceSaut));
                b_doubleSautPossible = false;
                doubleSautUIPouvoir.GetComponent<PouvoirUI>().peutUtiliserPouvoir = false;
                a_Joueur.SetTrigger("Saut");
                a_Joueur.SetBool("Atteri", false);
            }
        }
    }

    // La fonction gerant la course du personnage

    void Courrir(InputAction.CallbackContext context)
    {
        // Lorsque l'action est performee
        Debug.Log(context.phase);
        if (context.performed)
        {
            // Si le personnage n'est pas en mode de course
            if (course == false)
            {
                // Changer la vitesse maximale et activer la variable de course
                f_vitesseMaximale = vitesseMaximaleCourrir;
                course = true;
            }
            // Sinon
            else
            {
                // Changer la vitesse maximale et desactiver la course
                f_vitesseMaximale = vitesseMaximaleMarche;
                course = false;
            }
        }
    }

    // Fonction gerant l'accroupissement du personnage
    void Accroupir(InputAction.CallbackContext context)
    {

        // Si l'action est performee...
        Debug.Log(context.phase);
        if (context.performed && !GetComponent<Inputs_Guillaume>().declencherTir)
        {
            // Activer ou desactiver l'accroupissement
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

    // Fonction g?rant les dash
    public void Dash(InputAction.CallbackContext context)
    {
        //Debug.Log(f_movX);
        // Si le bouton est appuy?, qu'il peut dash, que le pouvoir est obtenu, et que son mouvement sur l'axe des x n'est pas 0...
        if (context.performed && b_dashPossible && dashObtenu && f_movX != 0)
        {
            //faire le dash
            dashUIPouvoir.GetComponent<PouvoirUI>().utiliserPouvoir();
            estDash = true;
            presentTimerDash = commencerTimerDash;
            rb_Joueur.velocity = Vector2.zero;
            f_directionDash = f_movX;
            dashUIPouvoir.GetComponent<PouvoirUI>().peutUtiliserPouvoir = false;
            b_dashPossible = false;
        }
    }

    // Fonction g?rant les cooldowns des pouvoirs (va devoir ?tre retravaill?e)
    void Cooldown()
    {
        // Si le cooldown du dash est plus grand ou egal a 0 et que le dash n'est pas possible 
        if (!b_dashPossible && f_cooldownDash >= 0)
        {
            // Diminuer la valeur du cooldown de dash
            f_cooldownDash -= 1;
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

    // Fonction d?tectant les collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si le personnage entre en collision avec un checkpoint, sauvegarder sa position
        if (collision.tag == "checkpoint")
        {
            checkpoint = transform.position;
        }
        // Si le personnage entre en contact avec une zone de mort, on le place au checkpoint
        if (collision.tag == "tombe")
        {
            transform.position = checkpoint;

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (b_estAuSol == true)
        {
            a_Joueur.SetBool("Atteri", true);
        }
    }

    public void ChangerClavier(InputAction.CallbackContext context)
    {
        // Si le bouton est appuy?, qu'il peut dash, que le pouvoir est obtenu, et que son mouvement sur l'axe des x n'est pas 0...
        if (context.started)
        {
            modeSouris = true;
        }
    }

    public void ChangerManette(InputAction.CallbackContext context)
    {
        // Si le bouton est appuy?, qu'il peut dash, que le pouvoir est obtenu, et que son mouvement sur l'axe des x n'est pas 0...
        if (context.started)
        {
            modeSouris = false;
        }
    }
}
