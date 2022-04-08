using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportementIdle : StateMachineBehaviour
{
    private Transform t_joueurPos;

    static public Vector2 posOri;
    static public bool test;

    [Header("Distance avant que l'ennemi entre en surveillance")]
    public float distancePersoEnnemiSurveillance;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        t_joueurPos = GameObject.Find("Beepo").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log(animator.GetComponent<Collider2D>().bounds.extents);
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
