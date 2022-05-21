using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportementSurveillance : StateMachineBehaviour
{
    /** Script de surveillance des ennemis
     * Créé par Guillaume Gauthier-Benoît
     * Dernière modification: 01/05/22
     */
    [Header("Distance avant que l'ennemi entre en idle")]
    public float distancePersoEnnemiIdle; // Distance avant que l'ennemi entre en idle
    [Header("Distance avant que l'ennemi entre en poursuite")]
    public float distancePersoEnnemiSuivre; // Distance avant que l'ennemi entre en poursuite

    public GameObject g_joueurPos; // gameobject du joueur
    public float vitesse; // vitesse de l'ennemi

    private RaycastHit2D infoRaycast;   // raycast pour le sol
    private Vector3 v_tailleCollider,   // les différentes extrémitées du sol
        v_tailleColliderNeg;
    private int i_layerMask; // le layer du sol

    private bool oriDirection = true; // Valeur boolean pour savoir si l'ennemi va dans sa direction originale ou pas

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Setter les valeurs des variables
        i_layerMask = LayerMask.GetMask("Sol");
        g_joueurPos = GameObject.Find("Beepo");
        infoRaycast = Physics2D.Raycast(animator.transform.position, Vector2.down, 10000, i_layerMask);
        // si l'ennemi touche au sol, calculer la taille du collider au sol
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

        // Si les dialogues ne sont pas activees, faire en sorte que l'ennemi suive le personnage
        if (!g_joueurPos.GetComponent<dialogues>().texteActivee)
        {
            // Si l'ennemi va dans sa direction originale, le déplacer dans cette direction, sinon, le déplacer dans la direction opposée
            if (oriDirection)
            {
                animator.transform.position = Vector3.MoveTowards(animator.transform.position, new Vector3(v_tailleCollider.x, animator.transform.position.y, animator.transform.position.z), vitesse * Time.deltaTime);
            }
            else
            {
                animator.transform.position = Vector3.MoveTowards(animator.transform.position, new Vector3(v_tailleColliderNeg.x, animator.transform.position.y, animator.transform.position.z), vitesse * Time.deltaTime);
            }
        }

        // Lorsque le personnage est assez loin de l'ennemi, le mettre en idle
        if (Vector2.Distance(animator.transform.position, g_joueurPos.transform.position) >= distancePersoEnnemiIdle)
        {
            animator.SetBool("estSurveille", false);
            animator.SetBool("estSuivre", false);
        }
        // Ou lorsque le personnage est assez près, le mettre en mode de poursuite
        else if (Vector2.Distance(animator.transform.position, g_joueurPos.transform.position) <= distancePersoEnnemiSuivre)
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
    }
}
