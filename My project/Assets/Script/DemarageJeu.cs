using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DemarageJeu : MonoBehaviour
/* script réalisée par Jonathan Mores
* Modifié 04/21/2021*/
{ /*scipt controlant le changement de Scene et les textes */

    public Text textInstructions; /* texte qui clignotera*/
    public Text textPoint; /* texte qui affiche les point*/
    public string SceneACharge; /* variable ou on écris la scene que l'on veut charger, cela nous evite de creer plein de script*/

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DisparaitApparait", 0, 0.7f);

        if (textPoint != null) /*si on a un Texte qui montre les points on le montre en allant chercher la variable statique de points*/
        {
            textPoint.text = "Vous avez " + DeplacementBoy.RecuperePoint() + " points"; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) /*si on appuie sur espace on charge la prochaine scene*/
        {
            Invoke("JoueSceneJeu", 0f);
        }


    }


    void JoueSceneJeu() /*on charge la scene a charger*/
    {
        SceneManager.LoadScene(SceneACharge);
    }


    void DisparaitApparait() /* disparait et apparait le texte*/
    {
        if (textInstructions.gameObject.activeSelf == false)
        {
            textInstructions.gameObject.SetActive(true);
            
        }
        else if (textInstructions.gameObject.activeSelf == true)
        {
            textInstructions.gameObject.SetActive(false);
            
        }

    }




}