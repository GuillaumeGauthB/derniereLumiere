using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PouvoirUI : MonoBehaviour
{

    [Header("Délai entre utilisations du pouvoir")]
    public float delayPouvoir;

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

    //Debug Inputs
    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            utiliserPouvoir();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            resetPouvoir();
        }
    }*/


    private void Start()
    {
        i_currentImageCouleurPouvoir = lueur.GetComponent<Image>();
        resetPouvoir();
    }

    //Fonction permettant activant l'animation des lanternes lors de l'utilisation d'un pouvoir
    public void utiliserPouvoir()
    {
        i_currentImageCouleurPouvoir.color = couleurPouvoirUtilise;
        peutUtiliserPouvoir = false;
        c_coroutineAnimLueur = StartCoroutine(animLueur());

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
        peutUtiliserPouvoir = true;
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
