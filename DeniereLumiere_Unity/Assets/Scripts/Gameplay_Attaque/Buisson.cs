using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buisson : MonoBehaviour
{
    public GameObject compteurLuciole;
    private LuciolesCompteurController l_compteurLucioles;
    public ParticleSystem luciolesParticules;
    private Animator animationBuisson;

    private int i_nbreLuciolesBuisson = 12;

    private void Start()
    {
        animationBuisson = GetComponent<Animator>();
        l_compteurLucioles = compteurLuciole.GetComponent<LuciolesCompteurController>();
        setParticulesBuisson(i_nbreLuciolesBuisson);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            RefillJoueur();
        }
    }

    private void RefillJoueur()
    {
        int currentNbreLucioles = l_compteurLucioles.getNbreLucioles();
        if (currentNbreLucioles != 12)
        {
            animationBuisson.SetTrigger("Ramasse");
            int nbreLuciolesARefill = 12 - currentNbreLucioles;
            if (nbreLuciolesARefill >= i_nbreLuciolesBuisson)
            {
                l_compteurLucioles.ajouterLucioles(i_nbreLuciolesBuisson);
                i_nbreLuciolesBuisson = 0;
                setParticulesBuisson(i_nbreLuciolesBuisson);
            }
            else
            {
                l_compteurLucioles.ajouterLucioles(nbreLuciolesARefill);
                i_nbreLuciolesBuisson -= nbreLuciolesARefill;
                setParticulesBuisson(i_nbreLuciolesBuisson);
            }
        }
    }

    private void RefillBuisson()
    {
        i_nbreLuciolesBuisson = 12;
        setParticulesBuisson(i_nbreLuciolesBuisson);
    }

    private void setParticulesBuisson(int nbreLucioles)
    {
        var luciolesEmission = luciolesParticules.emission;
        luciolesEmission.rateOverTime = nbreLucioles / 4;
    }

}
