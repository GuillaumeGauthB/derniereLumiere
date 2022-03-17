using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* script réalisée par Jonathan Mores
* Modifié 04/21/2021*/
/*Ce Script permet de replacer les plateformes lorsqu'elle quitte la scene et de reactives leurs objets child*/
public class ReplacementPlateforme : MonoBehaviour
{
    public float positionYDebut; /*position Y au debut du jeu*/
    public float positionYRetour; /*Position Y lorsqu'elle réaparrait en bas*/
    public float positionXMin; /* valeur minimale X*/
    public float positionXMax; /* valeur maximale x*/
    public float positionFin; /* position considérée comme la fin de la scene */
    public GameObject Etoile; /* les objets childs que l'on veut faire réaparaite*/
    public GameObject Ennemi;
    public GameObject Coeur;

    // Start is called before the first frame update
    void Start()
    {
        float positionAleatoireX = Random.Range(positionXMin, positionXMax);
        transform.position = new Vector2(positionAleatoireX, positionYDebut);

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= positionFin) /*si la position y de l'object est plus petite ou égal a sa position de fin on la replace en haut et avec une position X aléatoire et on reactive son etoile*/
        {
            
        
            float positionAleatoireX = Random.Range(positionXMin, positionXMax);
            transform.position = new Vector2(positionAleatoireX, positionYRetour);
            
            Etoile.SetActive(true);
            

            if (Ennemi != null) /* si il y a un ennemi on le reactive*/
            {
                Ennemi.SetActive(true);
            }

            if (Coeur != null && DeplacementBoy.RecupererVie() == false) /* si le personnage n'a pas de vie extra et qu'il y a un coeur sur cette platforme on l'active a la place de l'étoile*/
            {
                Etoile.SetActive(false);
                Coeur.SetActive(true);
            }

            
        }


    }
    private void OnCollisionEnter2D(Collision2D infosCollision)
    {
        /* J'ai appris cette ligne de code en introduction à la création de Jeu Video.
           Je sais que nous avons toujours pas vue ces fonctions en classe meme si j'imagine que nous allons les voir éventuellement.
       
        
        */

        /*si on touche le game object Boy, on deviens son parent et nos déplacement sont transmis à lui*/


        if (infosCollision.gameObject.name == "Boy")
        {
            infosCollision.collider.transform.SetParent(transform);
        }

    }
    private void OnCollisionExit2D(Collision2D infosCollision) /*si on ne touche plus Boy on n'est plus son parent*/
    {
        if (infosCollision.gameObject.name == "Boy")
        {
            infosCollision.collider.transform.SetParent(null);
        }
    }
}
