using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Joueur_Mouvement : MonoBehaviour
{
    public InputMaster controles;

    void Awake()
    {
        controles.Joueur.Saut.performed += ctx => Saut();
    }
}
