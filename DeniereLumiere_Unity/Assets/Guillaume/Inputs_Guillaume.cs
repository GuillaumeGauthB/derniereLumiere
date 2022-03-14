using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs_Guillaume : MonoBehaviour
{
    
    public int 
        degatTir, // Les points de degats faits par le tir de luciole
        knockbackCoup, // le knockback du coup fait par l'attaque corps a corps
        vitesseKnockback; // la vitesse a laquelle les ennemis sont poussers
    
    private LineRenderer c_lineRenderer; // Component du Line Renderer pour viser
    public PlayerInput playerInput; // le playerinput
    private Vector2 v_sourisPosition; // Position de la souris pour viser

    public GameObject projectile; // Le projectile de base du personnage
    private GameObject g_clone; // Le clone du projectile, va etre tirer sur les ennemis

    private bool b_knockback; 

    [Header("Pour autres scripts")]
    public Vector2 v_deplacementCible;
    // Start is called before the first frame update
    void Start()
    {
        c_lineRenderer = gameObject.GetComponent<LineRenderer>();
        playerInput = gameObject.GetComponent<PlayerInput>();
        c_lineRenderer.enabled = false;
        playerInput.neverAutoSwitchControlSchemes = false;
    }

    // Fonction qui gere l'attaque corps a corps du personnage
    public void AttaquePhysique(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Trigger l'animation (a faire)
            // Activer une variable qui va permettre la detection de collision quand l'attaque est performee
            gameObject.transform.Find("KnockbackAttaque").gameObject.SetActive(true);

        }
    }

    #region Tir
    // Fonction qui gere le debut du "visage" du tir de lucioles ansi que son tir
    public void DeclencherTir(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            playerInput.SwitchCurrentActionMap("TirLucioles");
        }
    }

    // Fonction qui gere le "visage" du tir de luciole
    public void AttaqueTirViser(InputAction.CallbackContext context)
    {
        // Faire apparaitre le line renderer qui va sercir de 
        c_lineRenderer.enabled = true;
        v_sourisPosition = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
        
        c_lineRenderer.SetPosition(0, gameObject.transform.position);
        if(playerInput.currentControlScheme == "Clavier")
        {
            v_deplacementCible = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) - new Vector2(v_sourisPosition.x, v_sourisPosition.y);
            v_deplacementCible = v_deplacementCible.normalized * -1;
            c_lineRenderer.SetPosition(1, v_sourisPosition);
        }
        else
        {
            v_deplacementCible = context.ReadValue<Vector2>() * 10f;
            c_lineRenderer.SetPosition(1, new Vector3(v_deplacementCible.x, v_deplacementCible.y));
            
            if(context.ReadValue<Vector2>().x == 0 && context.ReadValue<Vector2>().y == 0)
            {
                c_lineRenderer.enabled = false;
            }
            else
            {
                c_lineRenderer.enabled = true;
            }
        }
    }

    // Fonction qui gere l'annulation du tir durant son "visage"
    public void AnnulerTirViser(InputAction.CallbackContext context)
    {
        if(context.canceled)
        {
            playerInput.SwitchCurrentActionMap("Mouvements-tests");
            c_lineRenderer.enabled = false;
        }
    }

    // Fonction qui tire la balle
    public void AttaqueTir(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //Tirer la balle
            g_clone = Instantiate(projectile.gameObject, projectile.transform.position, c_lineRenderer.transform.rotation);
            g_clone.SetActive(true);
        }
    }
    #endregion
}
