using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonsEnnemis : MonoBehaviour
{   /************/
    public AudioClip son1;
    public AudioClip son2;
    public AudioClip son3;
    /************/
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
