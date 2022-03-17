using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* script réalisée par Jonathan Mores
 * Modifié 04/21/2021*/
/*Script qui controle le deplacement du personnage, l'attaque, les reaction au collision, le deplacement de la scene gagnante ou perdante, et les sons*/
public class DeplacementBoy : MonoBehaviour
{
    
    private bool finJeu; /* permet de savoir si le jeu est fini ou non*/
    private bool tombeAttaque; /* permet de savoir si on réalise l'attaque ou non*/

    private static bool vieBonus; /*permet de savoir si notre vie est active ou non*/

    
    public Text textPointage; /*Permet de savoir le nombre de point*/

    private static int points; /* nos points en variable statique, et oui j'ai regardé le tutoriel ;P Merci beaucouo*/

    public float VitesseMouvement;/*la vitesse X*/
    public float VitesseSaut; /*la vitesse Y*/

    public AudioClip saut1Son; /* tout nos sons*/
    public AudioClip saut2Son;
    public AudioClip saut3Son;
    public AudioClip etoileSon;
    public AudioClip sonMort;
    public AudioClip sonCoeur;
    public AudioClip sonEnnemi; 

    public GameObject ventACloner; /* le vent derriere le personnage*/
    public GameObject coeurUI; /* le coeur nous montrant que l'on a une vie*/
    
   


    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        finJeu = false; /*Au début la variable finJeu est set a false*/
        vieBonus = false;
        InvokeRepeating("LanceVent", 0f, 0.5f);
    }



    // Update is called once per frame
    void Update()
    {
        textPointage.text = "Points:" + points; /* ce qui est écrit dans le compteur*/

        if (points == 200)
        {
            SceneManager.LoadScene("SceneVictoire");
        }


        /******************** Controles *********************/

        if (finJeu == false) /* si finJeu est False on prends les touches du clavier*/
        {
            float VitesseX = GetComponent<Rigidbody2D>().velocity.x; /*Variable de vitesse horizontal*/
            float VitesseY = GetComponent<Rigidbody2D>().velocity.y; /*Variable de vitesse vertical*/



            if (Input.GetKey("a") || Input.GetKey("left")) /*si la touche a ou la flèche gauche est appuyé on flip le sprite et on deplace vers la gauche*/
            {

                VitesseX = -VitesseMouvement;
                GetComponent<SpriteRenderer>().flipX = true;

            }else if (Input.GetKey("d") || Input.GetKey("right")) /*si la touche a ou la flèche gauche est appuyé on ne flip pas le sprite et on deplace vers la gauche*/
            {
                VitesseX = VitesseMouvement;
                GetComponent<SpriteRenderer>().flipX = false;
            } else
            {
                VitesseX = GetComponent<Rigidbody2D>().velocity.x; 
            }




            if (Input.GetKeyDown("w") && Physics2D.OverlapCircle(transform.position, 0.5f) == true || Input.GetKeyDown("up") && Physics2D.OverlapCircle(transform.position, 0.5f) == true)
            { /*si on appuie sur la touche w ou la fleche de haut on donne une vitesse vertical et on active la valeur boléenne saut pour Animator */
                var sautAleatoire = Random.Range(1, 3);
                    VitesseY = VitesseSaut;
                    GetComponent<Animator>().SetBool("saut", true);

                if (sautAleatoire == 1) {
                    GetComponent<AudioSource>().PlayOneShot(saut1Son);
                }
                else if(sautAleatoire == 2)
                {
                    GetComponent<AudioSource>().PlayOneShot(saut2Son);
                } 
                else if (sautAleatoire == 3) {
                    GetComponent<AudioSource>().PlayOneShot(saut3Son);
                }

            } else
            {
                VitesseY = GetComponent<Rigidbody2D>().velocity.y; 
            }



            if (Input.GetKeyDown("s") || Input.GetKeyDown("down") && Physics2D.OverlapCircle(transform.position, 0.5f) == false && tombeAttaque == false)
            {
                Invoke("Fige", 0f);
                Invoke("ChargeBas", 0.2f);
                tombeAttaque = true;

            }



            if (transform.position.y <= -7.27 && finJeu == false) /*si le personnage descends en bas de la camera il meurt et on relance la scène*/
            {
                
                Invoke("PartirSceneMort", 2f);
                Invoke("FonctionMort", 0f);
                
            }
            GetComponent<Rigidbody2D>().velocity = new Vector2(VitesseX, VitesseY); 




            if (VitesseX > 0.5f || VitesseX < -0.5f) /* si la vitesse X est haute on active la valeur booléenne course*/
            {
                GetComponent<Animator>().SetBool("course", true);
                

            } else
            {
                GetComponent<Animator>().SetBool("course", false);
                
            }
        }




    }


    /***************************ON COLLISION******************************/


    private void OnCollisionEnter2D(Collision2D infosCollision) /*Collision*/
    {
        if ( Physics2D.OverlapCircle(transform.position,0.5f )) /* si le personnage touche une entité proche de son point d'ancrage on remet la valeur booléenne de saut a false dans l'animator et on retablit les parametres modifié du personnage */
        {
            

            if (infosCollision.gameObject.tag == "ennemi" && tombeAttaque == true) /* si on Charge vers le bas (tombeAttaque) on "tue" l'ennemi et on gagne 15 points*/
            {
                infosCollision.gameObject.SetActive(false);
                points += 15;
                GetComponent<AudioSource>().PlayOneShot(sonEnnemi);

            } else {
                GetComponent<Animator>().SetBool("saut", false);
                GetComponent<Rigidbody2D>().gravityScale = 1f;
                GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePositionY & ~RigidbodyConstraints2D.FreezePositionX;
                tombeAttaque = false;
             }

            

 

        }


        if (infosCollision.gameObject.tag == "ennemi" && finJeu == false && tombeAttaque == false) /*si on touche un ennemi, lorsque l'on ne charge pas vers le bas et que l'on a une vie bonus, on la perds sinon on meurt */
        {   if (vieBonus == true)
            {
                infosCollision.gameObject.SetActive(false);
                vieBonus = false;
                coeurUI.gameObject.SetActive(false);
                GetComponent<AudioSource>().PlayOneShot(sonEnnemi);
            }
            else
            {

                infosCollision.gameObject.GetComponent<Collider2D>().enabled = false;
                Invoke("PartirSceneMort", 2f);
                Invoke("FonctionMort", 0f);
            }
        }

        

        
        


       
    }


    /****************************ON TRIGGER******************************/






    private void OnTriggerEnter2D (Collider2D infosCollision) /* ici on établi les collisions Trigger, on uttilise le Trigger pour pas que le personnage cogner sur les objets qu'il touche*/
    {
        if (infosCollision.gameObject.tag == "etoile" && finJeu == false) /*si on touche une étoile on rajoute 5 points dans le compteur et on joue son son*/
        {
            points += 5;
            infosCollision.gameObject.SetActive(false);
            GetComponent<AudioSource>().PlayOneShot(etoileSon);

        }

        if (infosCollision.gameObject.name == "Coeur") /*si on touche un coeur on rajoute on donne une vie bonus au personnage et on joue son son*/
        {
            infosCollision.gameObject.SetActive(false);
            coeurUI.gameObject.SetActive(true);
            vieBonus = true;
            GetComponent<AudioSource>().PlayOneShot(sonCoeur);
        }


    }





    /***********************************FONCTIONS****************************************/



    void FonctionMort() /*quand la fonction est activée on active la valeur booléenne de mort dans l'animator, 
                         * on active la valeur booléenne finJeu (qui fige les touche du clavier)
                         * on arrete les mouvements horizontal
                         * et on affiche Vous avez perdu sur l'écran*/
    {
        GetComponent<Animator>().SetBool("mort", true);
        finJeu = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        GetComponent<AudioSource>().PlayOneShot(sonMort);


    }


    void PartirSceneMort() /*on charge la scene de mort*/
    {
        SceneManager.LoadScene("SceneMort");
    }



    void LanceVent()
    {
        float VitesseX = GetComponent<Rigidbody2D>().velocity.x;
        if (VitesseX > 0.5f || VitesseX < -0.5f) /* si la vitesse X est haute on active le vent*/
        {
            if (Physics2D.OverlapCircle(transform.position, 0.5f) == true) /* si le personnage touche par terre on place du vent par rapport au flipX du personnage et on fait disparaitre ce vent*/
            {
                GameObject objetClone = Instantiate(ventACloner);
                if (GetComponent<SpriteRenderer>().flipX == false)

                {
                    objetClone.SetActive(true);
                    objetClone.transform.position = transform.position + new Vector3(-1, 0.3f, 0);
                    objetClone.GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 0);

                }
                else
                {

                    objetClone.SetActive(true);
                    objetClone.transform.position = transform.position + new Vector3(1, 0.3f, 0);
                    objetClone.GetComponent<SpriteRenderer>().flipX = false;
                    objetClone.GetComponent<Rigidbody2D>().velocity = new Vector2(2, 0);

                }
                Destroy(objetClone, 2f);
            }

        }
    }
    void Fige() /*fonction qui fige completement le personnage */
    {

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

    }

    void ChargeBas() /* fonction qui die au personnage defiger tout sauf ces mouvements en Y et d'augmenter sa gravite pour qu'il charge vers le bas */
    {
        GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePositionY;
        GetComponent<Rigidbody2D>().gravityScale = 10f;
    }
    public static int RecuperePoint() /*fonction pour envoyer la variable statique des points*/
    {
        return points;
    }
    public static bool RecupererVie() /*fonction pour envoyer la variable statique de la vie bonus */
    {
        return vieBonus;
    }
}

   



