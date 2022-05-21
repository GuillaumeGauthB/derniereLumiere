using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonsEnnemis : MonoBehaviour
{
    /** Script qui joue les sons des ennemis
     * Cree par Jonathan Mores
     * Derniere modification: 29/04/22
     */

    // -------- Les sons
    /************/
    public AudioClip son1;
    public AudioClip son2;
    public AudioClip son3;
    /************/

    public void JoueSon1()
    {
        if (son1)
        {
            GetComponent<AudioSource>().PlayOneShot(son1);
        }
    }
    public void JoueSon2()
    {
        if (son2)
        {
            GetComponent<AudioSource>().PlayOneShot(son2);
        }
    }
    public void JoueSon3()
    {
        if (son3)
        {
            GetComponent<AudioSource>().PlayOneShot(son3);
        }
    }
}
