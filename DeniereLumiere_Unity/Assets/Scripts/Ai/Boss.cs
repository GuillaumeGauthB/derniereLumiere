using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    private GameObject g_Perso;
    private GameObject g_BouledeFeu;
    private GameObject g_RayonFeu;
    private Animator a_AnimBoss; 
    public float bossAttaqueCooldown;
    public float bossRangeClose;

    
    // Start is called before the first frame update
    void Start()
    {
        a_AnimBoss = GetComponent<Animator>();
        g_Perso = GameObject.FindWithTag("Perso");
        g_BouledeFeu = transform.GetChild(0).gameObject;
        g_RayonFeu = transform.GetChild(1).gameObject;
        InvokeRepeating("AttaqueBoss", 2f,bossAttaqueCooldown);
    }

    // Update is called once per frame
    void AttaqueBoss()
    {
        if(g_Perso.transform.position.x > transform.position.x + bossRangeClose)
        {
            int attaqueRandom;
            attaqueRandom = Random.Range(1, 3);
            if(attaqueRandom == 1)
            {
                a_AnimBoss.SetTrigger("FeuHaut");
            }
            else
            {
                a_AnimBoss.SetTrigger("FeuBas");
            }
        }
        else
        {
            a_AnimBoss.SetTrigger("Morsure");
        }
    }

    void InstantanerBouleDeFeu()
    {
       GameObject g_clone = Instantiate(g_BouledeFeu,g_BouledeFeu.transform.position, g_BouledeFeu.transform.rotation);
       g_clone.SetActive(true);
       g_clone.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
       /*g_clone.transform.parent = null;*/
       g_clone.GetComponent<ProjectilesBoss>().enabled = true;  
    }

    void InstantanerRayonFeu()
    {
        GameObject g_clone = Instantiate(g_RayonFeu, g_RayonFeu.transform.position, g_RayonFeu.transform.rotation);
        g_clone.SetActive(true);
    }

}
