using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LuciolesCompteurController : MonoBehaviour
{
    [Header("String Compteur Lucioles")]
    public TextMeshProUGUI compteurString;

    private const int MINIMUM_LUCIOLES = 0;
    private const int MAXIMUM_LUCIOLES = 12;

    private int i_compteur;

    private void Awake()
    {
        setNbreLucioles(8);
    }
    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ajouterLucioles(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            diminuerLucioles(1);
        }
    }*/

    // Fonction qui permet d'additioner de nouvelles lucioles au nombre déjà établi
    public void ajouterLucioles(int nbreLucioleAjoute)
    {
        compteurStringToInt();
        if (i_compteur + nbreLucioleAjoute > MAXIMUM_LUCIOLES)
        {
            compteurString.text = "x12";
        } else
        {
            compteurString.text = "x" + (i_compteur + nbreLucioleAjoute).ToString();
        }
        
    }

    // Fonction qui permet de soustraire des lucioles au nombre établi
    public void diminuerLucioles(int nbreLucioleEnleve)
    {
        if (nbreLucioleEnleve >= 0) nbreLucioleEnleve *= -1;
        compteurStringToInt();
        if (i_compteur + nbreLucioleEnleve < MINIMUM_LUCIOLES)
        {
            compteurString.text = "x0";
        }
        else
        {
            compteurString.text = "x" + (i_compteur + nbreLucioleEnleve).ToString();
        }
    }

    // Fonction qui permet de d'établir un nouveau nombre de luciole
    public void setNbreLucioles(int nbreLuciole)
    {
        compteurString.text = "x" + nbreLuciole.ToString();
    }

    // Fonction qui permet d'aller cherche le nombre de luciole affiché
    public int getNbreLucioles()
    {
        compteurStringToInt();
        return i_compteur;
    }

    // Fonction qui transforme le string du compteur à la valeur int du compteur
    private void compteurStringToInt()
    {
        string[] splitString = compteurString.text.Split("x");
        i_compteur = int.Parse(splitString[1]);
    }
}
