using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLoup : MonoBehaviour
{
    bool b_attaqueEnCours;
    int i_attaqueAExecuter;

    [Header("Changement de plateformes")]
    public GameObject[] setPlateformes;
    public GameObject attaqueLuciole;
    private GameObject g_attaqueLuciole_clone;
    public Transform[] plateformes;
    int i_plateformeActivee = 0;
    int i_plateformeActivable;

    [Header("Attaque Saut")]
    public GameObject[] picsGauche,
        picsDroit;

    [Header("Attaque Deplacement")]
    private int i_attaqueDeplacementLocation;
    public GameObject transformationDeplacement;
    private Vector3 v_positionOriginale;
    public Vector3 positionFlipX;
    private bool b_animationEnCours;

    // Start is called before the first frame update
    void Start()
    {
        v_positionOriginale = gameObject.transform.position;
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(2);
        b_attaqueEnCours = false;
        yield return null;
    }
    IEnumerator ChangementPlateforme()
    {
        //yield return new WaitForSeconds(1);
        foreach(GameObject plateformeGroupe in setPlateformes)
        {
            if (plateformeGroupe.activeSelf)
            {
                plateformes = plateformeGroupe.GetComponentsInChildren<Transform>();
                foreach (Transform plateforme in plateformes)
                {
                    if(plateforme.name != "Env1" || plateforme.name != "Env2" || plateforme.name != "Env3" || plateforme.gameObject.layer != 3)
                    {
                        plateforme.GetChild(0).gameObject.SetActive(true);
                    }
                }
                yield return new WaitForSeconds(1.5f);
                foreach (Transform plateforme in plateformes)
                {
                    if (plateforme.name != "Env1" || plateforme.name != "Env2" || plateforme.name != "Env3" || plateforme.gameObject.layer != 3)
                    {
                        plateforme.GetChild(0).gameObject.SetActive(false);
                    }
                }
                plateformeGroupe.SetActive(false);
            }
        }
        i_plateformeActivable = Random.Range(0, 3);
        while(i_plateformeActivable == i_plateformeActivee)
        {
            i_plateformeActivable = Random.Range(0, 3);
        }
        i_plateformeActivee = i_plateformeActivable;
        setPlateformes[i_plateformeActivee].SetActive(true);
        setPlateformes[i_plateformeActivee].gameObject.transform.GetChild(0).gameObject.SetActive(true);
        // Enclencher l'attaque de lucioles
        g_attaqueLuciole_clone = Instantiate(attaqueLuciole, attaqueLuciole.transform.position, attaqueLuciole.transform.rotation);
        g_attaqueLuciole_clone.SetActive(true);
        StartCoroutine("Cooldown");
        yield return null;
    }

    IEnumerator AttaqueSaut()
    {
        gameObject.GetComponent<Animator>().enabled = true;
        b_animationEnCours = true;
        if (GetComponent<SpriteRenderer>().flipX)
        {
            GetComponent<Animator>().SetTrigger("SautDroit");
        }
        else
        {
            GetComponent<Animator>().SetTrigger("Saut");
        }
        Debug.Log(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 0.5f);
        for(int i = 0; i < picsDroit.Length; i++)
        {
            picsDroit[i].gameObject.SetActive(true);
            picsGauche[i].gameObject.SetActive(true);
            picsDroit[i].gameObject.GetComponent<Animator>().SetBool("Pic", true);
            picsGauche[i].gameObject.GetComponent<Animator>().SetBool("Pic", true);
            yield return new WaitForSeconds(0.25f);
        }
        Debug.Log(picsDroit.Length);
        yield return new WaitForSeconds(1);
        for (int i = 0; i < picsDroit.Length; i++)
        {
            picsDroit[i].gameObject.GetComponent<Animator>().SetBool("Pic", false);
            picsGauche[i].gameObject.GetComponent<Animator>().SetBool("Pic", false);
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < picsDroit.Length; i++)
        {
            picsDroit[i].gameObject.SetActive(false);
            picsGauche[i].gameObject.SetActive(false);
        }
        b_animationEnCours = false;
        GetComponent<Animator>().enabled = false;
        StartCoroutine("Cooldown");
        yield return null;
    }

    IEnumerator AttaqueDeplacement()
    {
        gameObject.GetComponent<Animator>().enabled = true;
        b_animationEnCours = true;
        i_attaqueDeplacementLocation = Random.Range(0, 3);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //gameObject.GetComponent<Collider2D>().enabled = false;
        transformationDeplacement.SetActive(true);
        yield return new WaitForSeconds(1f);
        if (GetComponent<SpriteRenderer>().flipX)
        {
            if(i_attaqueDeplacementLocation == 1)
            {
                GetComponent<Animator>().SetTrigger("AttaqueDroit");
            }
            else
            {
                GetComponent<Animator>().SetTrigger("AttaqueDroitHaut");
            }
        }
        else
        {
            if (i_attaqueDeplacementLocation == 1)
            {
                GetComponent<Animator>().SetTrigger("AttaqueGauche");
            }
            else
            {
                GetComponent<Animator>().SetTrigger("AttaqueGaucheHaut");
            }
        }
        yield return new WaitForSeconds(1);
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        /*if (GetComponent<SpriteRenderer>().flipX)
        {
            Debug.Log("flipx");
            gameObject.transform.position = positionFlipX;
        }
        else
        {
            Debug.Log("ori");
            gameObject.transform.position = v_positionOriginale;
        }*/
        b_animationEnCours = false;

        //gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Animator>().enabled = false;
        transformationDeplacement.SetActive(false);
        StartCoroutine("Cooldown");
        yield return null;
    }
    private void FixedUpdate()
    {
        if (!b_animationEnCours)
        {
            if (GetComponent<SpriteRenderer>().flipX)
            {
                Debug.Log("flipx");
                gameObject.transform.localPosition = positionFlipX;
            }
            else
            {
                Debug.Log("ori");
                gameObject.transform.position = v_positionOriginale;
            }
        }

        if (!b_attaqueEnCours)
        {
            b_attaqueEnCours = true;
            i_attaqueAExecuter = Random.Range(1, 4);
            //i_attaqueAExecuter = 2;
            if(i_attaqueAExecuter == 1)
            {
                //ChangementPlateforme();
                StartCoroutine("ChangementPlateforme");
            }
            else if(i_attaqueAExecuter == 2)
            {
                StartCoroutine("AttaqueDeplacement");
            }
            else
            {
                //AttaqueSaut();
                StartCoroutine("AttaqueSaut");
            }
        }
    }

    // Update is called once per frame
    void AttaqueCorpsACorps()
    {

    }
}
