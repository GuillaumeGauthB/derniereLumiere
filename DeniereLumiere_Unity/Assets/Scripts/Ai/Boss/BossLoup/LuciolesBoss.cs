using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuciolesBoss : MonoBehaviour
{
    /** Script de gestion des lucioles ennemies du boss
     * Créé par Guillaume Gauthier-Benoît
     * Dernière modification: 14/05/22
     */

    private float vitesse = 3; // la vitesse des lucioles
    private GameObject beepo; // la cible des lucioles
    
    void Start()
    {
        beepo = GameObject.Find("Beepo"); // choisir le joueur comme cible
        Invoke("Destruction", 4f); // appeler Destruction() apres 4s
    }

    // Update is called once per frame
    void Update()
    {
        // faire deplacement les lucioles en direction de la cible
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, beepo.transform.position, vitesse * Time.deltaTime);
    }

    // la fonction gerant la destruction des lucioles
    void Destruction()
    {
        // detruire la luciole
        Destroy(gameObject);
    }
}
