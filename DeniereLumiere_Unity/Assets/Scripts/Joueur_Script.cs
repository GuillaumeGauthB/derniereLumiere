using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*Script créé par Jonathan Mores*/
/*Dernière modification 2022-03-09*/
public class Joueur_Script : MonoBehaviour
{
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
    }

    private void FixedUpdate()
    {
        /* permet de lire le input du new input system*/
        Vector2 inputVector = i_inputJoueur.Player.Mouvement.ReadValue<Vector2>();
        rb_Joueur.AddRelativeForce(new Vector2(inputVector.x * vitesseAcceleration, 0f), ForceMode2D.Impulse);
        /* permet au personnage de ne pas dépasser sa vitesse maximale*/
        if (rb_Joueur.velocity.x > f_vitesseMaximale || rb_Joueur.velocity.x < -f_vitesseMaximale)
        {
            rb_Joueur.velocity = new Vector2(inputVector.x * f_vitesseMaximale, rb_Joueur.velocity.y);
        }
        /*ligne pour voir si le personnage ce déplace*/
        if (rb_Joueur.velocity.magnitude > 0)
        {
            /*Debug.Log(rb_Joueur.velocity);*/
        }

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

    public void voirSiJoueurSurPlateforme(Collision2D collision, bool value)
    {
        var player = collision.gameObject.GetComponent<Joueur_Script>();
        if (player != null)
        {
            b_JoueurSurPlateforme = value;
        }
    }
}
