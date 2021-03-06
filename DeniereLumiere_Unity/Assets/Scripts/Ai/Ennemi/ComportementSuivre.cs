using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportementSuivre : StateMachineBehaviour
{
    /** Script de poursuite des ennemis
     * Cr?? par Guillaume Gauthier-Beno?t
     * Derni?re modification: 01/05/22
     */

    private GameObject g_joueurPos; // le gameobject du joueur
    public float vitesse; // la vitesse de deplacement

    [Header("Distance avant que l'ennemi entre en surveillance")]
    public float distancePersoEnnemiSurveillance; // la distance provoquant un changement de state

    private RaycastHit2D infoRaycast; // le raycast touchant le sol
    private Vector3 v_tailleCollider,   // les diff?rentes extr?mit?es du sol
        v_tailleColliderNeg;

    private int i_layerMask;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Set les valeurs correctement
        i_layerMask = LayerMask.GetMask("Sol");
        g_joueurPos = GameObject.Find("Beepo");
        infoRaycast = Physics2D.Raycast(animator.transform.position, Vector2.down, 3, i_layerMask);
        // si le raycast touche au sol, calculer la taille du collider
        if (infoRaycast)
        {
            v_tailleCollider = infoRaycast.collider.bounds.extents + infoRaycast.collider.bounds.center - new Vector3(animator.GetComponent<Collider2D>().bounds.extents.x, 0, 0);
            v_tailleColliderNeg = infoRaycast.collider.bounds.center - infoRaycast.collider.bounds.extents + new Vector3(animator.GetComponent<Collider2D>().bounds.extents.x, 0, 0);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


        // Si les dialogues ne sont pas activees, faire en sorte que l'ennemi suive le personnage
        if (!g_joueurPos.GetComponent<dialogues>().texteActivee)
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, new Vector2(g_joueurPos.transform.position.x, animator.transform.position.y), vitesse * Time.deltaTime);
        }

        // Si le personnage s'?loigne trop loin de l'ennemi, le faire passer ? l'?tat de surveillance
        if (Vector2.Distance(animator.transform.position, g_joueurPos.transform.position) > distancePersoEnnemiSurveillance)
        {
            animator.SetBool("estSuivre", false);
            animator.SetBool("estSurveille", false);
        }

        // Lorsque l'ennemi atteint une des extr?mit?es du sol, le garder ? ce point et l'emp?cher de continuer ? avancer
        if (v_tailleCollider.x <= animator.transform.position.x)
        {
            animator.transform.position = new Vector2(v_tailleCollider.x, animator.transform.position.y);
        }
        else if (v_tailleColliderNeg.x >= animator.transform.position.x)
        {
            animator.transform.position = new Vector2(v_tailleColliderNeg.x, animator.transform.position.y);
        }
    }
}
