using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportementSurveillance : StateMachineBehaviour
{
    /** Script de surveillance des ennemis
     * Créé par Guillaume Gauthier-Benoît
     * Dernière modification: 07/04/22
     */
    [Header("Distance avant que l'ennemi entre en idle")]
    public float distancePersoEnnemiIdle; // Distance avant que l'ennemi entre en idle
    [Header("Distance avant que l'ennemi entre en poursuite")]
    public float distancePersoEnnemiSuivre; // Distance avant que l'ennemi entre en poursuite

    public Transform t_joueurPos; // Transform du joueur
    public float vitesse; // vitesse de l'ennemi

    private RaycastHit2D infoRaycast;   // raycast pour le sol
    private Vector3 v_tailleCollider,   // les différentes extrémitées du sol
        v_tailleColliderNeg;
    private int i_layerMask;

    private bool oriDirection = true; // Valeur boolean pour savoir si l'ennemi va dans sa direction originale ou pas

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Setter les valeurs des variables
        i_layerMask = LayerMask.GetMask("Sol");
        t_joueurPos = GameObject.Find("Beepo").transform;
        infoRaycast = Physics2D.Raycast(animator.transform.position, Vector2.down, 10000, i_layerMask);
        if (infoRaycast)
        {
            v_tailleCollider = infoRaycast.collider.bounds.extents + infoRaycast.collider.bounds.center - new Vector3(animator.GetComponent<Collider2D>().bounds.extents.x, 0, 0);
            v_tailleColliderNeg = infoRaycast.collider.bounds.center - infoRaycast.collider.bounds.extents + new Vector3(animator.GetComponent<Collider2D>().bounds.extents.x, 0, 0);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log(infoRaycast.collider.gameObject.layer);

        // Si l'ennemi va dans sa direction originale, le déplacer dans cette direction, sinon, le déplacer dans la direction opposée
        if (oriDirection)
        {
            animator.transform.position = Vector3.MoveTowards(animator.transform.position, new Vector3(v_tailleCollider.x, animator.transform.position.y, animator.transform.position.z), vitesse * Time.deltaTime);
        }
        else
        {
            animator.transform.position = Vector3.MoveTowards(animator.transform.position, new Vector3(v_tailleColliderNeg.x, animator.transform.position.y, animator.transform.position.z), vitesse * Time.deltaTime);
        }

        // Lorsque le personnage est assez loin de l'ennemi, le mettre en idle
       if (Vector2.Distance(animator.transform.position, t_joueurPos.position) >= distancePersoEnnemiIdle)
        {
            animator.SetBool("estSurveille", false);
            animator.SetBool("estSuivre", false);
        }
       // Ou lorsque le personnage est assez près, le mettre en mode de poursuite
        else if(Vector2.Distance(animator.transform.position, t_joueurPos.position) <= distancePersoEnnemiSuivre)
        {
            animator.SetBool("estSuivre", true);
        }

        // Lorsque l'ennemi atteint une des extrémitées du sol, le garder à ce point et l'empêcher de continuer à avancer
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
        Debug.DrawRay(animator.transform.position, Vector2.down, Color.red);



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
