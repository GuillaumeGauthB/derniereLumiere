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
    private PlayerInput c_playerInput; // Input qui permet de changement de map
    public Vector2 sourisPosition; // Position de la souris pour viser

    public GameObject projectile; // Le projectile de base du personnage
    private GameObject g_clone; // Le clone du projectile, va etre tirer sur les ennemis

    private bool b_knockback;

    // Start is called before the first frame update
    void Start()
    {
        c_lineRenderer = gameObject.GetComponent<LineRenderer>();
        c_playerInput = gameObject.GetComponent<PlayerInput>();
        c_lineRenderer.enabled = false;
    }

    // Fonction qui gere l'attaque corps a corps du personnage
    public void AttaquePhysique(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Attaque Physique");
            // Trigger l'animation (a faire)
            // Activer une variable qui va permettre la detection de collision quand l'attaque est performee
            gameObject.transform.Find("KnockbackAttaque").gameObject.SetActive(true);

        }
    }

    #region Tir
    // Fonction qui gere le debut du "visage" du tir de lucioles ansi que son tir
    public void DeclencherTir(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //Debug.Log("Prep tir");
            c_playerInput.SwitchCurrentActionMap("TirLucioles");
        }
    }

    // Fonction qui gere le "visage" du tir de luciole
    public void AttaqueTirViser(InputAction.CallbackContext context)
    {
        c_lineRenderer.enabled = true;
        sourisPosition = context.ReadValue<Vector2>();
        //Debug.Log(sourisPosition);
        c_lineRenderer.SetPosition(0, (Camera.main.ScreenToWorldPoint(sourisPosition)));
    }

    // Fonction qui gere l'annulation du tir durant son "visage"
    public void AnnulerTirViser(InputAction.CallbackContext context)
    {
        if(context.canceled)
        {
            //Debug.Log("Tir annuler");
            c_playerInput.SwitchCurrentActionMap("Mouvements-tests");
            c_lineRenderer.enabled = false;
        }
    }

    // Fonction qui tire la balle
    public void AttaqueTir(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //Tirer la balle
            //Debug.Log("la balle est tirer");
            g_clone = Instantiate(projectile.gameObject, projectile.transform.position, c_lineRenderer.transform.rotation);
            g_clone.SetActive(true);
        }
    }
    #endregion
}
