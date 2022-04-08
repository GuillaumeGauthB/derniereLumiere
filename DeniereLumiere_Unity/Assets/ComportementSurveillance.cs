using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportementSurveillance : StateMachineBehaviour
{
    [Header("Distance avant que l'ennemi entre en idle")]
    public float distancePersoEnnemiIdle;
    [Header("Distance avant que l'ennemi entre en poursuite")]
    public float distancePersoEnnemiSuivre;

    public Transform t_joueurPos;
    public float vitesse;

    private RaycastHit2D infoRaycast,
        infoRaycastDroit,
        infoRaycastGauche;
    private Vector3 v_tailleCollider;
    private Vector3 v_tailleColliderNeg;

    private bool oriDirection = true;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        t_joueurPos = GameObject.Find("Beepo").transform;
        infoRaycast = Physics2D.Raycast(animator.transform.position, Vector2.down);
        infoRaycastGauche = Physics2D.Raycast(animator.transform.position, Vector2.left);
        infoRaycastDroit = Physics2D.Raycast(animator.transform.position, Vector2.right);
        v_tailleCollider = infoRaycast.collider.bounds.extents + infoRaycast.collider.bounds.center - new Vector3(animator.GetComponent<Collider2D>().bounds.extents.x, 0, 0);
        v_tailleColliderNeg = infoRaycast.collider.bounds.center - infoRaycast.collider.bounds.extents + new Vector3(animator.GetComponent<Collider2D>().bounds.extents.x, 0, 0);
        //animator.transform.position = new Vector2(animator.transform.position.x, ComportementIdle.posOri.y);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.transform.position = new Vector2(animator.transform.position.x, animator.transform.position.y) * (vitesse * Time.deltaTime);
        //animator.transform.position = Vector2.MoveTowards(animator.transform.position, new Vector2(t_joueurPos.position.x, animator.transform.position.y), vitesse * Time.deltaTime);
        
        // Deplacement a droite et a gauche
        if (oriDirection)
        {
            animator.transform.position = Vector3.MoveTowards(animator.transform.position, new Vector3(v_tailleCollider.x, animator.transform.position.y, animator.transform.position.z), vitesse * Time.deltaTime);
        }
        else
        {
            animator.transform.position = Vector3.MoveTowards(animator.transform.position, new Vector3(v_tailleColliderNeg.x, animator.transform.position.y, animator.transform.position.z), vitesse * Time.deltaTime);
        }

        // Changement de state
       if (Vector2.Distance(animator.transform.position, t_joueurPos.position) >= distancePersoEnnemiIdle)
        {
            animator.SetBool("estSurveille", false);
            animator.SetBool("estSuivre", false);
        }
        else if(Vector2.Distance(animator.transform.position, t_joueurPos.position) <= distancePersoEnnemiSuivre)
        {
            animator.SetBool("estSuivre", true);
        }

        // BLoquer le personnage au bout du collider de son sol
        if (v_tailleCollider.x <= animator.transform.position.x)
        {
            animator.transform.position = new Vector2(v_tailleCollider.x, animator.transform.position.y);
            oriDirection = false;
        }
        else if (v_tailleColliderNeg.x >= animator.transform.position.x)
        {
            animator.transform.position = new Vector2(v_tailleColliderNeg.x, animator.transform.position.y);
            oriDirection = true;
        }
        Debug.DrawRay(animator.transform.position, Vector2.left, Color.red);

        // Detection des collisions a droite et a gauche
        /*if (infoRaycastDroit)
        {
            
            if (infoRaycastDroit.collider.gameObject.layer == 3)
            {
                Debug.Log(infoRaycastDroit.collider.bounds.extents.x - infoRaycastDroit.collider.bounds.center.x);
                animator.transform.position = new Vector3(infoRaycastDroit.collider.bounds.extents.x - infoRaycastDroit.collider.bounds.center.x + animator.GetComponent<Collider2D>().bounds.extents.x, 0, 0);
            }
        }
        else if (infoRaycastGauche)
        {
            if (infoRaycastGauche.collider.gameObject.layer == 3)
            {
                animator.transform.position = new Vector3(infoRaycastGauche.collider.bounds.extents.x + infoRaycastGauche.collider.bounds.center.x - animator.GetComponent<Collider2D>().bounds.extents.x, 0, 0);
            }
        }*/
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
