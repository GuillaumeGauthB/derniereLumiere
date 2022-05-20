using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusiqueController : MonoBehaviour
{
    /** Script de gestion de la musique du jeu
     * Cree par Jerome Trottier
     * Derniere modification: 20/05/22
     */

    // Methode qui permet de changer la musique qui joue
    public void SetNouvelleMusique(AudioClip musique)
    {
        gameObject.GetComponent<AudioSource>().clip = musique;
        gameObject.GetComponent<AudioSource>().Play();
    }
}
