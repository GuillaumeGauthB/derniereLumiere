using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private GameObject g_Perso;
    private Animator a_AnimBoss; 
    public float bossAttaqueCooldown;
    public float bossRangeClose;

    
    // Start is called before the first frame update
    void Start()
    {
        a_AnimBoss = GetComponent<Animator>();
        g_Perso = GameObject.FindWithTag("Perso");
        InvokeRepeating("AttaqueBoss", 2f,bossAttaqueCooldown);
    }

    // Update is called once per frame
    void AttaqueBoss()
    {
        if(g_Perso.transform.position.x < transform.position.x - bossRangeClose)
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
}
