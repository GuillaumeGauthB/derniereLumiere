using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Joueur_Script : MonoBehaviour
{
    /* Script principal du personnage
      Par : Jonathan Mores, Jerome Trottier et Guillaume Gauthier-Benoit
      Derni?re modification : 18/05/2022
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
    [Header("Variables a Guillaume")]
    public static bool doubleSautObtenu, // Variables determinant quels pouvoirs sont obtenus
        dashObtenu = false,
        stunObtenu,
        tirObtenu = false;
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

    public GameObject doubleSautUIPouvoir; // le morceau de l'interface pour le double saut
    public GameObject dashUIPouvoir; // le morceau de l'interface pour le dash
    public GameObject SceneController;

    public bool modeSouris = true; // mode de jeu, clavier et souris ou manette

    static public bool mort; // etat de mort du personnage

    public AudioClip sonSaut; // son du saut et du double saut du personnage
    public AudioClip sonDash; // son du dash du personnage

    private bool b_estAuSolEnnemi; // variable permettant de savoir si le personnage est sur un ennemi

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
        i_inputJoueur.Player.EnclencherTir.canceled += GetComponent<Inputs_Guillaume>().DeclencherTir;

    }
    private void Update()
    {
        // Verifier si le personnage est sur le sol
        b_estAuSol = Physics2D.OverlapCircle(checkSol.position, 0.3f, solLayer);

        // Si le personnage est au sol, permettre un double saut
        if (b_estAuSol)
        {
            a_Joueur.SetBool("estAuSol", true);
            if (doubleSautUIPouvoir.GetComponent<PouvoirUI>().peutUtiliserPouvoir)
            {
                b_doubleSautPossible = true;
            }
            
        }

    }

    private void FixedUpdate()
    {
        // faire disparaitree le curseur
        Cursor.visible = false;
        
        // si le personnage n'est ni mort, ni en dash et ni en lecture de dialogues...
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

            f_cooldownDash = doubleSautUIPouvoir.GetComponent<PouvoirUI>().delayPouvoir;
            CancelInvoke("Cooldown");
        }
    }

    // La fonction gerant l'activation du saut
    void Saut(InputAction.CallbackContext context)
    {
        // Lorsque l'action est performee...
        if (context.performed && !GetComponent<dialogues>().texteActivee && GameObject.Find("PanelPause").GetComponent<Pause>().peutDeplacer)
        {
            // si le personnage est au sol et n'est pas accroupit...
            if ((b_estAuSol == true || b_estAuSolEnnemi == true) && !accroupir)
            {
                // Faire sauter le personnage
                rb_Joueur.AddForce(new Vector2(0, 1 * forceSaut));
                a_Joueur.SetTrigger("Saut");
                GetComponent<AudioSource>().PlayOneShot(sonSaut);

            }
            // Si le personnage n'est pas sur le sol et peut faire un double saut...
            else if (!b_estAuSol && b_doubleSautPossible && doubleSautObtenu && !b_estAuSolEnnemi)
            {
                //Utiliser le double saut dans le UI
                doubleSautUIPouvoir.GetComponent<PouvoirUI>().utiliserPouvoir();
                // Faire sauter le personnage et empecher de faire un autre saut
                rb_Joueur.AddForce(new Vector2(0, 1 * forceSaut));
                b_doubleSautPossible = false;
                doubleSautUIPouvoir.GetComponent<PouvoirUI>().peutUtiliserPouvoir = false;
                a_Joueur.SetTrigger("DoubleSaut");
                GetComponent<AudioSource>().PlayOneShot(sonSaut);
            }
        }
    }

    // La fonction gerant la course du personnage
    void Courrir(InputAction.CallbackContext context)
    {
        // Lorsque l'action est performee
        //Debug.Log(context.phase);
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
        //Debug.Log(context.phase);
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
        // Si le bouton est appuy?, qu'il peut dash, que le pouvoir est obtenu, et que son mouvement sur l'axe des x n'est pas 0...
        if (context.performed && b_dashPossible && dashObtenu && dashUIPouvoir.GetComponent<PouvoirUI>().peutUtiliserPouvoir && !GetComponent<Inputs_Guillaume>().declencherTir)
        {
            // Faire le dash
            dashUIPouvoir.GetComponent<PouvoirUI>().utiliserPouvoir();
            estDash = true;
            presentTimerDash = commencerTimerDash;
            rb_Joueur.velocity = Vector2.zero;
            f_directionDash = f_movX;
            dashUIPouvoir.GetComponent<PouvoirUI>().peutUtiliserPouvoir = false;

            // Faire jouer le son du dash
            GetComponent<AudioSource>().PlayOneShot(sonDash);

            a_Joueur.SetTrigger("Dash");
            //b_dashPossible = false;
            if (f_movX == 0)
            {
                if (sprite.GetComponent<SpriteRenderer>().flipX)
                {
                    f_directionDash = -1;
                }
                else
                {
                    f_directionDash = 1;
                }

            }

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


    // Fonction d?tectant les collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si le personnage entre en collision avec un checkpoint, sauvegarder sa position
        if (collision.tag == "checkpoint")
        {
            checkpoint = transform.position;
            collision.GetComponent<Animator>().SetTrigger("Save");
        }
        // Si le personnage entre en contact avec une zone de mort, on le place au checkpoint
        if (collision.tag == "tombe")
        {
            transform.position = checkpoint;

        }
        if (collision.gameObject.name == "Reine"){
            if (SceneController)
            {
                SceneController.GetComponent<SceneController>().changerScene("SceneFin");
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        // si le personnage est au sol et que atteri dans l'animator est false, le rendre true
        if (b_estAuSol == true && a_Joueur.GetBool("Atteri") == false)
        {
            a_Joueur.SetBool("Atteri", true);
        }

        // lorsque le personnage est sur l'ennemi, lui permettre de sauter
        if (collision.gameObject.tag == "ennemi")
        {
            b_estAuSolEnnemi = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // lorsque le personnage quitte le sol, mettre atterit a false
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Sol"))
        {
            a_Joueur.SetBool("Atteri", false);
        }

        // si le personnage quitte la tete d'une ennemi, lui empecher de sauter
        if (collision.gameObject.tag == "ennemi")
        {
            b_estAuSolEnnemi = false;
        }
    }

    // Prochaines fonctions sont la parce que le New Input System a eu une crise cardiaque pendant notre projet
    // pas tres pertinent

    // fonction permettant le changeant de manette a clavier et souris
    public void ChangerClavier(InputAction.CallbackContext context)
    {
        // aller en mode souris
        if (context.started)
        {
            modeSouris = true;
        }
    }

    // fonction permettant le changement de clavier et souris a manette
    public void ChangerManette(InputAction.CallbackContext context)
    {
        // aller en mode manette
        if (context.started)
        {
            modeSouris = false;
        }
    }
}
