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
    public Vector2 v_deplacementCible,
        v_deplacementCibleM; // Variable determinant la direction dans laquelle le projectile est tirer
    public GameObject pouvoirTir; // objet du ui contenant la permission de tirer

    private void Awake()
    {
        // regarder pour des inputs
        i_inputJoueur = new InputJoueur();
        i_inputJoueur.TirLucioles.Enable();
        i_inputJoueur.TirLucioles.Tir.started += AttaqueTir;
        i_inputJoueur.TirLucioles.AnnulerTir.canceled += AnnulerTirViser;
        i_inputJoueur.TirLucioles.ChangerClavier.started += GetComponent<Joueur_Script>().ChangerClavier;
        i_inputJoueur.TirLucioles.ChangerManette.started += GetComponent<Joueur_Script>().ChangerManette;
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
        // si le personnage est dans l'action map du tir et que les dialogues ne sont pas activees
        if (playerInput.currentActionMap.ToString().Contains("Tir") && !GetComponent<dialogues>().texteActivee)
        {
            // faire apparaitre la fleche et calculer les valeurs du visage manette et souris
            flecheViser.SetActive(true);
            Vector2 zoneViseSouris = i_inputJoueur.TirLucioles.PositionSouris.ReadValue<Vector2>();
            Vector2 zoneViseGamepad = i_inputJoueur.TirLucioles.PositionManette.ReadValue<Vector2>();

            // si on joue sur souris, utiliser le mode de visage de souris, sinon, manette
            if (GetComponent<Joueur_Script>().modeSouris)
            {
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
                v_deplacementCible = new Vector3(zoneViseGamepad.x, zoneViseGamepad.y, 0f) * 15f;
                flecheViser.transform.rotation = Quaternion.LookRotation(Vector3.forward, zoneViseGamepad);
                flecheViser.transform.rotation *= Quaternion.Euler(0, 0, 90);

            }

        }
        // sinon, desactiver la fleche
        else
        {
            flecheViser.SetActive(false);
        }
    }

    // Fonction qui gere l'attaque corps a corps du personnage
    public void AttaquePhysique(InputAction.CallbackContext context)
    {
        // Lorsque la touche est appuyee...
        if (context.started)
        {
            // Activer le knockback de l'attaque
            gameObject.transform.Find("KnockbackAttaque").gameObject.SetActive(true);
            // empecher le spam de l'attaque
            if (!b_knockback)
            {
                b_knockback = true;
                Invoke("DesactiverKnockback", 0.5f);
            }

        }
    }

    // fonction qui desactive le knockback
    void DesactiverKnockback()
    {
        // desactiver le knockback
        gameObject.transform.Find("KnockbackAttaque").gameObject.SetActive(false);
        b_knockback = false;
    }
    #region Tir

    // IEnumerator qui permet de changer entre mode tir et mode deplacement
    IEnumerator ChangerDeclencherTir(string type)
    {
        // attendre 0.1s
        yield return new WaitForSeconds(0.1f);
        // si le parametre est declencehr, declencherTir = true, sinon, false
        if(type == "declencher")
        {
            declencherTir = true;
        }
        else
        {
            declencherTir = false;
        }
        yield return null;
    }

    // Fonction qui gere le debut du "visage" du tir de lucioles ansi que son tir
    public void DeclencherTir(InputAction.CallbackContext context)
    {
        // Lorsque le bouton est relach?...
        if (context.canceled && Joueur_Script.tirObtenu && !declencherTir)
        {
            // changer le mode du personnage
            StartCoroutine(ChangerDeclencherTir("declencher"));
            // Changer le map du personnage ? TirLucioles
            playerInput.SwitchCurrentActionMap("TirLucioles");
        }
    }

    // Fonction qui gere l'annulation du tir durant son "visage"
    public void AnnulerTirViser(InputAction.CallbackContext context)
    {
        // Si le bouton est lach?...
        if (context.canceled && declencherTir)
        {
            // changer le mode du personnage
            StartCoroutine(ChangerDeclencherTir("fermer"));
            // Changer la map au mouvement et d?sactiver le viseur
            playerInput.SwitchCurrentActionMap("Player");
            c_lineRenderer.enabled = false;
        }
    }

    // Fonction qui tire la balle
    public void AttaqueTir(InputAction.CallbackContext context)
    {
        // Lorsque le bouton est appuye et que le joueur peut tirer...
        if (context.started && !GetComponent<dialogues>().texteActivee && declencherTir)
        {
            if (pouvoirTir.GetComponent<PouvoirUI>().peutUtiliserPouvoir)
            {
                // utiliser le pouvoir et le cooldown, ainsi que son animation et son son
                pouvoirTir.GetComponent<PouvoirUI>().utiliserPouvoir();
                sprite.GetComponent<Animator>().SetTrigger("Tir");
                GetComponent<AudioSource>().PlayOneShot(sonTir);
                // Cloner et activer la balle
                g_clone = Instantiate(projectile.gameObject, projectile.transform.position, c_lineRenderer.transform.rotation);
                g_clone.SetActive(true);
            }
        }
    }

    // Fonction obsolete, qui est encore dans le code juste au cas
    public void ManetteTir(InputAction.CallbackContext context)
    {

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