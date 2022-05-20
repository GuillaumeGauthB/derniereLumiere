using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusiqueController : MonoBehaviour
{
    public void SetNouvelleMusique(AudioClip musique)
    {
        gameObject.GetComponent<AudioSource>().clip = musique;
        gameObject.GetComponent<AudioSource>().Play();
    }
}
