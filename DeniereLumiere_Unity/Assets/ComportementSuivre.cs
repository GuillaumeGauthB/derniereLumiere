using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportementSuivre : StateMachineBehaviour
{
    private Transform t_joueurPos;
    public float vitesse;
    public float distancePersoEnnemi;

    private Transform t_checkSol;
    private bool b_surSol;
    private RaycastHit2D infoRaycast,
        infoRaycastDroit,
        infoRaycastGauche;
    private Vector3 v_tailleCollider;
    private Vector3 v_tailleColliderNeg;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        t_checkSol = animator.transform;
        t_joueurPos = GameObject.Find("Beepo").transform;
        infoRaycast = Physics2D.Raycast(animator.transform.position, Vector2.down);
        infoRaycastGauche = Physics2D.Raycast(animator.transform.position, Vector2.left);
        infoRaycastDroit = Physics2D.Raycast(animator.transform.position, Vector2.right);
        v_tailleCollider = infoRaycast.collider.bounds.extents + infoRaycast.collider.bounds.center - new Vector3(animator.GetComponent<Collider2D>().bounds.extents.x, 0, 0);
        v_tailleColliderNeg = infoRaycast.collider.bounds.center - infoRaycast.collider.bounds.extents + new Vector3(animator.GetComponent<Collider2D>().bounds.extents.x, 0, 0);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, new Vector2(t_joueurPos.position.x, animator.transform.position.y), vitesse * Time.deltaTime);
        if (Vector2.Distance(animator.transform.position, t_joueurPos.position) >= distancePersoEnnemi)
        {
            animator.SetBool("estSuivre", false);
            animator.SetBool("estSurveillance", true);
        }

        if(v_tailleCollider.x <= animator.transform.position.x)
        {
            animator.transform.position = new Vector2(v_tailleCollider.x, animator.transform.position.y);
        }
        else if(v_tailleColliderNeg.x >= animator.transform.position.x)
        {
            animator.transform.position = new Vector2(v_tailleColliderNeg.x, animator.transform.position.y);
        }
        Debug.DrawRay(animator.transform.position, Vector2.down, Color.red);

        // Detection des collisions a droite et a gauche
        if (infoRaycastDroit)
        {
            if (infoRaycastDroit.collider.gameObject.layer == 3)
            {
                animator.transform.position = infoRaycastDroit.collider.bounds.extents - infoRaycastDroit.collider.bounds.center + new Vector3(animator.GetComponent<Collider2D>().bounds.extents.x, 0, 0);
            }
        }
        else if (infoRaycastGauche)
        {
            if (infoRaycastGauche.collider.gameObject.layer == 3)
            {
                animator.transform.position = infoRaycastGauche.collider.bounds.extents + infoRaycastGauche.collider.bounds.center - new Vector3(animator.GetComponent<Collider2D>().bounds.extents.x, 0, 0);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

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
