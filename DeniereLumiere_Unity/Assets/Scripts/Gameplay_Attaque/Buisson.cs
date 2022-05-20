using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buisson : MonoBehaviour
{
    /** Script de gestion des buissons dans le monde
     * Cree par Jerome Trottier
     * Derniere modification: 20/05/22
     */

    [Header("Le panel UI du compteur de luciole")]
    public GameObject compteurLuciole;
    private LuciolesCompteurController l_compteurLucioles;

    [Header("Systeme de particule des lucioles")]
    public ParticleSystem luciolesParticules;
    private Animator animationBuisson;

    //Nombre de lucioles dans le buisson
    private int i_nbreLuciolesBuisson = 12;

    private void Start()
    {
        animationBuisson = GetComponent<Animator>();
        l_compteurLucioles = compteurLuciole.GetComponent<LuciolesCompteurController>();
        setParticulesBuisson(i_nbreLuciolesBuisson);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Quand le joueur entre en contact avec un buisson, le buisson refill le joueur de lucioles
        if (collision.gameObject.tag == "Player")
        {
            RefillJoueur();
        }
    }

    // Fonction qui permet de redonner aux joueur le nombre adequat de lucioles
    private void RefillJoueur()
    {
        // Variable qui determine le nombre present de lucioles sur le joueur
        int currentNbreLucioles = l_compteurLucioles.getNbreLucioles();

        // Si le nombre de luciole n'est pas egal a 12, il faut refill le joueur, sinon on ne fait rien
        if (currentNbreLucioles != 12)
        {
            // Activer l'animation du buisson
            animationBuisson.SetTrigger("Ramasse");
            
            // Variable qui determine combien de luciole il faut donner au joueur
            int nbreLuciolesARefill = 12 - currentNbreLucioles;

            // Si le nombre de luciole a redonner est superieur au nombre de luciole presente dans le buisson, on redonne simplement l'entierete de 
            // ce qui reste dans le buisson
            if (nbreLuciolesARefill >= i_nbreLuciolesBuisson)
            {
                l_compteurLucioles.ajouterLucioles(i_nbreLuciolesBuisson);
                i_nbreLuciolesBuisson = 0;
                setParticulesBuisson(i_nbreLuciolesBuisson);
            }
            else // Sinon, on lui donne le bon montant de luciole
            {
                l_compteurLucioles.ajouterLucioles(nbreLuciolesARefill);
                i_nbreLuciolesBuisson -= nbreLuciolesARefill;
                setParticulesBuisson(i_nbreLuciolesBuisson);
            }
        }
    }

    // Fonction qui permet de regenerer les lucioles du buisson
    private void RefillBuisson()
    {
        i_nbreLuciolesBuisson = 12;
        setParticulesBuisson(i_nbreLuciolesBuisson);
    }

    // Methode qui permet d'afficher le bon nombre de lucioles autour du buisson, gerer par un systeme de particules
    private void setParticulesBuisson(int nbreLucioles)
    {
        var luciolesEmission = luciolesParticules.emission;
        luciolesEmission.rateOverTime = nbreLucioles / 4;
    }

}
