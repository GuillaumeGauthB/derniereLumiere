using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticuleSystemeMenu : MonoBehaviour
{
    /** Script de gestion du menu principal du jeu
     * Cree par Jerome Trottier
     * Derniere modification: 14/04/22
     */

    [Header("Decalage au besoin de la position de systeme de particule")]
    public float decalage;

    [Header("Temps de transition entre les changements de position")]
    public float tempsAnimation;

    private Coroutine animationParticule;

    // Methode qui permet de donner une nouvelle position en Y aux particules
    public void setNouvellePosition(float posY)
    {
        if (animationParticule != null) StopCoroutine(animationParticule);
        animationParticule = StartCoroutine(transitionPosition(posY));
    }

    // IEnumerator de la transition entre les changement de positions
    private IEnumerator transitionPosition(float nouvellePos)
    {
        // Variable de temps pas affecter par le timescale (pour le menu pause)
        float timeToStart = Time.realtimeSinceStartup;
        Vector3 posDepart = transform.position;
        Vector3 posCible = new Vector3(transform.position.x, nouvellePos + decalage, transform.position.z);
        // While la position cible n'est pas atteinte on bouge le systeme de particule vers la position cible
        while (posDepart != posCible)
        {
            posDepart = Vector3.Lerp(posDepart, posCible, (Time.realtimeSinceStartup - timeToStart)/tempsAnimation);
            transform.position = posDepart;
            yield return null;
        }
        yield return new WaitForSecondsRealtime(0f);
    }

}