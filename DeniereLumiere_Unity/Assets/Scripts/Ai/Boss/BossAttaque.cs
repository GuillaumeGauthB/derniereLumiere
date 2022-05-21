using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttaque : MonoBehaviour
{
    /** Script principal du premier boss du jeu
     * Cree par Jonathan Mores
     * Derniere modification: 29/04/22
     */
    private GameObject g_Perso; // le gameObject du personnage
    private GameObject g_BouledeFeu; // le gameobject de la boule de feu
    private GameObject g_RayonFeu; // le gameObject du rayon
    private Animator a_AnimBoss; // l'animator du boss
    public float bossAttaqueCooldown; // le cooldown du boss
    public float bossRangeClose; // la distance entre le personnage et le boss pour qu'il fasse une attaque de pres


    // Start is called before the first frame update
    void Start()
    {
        // mettre les valeurs des variables
        a_AnimBoss = GetComponent<Animator>();
        g_Perso = GameObject.FindWithTag("Perso");
        g_BouledeFeu = transform.GetChild(0).gameObject;
        g_RayonFeu = transform.GetChild(1).gameObject;
        // appeler une fonction a chaque 2 secondes avec un cooldown
        InvokeRepeating("AttaqueBoss", 2f, bossAttaqueCooldown);
    }

    // Update is called once per frame
    void AttaqueBoss()
    {
        // Si le personnage est loin du boss...
        if (g_Perso.transform.position.x > transform.position.x + bossRangeClose)
        {
            //Faire une attaque au hasard entre FeuHaut ou FeuBas
            int attaqueRandom;
            attaqueRandom = Random.Range(1, 3);
            if (attaqueRandom == 1)
            {
                a_AnimBoss.SetTrigger("FeuHaut");
            }
            else
            {
                a_AnimBoss.SetTrigger("FeuBas");
            }
        }
        // si le personnage est trop pres du boss...
        else
        {
            // faire l'attaque de morsure
            a_AnimBoss.SetTrigger("Morsure");
        }
    }

    // Fonction pour cloner les boules de feu
    void InstantanerBouleDeFeu()
    {
        // Cloner la boule de feu
        GameObject g_clone = Instantiate(g_BouledeFeu, g_BouledeFeu.transform.position, g_BouledeFeu.transform.rotation);
        // l'activer et desactiver ses contraintes
        g_clone.SetActive(true);
        g_clone.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        // activer le script de projectile du clone
        g_clone.GetComponent<ProjectilesBoss>().enabled = true;
    }

    // Fonction pour cloner le rayon de feu
    void InstantanerRayonFeu()
    {
        // cloner le rayon de feu et l'activer
        GameObject g_clone = Instantiate(g_RayonFeu, g_RayonFeu.transform.position, g_RayonFeu.transform.rotation);
        g_clone.SetActive(true);
    }

}
