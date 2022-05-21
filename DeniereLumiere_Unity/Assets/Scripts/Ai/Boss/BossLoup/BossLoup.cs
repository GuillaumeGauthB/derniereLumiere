using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLoup : MonoBehaviour
{
    /** Script de gestion du boss loup dans niveau 2
     * Créé par Guillaume Gauthier-Benoît
     * Dernière modification: 20/05/22
     */

    bool b_attaqueEnCours; // valeur boolean permettant de savoir si une attaque est en cours
    int i_attaqueAExecuter; // valeur int detectant l'attaque a executer
    float f_distancePersoBoss; // valeur float detectant la distance entre le personnage et le boss
    GameObject g_perso; // le gameobject du joueur

    [Header("Globale")]
    public float cooldownValeur; // la longueur du cooldown
    private int i_derniereAttaque; // la derniere attaque executee
    public GameObject sprite; // le sprite du boss
    public float distanceAttaquePhysique; // distance pour attaque physique
    public GameObject collidersBossPiece; // les colliders de la piece du boss
    
    [Header("Changement de plateformes")]
    public GameObject[] setPlateformes; // tableau contenant les differents "sets" de plateformes
    public GameObject attaqueLuciole; // les lucioles ennemis (original)
    private GameObject g_attaqueLuciole_clone; // les lucioles ennemis (clone) 
    public Transform[] plateformes; // les individuelles dans setPlateformes (dependamment du "set")
    int i_plateformeActivee = 0; // la position dans le tableau du "set" de plateformes activees
    int i_plateformeActivable; // les differentes position dans le tableau des "sets" pouvant etre activee

    [Header("Attaque Saut")]
    public GameObject[] picsGauche, // les pics a gauche du boss
        picsDroit; // les pics a droite du boss

    [Header("Attaque Deplacement")]
    private int i_attaqueDeplacementLocation; // le type et la direction de l'attaque executee
    public GameObject transformationDeplacement, // l'apparence du boss en attaque deplacement
        deplacementHaut, // avertissement de l'attaque en haut
        deplacementBas; // avertissement de l'attaque en bas

    [Header("Attaque Corps a Corps")]
    public GameObject[] collidersCorps; // tableau contenant les colliders du corps du boss

    [Header("Sons")]
    public AudioClip sonChangementPlateforme, // les differents sons des attaques
        sonAttaqueCorps,
        sonAttaqueSaut;

    private void Start()
    {
        //Initaliser le personnage
        g_perso = GameObject.Find("Beepo");
        // appeler AcctiverColliderBoss apres 2 secondes
        Invoke("ActiverColliderBoss", 2f);
    }

    // Fonction enfermant le personnage avec le boss
    void ActiverColliderBoss()
    {
        // Activer les colliders
        collidersBossPiece.SetActive(true);
    }

    // IEnumerator gerant le cooldown de l'attaque
    IEnumerator Cooldown()
    {
        // Attendre la duree du cooldown
        yield return new WaitForSeconds(cooldownValeur);
        // permettre une nouvelle attaque
        b_attaqueEnCours = false;
        // arreter l'enumerator
        yield return null;
    }

    // IEnumerator gerant le changement de plateformes
    IEnumerator ChangementPlateforme()
    {
        // jouer le son de l'attaque
        GetComponent<AudioSource>().PlayOneShot(sonChangementPlateforme);
        // jouer l'animation
        sprite.GetComponent<Animator>().SetTrigger("attaque");
        // attendre 0.25s
        yield return new WaitForSeconds(0.25f);
        // jouer le son de coup sur le sol
        GetComponent<AudioSource>().PlayOneShot(sonAttaqueCorps);
        // attendre la longueur de l'animation
        yield return new WaitForSeconds(sprite.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 0.25f);
        // Pour toutes les groupes de plateformes dans setPlateformes...
        foreach(GameObject plateformeGroupe in setPlateformes)
        {
            // Si le groupe de plateformes est activee...
            if (plateformeGroupe.activeSelf)
            {
                // Faire apparaitre des particules sur toutes les plateformes individuelles pour prevenir le changement
                plateformes = plateformeGroupe.GetComponentsInChildren<Transform>();
                foreach (Transform plateforme in plateformes)
                {
                    if(plateforme.name != "Env1" || plateforme.name != "Env2" || plateforme.name != "Env3" || plateforme.gameObject.layer != 3)
                    {
                        plateforme.GetChild(0).gameObject.SetActive(true);
                    }
                }
                // attendre 1.5 seconde
                yield return new WaitForSeconds(1.5f);
                // et desactiver les particules sur les plateformes individuelles
                foreach (Transform plateforme in plateformes)
                {
                    if (plateforme.name != "Env1" || plateforme.name != "Env2" || plateforme.name != "Env3" || plateforme.gameObject.layer != 3)
                    {
                        plateforme.GetChild(0).gameObject.SetActive(false);
                    }
                }
                // desactiver le groupe de plateformes
                plateformeGroupe.SetActive(false);
            }
        }
        // choisir au hasard entre toutes les groupes de plateformes disponible un groupe
        i_plateformeActivable = Random.Range(0, setPlateformes.Length);
        // si ce groupe est celui qui etait deja activee, refaire le choix
        while(i_plateformeActivable == i_plateformeActivee)
        {
            i_plateformeActivable = Random.Range(0, setPlateformes.Length);
        }
        // sauvegarder quelle plateforme est activee dans i_plateformeActivee
        i_plateformeActivee = i_plateformeActivable;
        // activer le groupe de plateformes choisit
        setPlateformes[i_plateformeActivee].SetActive(true);
        // activer precisement la premiere plateforme du groupe sinon elle n'apparait pas
        setPlateformes[i_plateformeActivee].gameObject.transform.GetChild(0).gameObject.SetActive(true);
        // Cloner l'attaque de luciole et enclencher son attaque
        g_attaqueLuciole_clone = Instantiate(attaqueLuciole, attaqueLuciole.transform.position, attaqueLuciole.transform.rotation);
        g_attaqueLuciole_clone.transform.localScale = new Vector2(1, 1);
        g_attaqueLuciole_clone.SetActive(true);
        // commencer le cooldown de l'attaque
        StartCoroutine("Cooldown");
        // arreter la coroutine
        yield return null;
    }

    // IEnumerator gerant l'attaque en saut
    IEnumerator AttaqueSaut()
    {
        //jouer le son de l'attaque
        GetComponent<AudioSource>().PlayOneShot(sonAttaqueSaut);
        // desactiver le sprite du boss
        sprite.GetComponent<SpriteRenderer>().enabled = false;

        // activer son systeme de particules
        transformationDeplacement.SetActive(true);
        // Si le boss se trouve a la droite de l'ecran...
        if (!sprite.GetComponent<SpriteRenderer>().flipX)
        {
            // executer l'animation de droite
            GetComponent<Animator>().SetTrigger("SautDroit");
        }
        // sinon...
        else
        {
            // executer l'animation de gauche
            GetComponent<Animator>().SetTrigger("Saut");
        }
        // attendre la longueur de l'animation
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        // jouer le son de collision au sol
        GetComponent<AudioSource>().PlayOneShot(sonAttaqueCorps);
        // attendre la longueur de l'animation + 0.5 seconde
        yield return new WaitForSeconds(0.5f);
        // activer les pics et les animations de pics de gauche et de droits
        for(int i = 0; i < picsDroit.Length; i++)
        {
            picsDroit[i].gameObject.SetActive(true);
            picsGauche[i].gameObject.SetActive(true);
            picsDroit[i].gameObject.GetComponent<Animator>().SetBool("Pic", true);
            picsGauche[i].gameObject.GetComponent<Animator>().SetBool("Pic", true);
            // attendre 0.25s entre chaque pic
            yield return new WaitForSeconds(0.25f);
        }
        // attendre une seconde
        yield return new WaitForSeconds(1);
        // desactiver l'animation pour chacun d'entre eux
        for (int i = 0; i < picsDroit.Length; i++)
        {
            picsDroit[i].gameObject.GetComponent<Animator>().SetBool("Pic", false);
            picsGauche[i].gameObject.GetComponent<Animator>().SetBool("Pic", false);
        }
        // attendre une seconde
        yield return new WaitForSeconds(1.5f);
        // et desactiver les pics
        for (int i = 0; i < picsDroit.Length; i++)
        {
            picsDroit[i].gameObject.SetActive(false);
            picsGauche[i].gameObject.SetActive(false);
        }
        // desactiver le sprite du boss
        sprite.GetComponent<SpriteRenderer>().enabled = true;
        // activer son systeme de particules
        transformationDeplacement.SetActive(false);

        // commencer le cooldown de l'attaque
        StartCoroutine("Cooldown");
        // arreter la coroutine
        yield return null;
    }

    // IEnumerator gerant l'attaque deplacement
    IEnumerator AttaqueDeplacement()
    {

        // choisir au hasard 1 ou 2
        i_attaqueDeplacementLocation = Random.Range(0, 3);
        // desactiver le sprite renderer du personnage
        sprite.GetComponent<SpriteRenderer>().enabled = false;
        // activer le systeme de particule de l'attaque
        transformationDeplacement.SetActive(true);
        // si le nombre choisit au hasard est egal a 1...
        if (i_attaqueDeplacementLocation == 1)
        {
            // activer l'avertissement de l'attaque en bas
            deplacementBas.SetActive(true);
        }
        // sinon...
        else
        {
            // activer l'avertissement de l'attaque en haut
            deplacementHaut.SetActive(true);
        }
        // attendre 2 secondes avant d'activer l'attaque
        yield return new WaitForSeconds(2f);
        // si le sprite renderer est flipped...
        if (!sprite.GetComponent<SpriteRenderer>().flipX)
        {
            // Activer l'animation d'attaque de droite (<---------)
            // ... et que le nombre choisit au hasard est egal a 1...
            if (i_attaqueDeplacementLocation == 1)
            {
                // activer l'animation d'attaque en bas
                GetComponent<Animator>().SetTrigger("AttaqueDroit");
            }
            // sinon...
            else
            {
                // activer l'animation d'attaque en haut
                GetComponent<Animator>().SetTrigger("AttaqueDroitHaut");
            }
        }
        // sinon...
        else
        {
            // Activer l'animation d'attaque de gauche (--------->)
            // ... et que le nombre choisit au hasard est egal a 1...
            if (i_attaqueDeplacementLocation == 1)
            {
                // activer l'animation d'attaque en bas
                GetComponent<Animator>().SetTrigger("AttaqueGauche");
            }
            // sinon...
            else
            {
                // activer l'animation d'attaque en haut
                GetComponent<Animator>().SetTrigger("AttaqueGaucheHaut");
            }
        }
        // desactiver les avertissements d'attaque
        deplacementBas.SetActive(false);
        deplacementHaut.SetActive(false);
        // si l'attaque est en bas...
        if (i_attaqueDeplacementLocation == 1) yield return new WaitForSeconds(1); // attendre une seconde
        else yield return new WaitForSeconds(2); // sinon attendre 2 secondes
        // flipper le sprite renderer
        sprite.GetComponent<SpriteRenderer>().flipX = !sprite.GetComponent<SpriteRenderer>().flipX;
        // reactiver le sprite renderer
        sprite.GetComponent<SpriteRenderer>().enabled = true;

        // desactiver le systeme de particules de l'attaque
        transformationDeplacement.SetActive(false);
        // changer la position du sprite a une autre position locale dependamment de si son SpriteRenderer est flipX
        if (!sprite.GetComponent<SpriteRenderer>().flipX)
        {
            // changer la position du sprite locale pour sa position quand a droite
            sprite.transform.localPosition = new Vector3(1.03f, 0.72f);
        }
        else
        {
            // changer la position du sprite locale pour sa position quand a gauche
            sprite.transform.localPosition = new Vector3(-0.93f, 0.72f);
        }
        // commencer le cooldown
        StartCoroutine("Cooldown");
        // arreter la coroutine
        yield return null;
    }
    // la fonction de l'attaque corps a corps
    IEnumerator AttaquePhysique()
    {
        // Commencer l'attaque corps a corps
        sprite.GetComponent<Animator>().SetTrigger("attaque");
        // attendre 0.25s
        yield return new WaitForSeconds(0.25f);
        // jouer le son de coup
        GetComponent<AudioSource>().PlayOneShot(sonAttaqueCorps);
        // activer les colliders
        foreach (GameObject col in collidersCorps)
        {
            col.SetActive(true);
        }
        // attendre la longueur de l'animation
        yield return new WaitForSeconds(sprite.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        // desactiver les colliders
        foreach (GameObject col in collidersCorps)
        {
            col.SetActive(false);
        }
        // commencer le cooldown
        StartCoroutine("Cooldown");
        // arreter la coroutine
        yield return null;
    }

    private void FixedUpdate()
    {
        // si une attaque n'est pas en cours...
        if (!b_attaqueEnCours)
        {
            // choisir un numbre au hasard de 1 un 3
            i_attaqueAExecuter = Random.Range(1, 4);
            // calculer la distance entre le boss et le perso
            f_distancePersoBoss = Vector2.Distance(gameObject.transform.position, g_perso.transform.position);
            // si le perso est pres du boss...
            if (f_distancePersoBoss <= distanceAttaquePhysique)
            {
                // commencer l'attaque physique
                StartCoroutine("AttaquePhysique");
                b_attaqueEnCours = true;
            }
            // si l'attaque a executer n'est pas la meme que la derniere attaque utilisee...
            else if(i_derniereAttaque != i_attaqueAExecuter)
            {
                // ... et que le nombre choisit est 1...
                if (i_attaqueAExecuter == 1)
                {
                    // activer l'attaque de changement de plateformes
                    StartCoroutine("ChangementPlateforme");
                }
                // ... si le nombre est 2...
                else if (i_attaqueAExecuter == 2)
                {
                    // activer l'attaque de deplacement
                    StartCoroutine("AttaqueDeplacement");
                }
                // ... sinon
                else
                {
                    // activer l'attaque saut
                    StartCoroutine("AttaqueSaut");
                }
                // sauvegarder qu'une attaque est en cours
                b_attaqueEnCours = true;
                // sauvegarder l'attaque en tant que derniere attaque utilisee
                i_derniereAttaque = i_attaqueAExecuter;
            }
        }

        // si le boss a moins que 50% de sa vie...
        if(GetComponent<degatEnnemi>().ennemiVie < 50)
        {
            // le faire attaquer plus vite
            cooldownValeur = 3f;
        }
    }
}
