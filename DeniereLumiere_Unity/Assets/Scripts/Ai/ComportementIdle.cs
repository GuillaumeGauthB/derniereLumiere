using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportementIdle : StateMachineBehaviour
{
    /** Script du mode Idle des ennemis
     * Créé par Guillaume Gauthier-Benoît
     * Dernière modification: 08/04/22
     */

    private Transform t_joueurPos; // le transform du joueur

    [Header("Distance avant que l'ennemi entre en surveillance")]
    public float distancePersoEnnemiSurveillance; // Distance avant que l'ennemi entre en mode de surveillance

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Setter le transform du personnage principal
        t_joueurPos = GameObject.Find("Beepo").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Lorsque le personnage est assez près de l'ennemi, le mettre dans le mode de surveillance
        if(Vector2.Distance(animator.transform.position, t_joueurPos.position) < distancePersoEnnemiSurveillance)
        {
            animator.SetBool("estSurveille", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
