using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GestionnaireSceneMort : MonoBehaviour
{
    /** Script de gestion du fade in et out de la scene de mort et de la mort dans niveau1
     * Créé par Guillaume Gauthier-Benoît
     * Dernière modification: 07/04/22
     */
    public GameObject lumiere, // le gameobject de la lumiere dans SceneMort
        fadeOut; // le gameobject du fadeout dans niveau1
    public Text etesMort, // un des textes dans SceneMort
        cliquerSur; // un des textes dans SceneMort
    public Color ori, // couleur originale dans SceneMort
        normal, // couleur normale dans SceneMort
        oriFade, // couleur originale dans niveau1
        normalFade; // couleur normale dans niveau1
    private InputJoueur i_inputJoueur; // le player input du joueur
    private bool coroutineEnCours; // bool pour savoir 
    private float tempsMort; // le temps de mort du personnage

    private void Start()
    {
        ori = new Color(1, 1, 1, 0f); // Set les valeurs des couleurs
        normal = new Color(1, 1, 1, 0.3f);
        oriFade = new Color(0, 0, 0, 0);
        normalFade = new Color(0, 0, 0, 1);
        etesMort.GetComponent<Animator>().enabled = false; //Desactiver les animator des elements dans SceneMort
        cliquerSur.GetComponent<Animator>().enabled = false;
        lumiere.gameObject.SetActive(false);
    }

    // IEnumerator gerant les animations de fade in fade out
    IEnumerator AnimationFadeInFadeOut()
    {
        coroutineEnCours = true;
        // Si le joueur est dans SceneMort...
        if (SceneManager.GetActiveScene().name == "SceneMort")
        {
            // Modifier les couleurs pour fade in jusqu'a temps que la couleur soit la meme que normal
            while (etesMort.GetComponent<Text>().color != normal)
            {
                etesMort.GetComponent<Text>().color = Color.Lerp(ori, normal, (Time.time) / 1f);
                cliquerSur.GetComponent<Text>().color = Color.Lerp(ori, normal, (Time.time) / 1f);
                yield return null;
            }
            // Lorsque les deux couleurs sont les mm, commencer les animations de la scene de mort
            etesMort.GetComponent<Animator>().enabled = true;
            cliquerSur.GetComponent<Animator>().enabled = true;
            lumiere.gameObject.SetActive(true);
        }
        // Lorsque la scene est autre que SceneMort
        else
        {
            tempsMort = Time.time;
            // Modifier les couleurs pour fade in jusqu'a temps que la couleur soit la meme que normal
            while (fadeOut.GetComponent<Image>().color != normalFade)
            {
                fadeOut.GetComponent<Image>().color = Color.Lerp(oriFade, normalFade, (Time.time - tempsMort) / 1f);
                yield return null;
            }
            // Lorsque les deux couleurs sont les mm, charger la scene de mort
            SceneManager.LoadScene("SceneMort");
        }
        coroutineEnCours = false;
    }
    private void Update()
    {
        // Lorsque la coroutine n'est pas en cours et que la lumiere n'est pas activee dans SceneMort OU que la coroutine n'est pas en cours et que le joueur est mort...
        if (SceneManager.GetActiveScene().name == "SceneMort")
        {
            if (!coroutineEnCours && !lumiere.gameObject.active)
            {
                // Commencer la coroutine
                StartCoroutine("AnimationFadeInFadeOut");
            }
        }
        else
        {
            if (!coroutineEnCours && Joueur_Script.mort)
            {
                // Commencer la coroutine
                StartCoroutine("AnimationFadeInFadeOut");
            }
        }
    }
    private void Awake()
    {
        // Activere l'ecoute de sons pour changer de scene
        i_inputJoueur = new InputJoueur();
        i_inputJoueur.Mort.Enable();
        i_inputJoueur.Mort.LoadScene.started += ChangerScene;
    }

    public void ChangerScene(InputAction.CallbackContext context)
    {
        Debug.Log("test");
        // Lorsque le bouton est appuyer, changer de scenes
        if (SceneManager.GetActiveScene().name == "SceneMort")
        {
            Joueur_Script.mort = false;
            SceneManager.LoadScene("Niveau1");
        }
    }
}
