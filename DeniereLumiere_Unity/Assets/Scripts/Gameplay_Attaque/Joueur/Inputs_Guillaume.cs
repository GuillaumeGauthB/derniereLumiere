using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs_Guillaume : MonoBehaviour
{
    /* Attaques physique et de tir du personnage
      Par : Guillaume Gauthier-Benoit
      Derni?re modification : 03/04/2022
    */

    public int
        degatTir, // Les points de degats faits par le tir de luciole
        knockbackCoup, // le knockback du coup fait par l'attaque corps a corps
        vitesseKnockback; // la vitesse a laquelle les ennemis sont poussers

    private LineRenderer c_lineRenderer; // Component du Line Renderer pour viser
    public PlayerInput playerInput; // le playerinput
    private Vector2 v_sourisPosition; // Position de la souris pour viser

    public GameObject projectile; // Le projectile de base du personnage
    private GameObject g_clone; // Le clone du projectile, va etre tirer sur les ennemis
    public GameObject flecheViser;

    private bool b_knockback; // Variable permettant le cooldown de l'attaque de knockback
    public bool declencherTir, // Variable empechant le deplacement lorsque le mode de tir est actif
        controllerScheme;

    public AudioClip sonTir; // Variable permettant de jouer un son lorsque l'on tire

    private InputJoueur i_inputJoueur; // le player input du joueur

    private GameObject sprite; // le gameObject du sprite


    [Header("Pour autres scripts")]
    public Vector2 v_deplacementCible; // Variable determinant la direction dans laquelle le projectile est tirer
    public GameObject pouvoirTir;

    private void Awake()
    {
        i_inputJoueur = new InputJoueur();
        i_inputJoueur.TirLucioles.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {

        c_lineRenderer = gameObject.GetComponent<LineRenderer>(); // Assigner le LineRenderer a c_lineRenderer
        playerInput = gameObject.GetComponent<PlayerInput>(); // Assigner le PlayerInput a playerInput
        c_lineRenderer.enabled = false; // Desactiver le LineRenderer
        playerInput.neverAutoSwitchControlSchemes = false; // Permettre le changement automatique du control scheme 
        sprite = gameObject.transform.Find("Sprite").gameObject; // assigner le sprite
    }

    private void Update()
    {
        if (playerInput.currentActionMap.ToString().Contains("Tir") && !GetComponent<dialogues>().texteActivee)
        {
            flecheViser.SetActive(true);
            Vector2 zoneViseSouris = i_inputJoueur.TirLucioles.PositionSouris.ReadValue<Vector2>();
            Vector2 zoneViseGamepad = i_inputJoueur.TirLucioles.PositionManette.ReadValue<Vector2>();


            if (GetComponent<Joueur_Script>().modeSouris)
            {
                Debug.Log(zoneViseSouris);
                // Sauvegarder la valeur dans le monde de la position de la souris
                v_sourisPosition = Camera.main.ScreenToWorldPoint(zoneViseSouris);

                // Prendre la direction de la souris et le normalizer
                v_deplacementCible = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) - new Vector2(v_sourisPosition.x, v_sourisPosition.y);
                v_deplacementCible = v_deplacementCible.normalized * -1;
                flecheViser.transform.rotation = Quaternion.LookRotation(Vector3.forward, v_deplacementCible);
                flecheViser.transform.rotation *= Quaternion.Euler(0, 0, 90);
            }
            else
            {

                //Si le joueur joue avec une manette...
                // Utiliser la position du curseur * 15 pour le tir et mettre la valeur de v_deplacement relative a la position du personnage
                v_deplacementCible = gameObject.transform.position + new Vector3(zoneViseGamepad.x, zoneViseGamepad.y, 0f) * 15f;
                //v_deplacementCible = gameObject.transform.position + new Vector3(v_deplacementCible.x, v_deplacementCible.y, 0);
                flecheViser.transform.rotation = Quaternion.LookRotation(Vector3.forward, zoneViseGamepad);
                flecheViser.transform.rotation *= Quaternion.Euler(0, 0, 90);

            }

        }
        else
        {
            flecheViser.SetActive(false);
        }
    }

    // Fonction qui gere l'attaque corps a corps du personnage
    public void AttaquePhysique(InputAction.CallbackContext context)
    {
        // Lorsque la touche est appuy?e...
        if (context.started)
        {
            // Trigger l'animation (a faire)
            // Activer le knockback de l'attaque
            gameObject.transform.Find("KnockbackAttaque").gameObject.SetActive(true);
            if (!b_knockback)
            {
                b_knockback = true;
                Invoke("DesactiverKnockback", 0.5f);
            }

        }
    }

    void DesactiverKnockback()
    {
        gameObject.transform.Find("KnockbackAttaque").gameObject.SetActive(false);
        b_knockback = false;
    }
    #region Tir

    // Fonction qui gere le debut du "visage" du tir de lucioles ansi que son tir
    public void DeclencherTir(InputAction.CallbackContext context)
    {
        // Lorsque le bouton est relach?...
        if (context.canceled && Joueur_Script.tirObtenu)
        {

            // Creer une variable empechant les deplacements lorsque le mode de tir est activer
            declencherTir = true;
            // Changer le map du personnage ? TirLucioles
            playerInput.SwitchCurrentActionMap("TirLucioles");
        }
    }

    // Fonction qui gere le "visage" du tir de luciole
    public void AttaqueTirViser(InputAction.CallbackContext context)
    {
        //Debug.Log(playerInput.currentControlScheme);
        //Debug.Log(context);
        // Faire apparaitre le line renderer qui va servir de viseur
        //c_lineRenderer.enabled = true;
        // Sauvegarder la valeur dans le monde de la position de la souris
        //v_sourisPosition = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
        // Dessiner l'origine du viseur
        //c_lineRenderer.SetPosition(0, gameObject.transform.position);

        // Prendre la direction de la souris et le normalizer
        //v_deplacementCible = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) - new Vector2(v_sourisPosition.x, v_sourisPosition.y);
        //v_deplacementCible = v_deplacementCible.normalized * -1;
        // Dessiner la position finale du viseur
        //c_lineRenderer.SetPosition(1, v_sourisPosition);

        // Si le joueur joue avec le clavier...
        /*if (GetComponent<Joueur_Script>().modeSouris)
        {
            // Prendre la direction de la souris et le normalizer
            v_deplacementCible = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) - new Vector2(v_sourisPosition.x, v_sourisPosition.y);
            v_deplacementCible = v_deplacementCible.normalized * -1;
            // Dessiner la position finale du viseur
            c_lineRenderer.SetPosition(1, v_sourisPosition);
        }
        flecheViser.transform.rotation = Quaternion.LookRotation(Vector3.forward, v_deplacementCible);
        flecheViser.transform.rotation *= Quaternion.Euler(0, 0, 90);*/
    }

    // Fonction qui gere l'annulation du tir durant son "visage"
    public void AnnulerTirViser(InputAction.CallbackContext context)
    {
        // Si le bouton est lach?...
        if (context.canceled)
        {
            declencherTir = false;
            // Changer la map au mouvement et d?sactiver le viseur
            playerInput.SwitchCurrentActionMap("Player");
            c_lineRenderer.enabled = false;
        }
    }

    // Fonction qui tire la balle
    public void AttaqueTir(InputAction.CallbackContext context)
    {
        // Lorsque le bouton est appuy?
        if (context.started && !GetComponent<dialogues>().texteActivee)
        {
            if (pouvoirTir.GetComponent<PouvoirUI>().peutUtiliserPouvoir)
            {
                pouvoirTir.GetComponent<PouvoirUI>().utiliserPouvoir();
                sprite.GetComponent<Animator>().SetTrigger("Tir");
                GetComponent<AudioSource>().PlayOneShot(sonTir);
                // Cloner et activer la balle
                g_clone = Instantiate(projectile.gameObject, projectile.transform.position, c_lineRenderer.transform.rotation);
                g_clone.SetActive(true);
            }
        }
    }

    public void ManetteTir(InputAction.CallbackContext context)
    {
        Debug.Log("hahahahahahahahhahaAHHAAH");

        //c_lineRenderer.enabled = true;
        // Sauvegarder la valeur dans le monde de la position de la souris
        //v_sourisPosition = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
        // Dessiner l'origine du viseur


        if (!GetComponent<Joueur_Script>().modeSouris)
        {
            //gameObject.GetComponent<PlayerInput>().SwitchCurrentControlScheme("Gamepad");
            //Si le joueur joue avec une manette...
            // Utiliser la position du curseur * 10 pour le tir et mettre la valeur de v_deplacement relative a la position du personnage
            v_deplacementCible = context.ReadValue<Vector2>() * 15f;
            v_deplacementCible = gameObject.transform.position + new Vector3(v_deplacementCible.x, v_deplacementCible.y, 0);


        }

    }
    #endregion
}