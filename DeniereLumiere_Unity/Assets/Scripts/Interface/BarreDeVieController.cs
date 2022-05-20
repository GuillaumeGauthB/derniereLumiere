using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarreDeVieController : MonoBehaviour
{
    /**
     * Classe qui permet de gerer les barres de vies
     * Codeurs : Jerome
     * Derniere modification : 1/04/2022
    */
    [Header("Sliders à modifier")]
    public GameObject sliderVieMain;
    public GameObject sliderVieDelayed;

    private Slider c_sliderVie;
    private Slider c_sliderVieDelayed;

    [Header("Vitesses Animations Paramètres")]
    public float vitesseAnimVie = 1f;
    public float animationDelay = 1f;

    
    [Header("Maximum de vie du personnage")]
    public float maxVie;

    // Cherche les components Slider des sliders
    private void Start()
    {
        c_sliderVie = sliderVieMain.GetComponent<Slider>();
        c_sliderVieDelayed = sliderVieDelayed.GetComponent<Slider>();
    }

    // Fonction utilisé pour faire l'animation de l'ajout de vie
    public void soignerBarreDeVie(float vieAjout)
    {
        StartCoroutine(animBarreDeVie(vieAjout, c_sliderVie));
        StartCoroutine(animBarreDeVieDelay(vieAjout, c_sliderVieDelayed, 0f));
    }

    // Fonction utilisé pour faire l'animation de perte de vie 
    public void infligerDegatsBarreDeVie(float vieSoustraite)
    {
        if (vieSoustraite >= 0) vieSoustraite *= -1;
        StartCoroutine(animBarreDeVie(vieSoustraite, c_sliderVie));
        StartCoroutine(animBarreDeVieDelay(vieSoustraite, c_sliderVieDelayed, animationDelay));
    }

    // Coroutines utilisé pour animer la barre de vie principale
    private IEnumerator animBarreDeVie(float vieDifference, Slider sliderAnimer)
    {
        
        float sliderVieDepart = sliderAnimer.value;
        float sliderVieBut = sliderAnimer.value + vieToPourcentage(vieDifference);
        if (sliderVieBut >= 1f) sliderVieBut = 1f;

        float timeToStart = Time.time;
        while (sliderAnimer.value != sliderVieBut)
        {
            sliderAnimer.value = Mathf.Lerp(sliderVieDepart, sliderVieBut, (Time.time - timeToStart) * vitesseAnimVie);

            yield return null;
        }

        yield return new WaitForSeconds(0f);
    }

    // Couroutines utilisé pour animer la barre de vie secondaire (delay)
    private IEnumerator animBarreDeVieDelay(float vieDifference, Slider sliderAnimer, float delay)
    {
        yield return new WaitForSeconds(delay);
        float sliderVieDepart = sliderAnimer.value;

        float sliderVieBut;
        if (delay == 0)
        {
            sliderVieBut = sliderAnimer.value + vieToPourcentage(vieDifference);
        } else
        {
            sliderVieBut = c_sliderVie.value;
        }
        if (sliderVieBut >= 1f) sliderVieBut = 1f;

        float timeToStart = Time.time;
        while (sliderAnimer.value != sliderVieBut)
        {
            sliderAnimer.value = Mathf.Lerp(sliderVieDepart, sliderVieBut, (Time.time - timeToStart) * vitesseAnimVie);

            yield return null;
        }

        yield return new WaitForSeconds(0f);
    }

    // Fonction utilisée pour soigner la barre de vie au maximum
    public void resetBarreDeVie()
    {
        c_sliderVie.value = 1f;
        c_sliderVieDelayed.value = 1f;
    }

    // Fonction utilisée pour set un nouveau maximum à la barre de vie
    public void setMaxBarreDeVie(float nouveauMax)
    { 
        maxVie = nouveauMax;
    }

    // Fonction utilisée pour augmenter d'une valeur le maximum de la barre de vie
    public void augmenterMaxBarreDeVie(float vieAjouter)
    {
        maxVie += vieAjouter;
    }

    // Fonction private pour convertir des floats entiers aux floats des sliders
    private float vieToPourcentage(float vie)
    {
        return vie / maxVie;
    }

}
