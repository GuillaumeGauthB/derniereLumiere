using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PouvoirUI : MonoBehaviour
{
    /**
     * Classe qui permet de gerer les pouvoirs dans le UI
     * Codeurs : Jerome
     * Derniere modification : 20/05/2022
    */
    [Header("Délai entre utilisations du pouvoir")]
    public float delayPouvoir;

    [Header("Coût Lucioles")]
    public int coutUtilisation;
    public GameObject compteurLucioles;

    [Header("Couleurs lueur")]
    public Color couleurPouvoir;
    public Color couleurPouvoirMidPoint;
    public Color couleurPouvoirUtilise;

    private Image i_currentImageCouleurPouvoir;
    private Coroutine c_coroutineAnimLueur;
    private bool b_coroutineEnCours;

    [Header("GameObject de la lueur")]
    public GameObject lueur;

    [Header("Joueur peut ou pas utiliser le pouvoir")]
    public bool peutUtiliserPouvoir;

    private void FixedUpdate()
    {
        if (compteurLucioles.GetComponent<LuciolesCompteurController>().getNbreLucioles() < coutUtilisation) peutUtiliserPouvoir = false;
        else peutUtiliserPouvoir = true;
    }


    private void Start()
    {
        i_currentImageCouleurPouvoir = lueur.GetComponent<Image>();
        resetPouvoir();
    }

    //Fonction permettant activant l'animation des lanternes lors de l'utilisation d'un pouvoir
    public void utiliserPouvoir()
    {
        // Si le joueur a le nombre suffisant de luciole, il peut utiliser le pouvoir
        if (compteurLucioles.GetComponent<LuciolesCompteurController>().getNbreLucioles() >= coutUtilisation)
        {
            
            compteurLucioles.GetComponent<LuciolesCompteurController>().diminuerLucioles(coutUtilisation);
            i_currentImageCouleurPouvoir.color = couleurPouvoirUtilise;
            // Il ne peut pas utiliser le pouvoir tant que le cooldown n'est pas termine
            peutUtiliserPouvoir = false;
            c_coroutineAnimLueur = StartCoroutine(animLueur());
        }
    }

    // Animation de la lueur des lanternes lorsqu'elles sont utilisées
    private IEnumerator animLueur()
    {
        b_coroutineEnCours = true;
        float timeToStart = Time.time;
        while (i_currentImageCouleurPouvoir.color != couleurPouvoirMidPoint)
        {
            i_currentImageCouleurPouvoir.color = Color.Lerp(couleurPouvoirUtilise, couleurPouvoirMidPoint, (Time.time - timeToStart) / delayPouvoir);

            yield return null;
        }

        i_currentImageCouleurPouvoir.color = couleurPouvoir;
        // On peut utiliser le pouvoir a nouveau
        peutUtiliserPouvoir = true;
        // La couroutine est terminee
        b_coroutineEnCours = false;
        yield return new WaitForSeconds(0f);
    }

    // Fonction permettant de reset les lanternes à leurs états initials
    public void resetPouvoir() {
        if (b_coroutineEnCours) StopCoroutine(c_coroutineAnimLueur);
        i_currentImageCouleurPouvoir.color = couleurPouvoir;
        peutUtiliserPouvoir = true;
    }


}
