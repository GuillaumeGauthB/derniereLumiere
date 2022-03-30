using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs_Guillaume : MonoBehaviour
{
    /* Attaques physique et de tir du personnage
      Par : Guillaume Gauthier-Benoit
      Dernière modification : 14/03/2022
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
        // Lorsque la touche est appuyée...
        if (context.started)
        {
            // Trigger l'animation (a faire)
            // Activer une variable qui va permettre la detection de collision quand l'attaque est performee
            // Activer le knockback de l'attaque
            gameObject.transform.Find("KnockbackAttaque").gameObject.SetActive(true);

        }
    }

    #region Tir
    // Fonction qui gere le debut du "visage" du tir de lucioles ansi que son tir
    public void DeclencherTir(InputAction.CallbackContext context)
    {
        // Lorsque le bouton est relaché...
        if (context.canceled)
        {
            // Changer le map du personnage à TirLucioles
            playerInput.SwitchCurrentActionMap("TirLucioles");
        }
    }

    // Fonction qui gere le "visage" du tir de luciole
    public void AttaqueTirViser(InputAction.CallbackContext context)
    {
        // Faire apparaitre le line renderer qui va servir de viseur
        c_lineRenderer.enabled = true;
        // Sauvegarder la valeur dans le monde de la position de la souris
        v_sourisPosition = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
        // Dessiner l'origine du viseur
        c_lineRenderer.SetPosition(0, gameObject.transform.position);

        // Si le joueur joue avec le clavier...
        if(playerInput.currentControlScheme == "Clavier")
        {
            // Prendre la direction de la souris et le normalizer
            v_deplacementCible = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) - new Vector2(v_sourisPosition.x, v_sourisPosition.y);
            v_deplacementCible = v_deplacementCible.normalized * -1;
            // Dessiner la position finale du viseur
            c_lineRenderer.SetPosition(1, v_sourisPosition);
        }
        else
        {
            //Si le joueur joue avec une manette...

            // Utiliser la position du curseur * 10 pour le tir
            v_deplacementCible = context.ReadValue<Vector2>() * 10f;
            // Dessiner la position finale du viseur
            c_lineRenderer.SetPosition(1, new Vector3(v_deplacementCible.x, v_deplacementCible.y));
            
            // Si la position du joystick est egal a 0 en x et en y...
            if(context.ReadValue<Vector2>().x == 0 && context.ReadValue<Vector2>().y == 0)
            {
                // Désactiver le viseur
                c_lineRenderer.enabled = false;
            }
            else
            {
                // Sinon, activer le viseur
                c_lineRenderer.enabled = true;
            }
        }
    }

    // Fonction qui gere l'annulation du tir durant son "visage"
    public void AnnulerTirViser(InputAction.CallbackContext context)
    {
        // Si le bouton est laché...
        if(context.canceled)
        {
            // Changer la map au mouvement et désactiver le viseur
            playerInput.SwitchCurrentActionMap("Mouvements-tests");
            c_lineRenderer.enabled = false;
        }
    }

    // Fonction qui tire la balle
    public void AttaqueTir(InputAction.CallbackContext context)
    {
        // Lorsque le bouton est appuyé
        if (context.started)
        {
            // Cloner et activer la balle
            g_clone = Instantiate(projectile.gameObject, projectile.transform.position, c_lineRenderer.transform.rotation);
            g_clone.SetActive(true);
        }
    }
    #endregion
}
