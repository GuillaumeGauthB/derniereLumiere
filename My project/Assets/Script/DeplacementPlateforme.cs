using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* script réalisée par Jonathan Mores
* Modifié 04/21/2021*/
/*script controlant le depacement des plateformes*/
public class DeplacementPlateforme : MonoBehaviour
{
    private bool debutDefilement;
    public float vitesse;
    public GameObject Decor;
    // Start is called before the first frame update
    void Start()
    {
        debutDefilement = false;
        
}

    // Update is called once per frame
    void Update()
    {
        if (debutDefilement == true) /*si debutDefilement est activée on se déplace vers le bas*/
        {
            transform.Translate(0, vitesse, 0);

            
        }

        if (Input.GetKeyDown("w") || Input.GetKeyDown("up")) /*si on détecte un saut on appelle la fonction Defilement Jeu*/
        {
            Invoke("DefilementJeu", 2f);

            
        }
    
       



    }
    void DefilementJeu() /*si la variable debutDefilement n'est pas activé on l'active*/
    {
        if (debutDefilement == false)
        {

            debutDefilement = true;
            Decor.GetComponent<Animator>().SetTrigger("activer");

        }
    }

  
}
