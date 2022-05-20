using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class dialogues : MonoBehaviour
{
    public Text textbox; // La boite de texte du jeu
    public GameObject textGameObject; // Le gameObject contenant l'enti?ret?e de la boite de dialogue
    private Coroutine c_textOnGoing; // La couroutine d'imprimation de texte
    public GameObject sourceText, // Le gameObject contenant les lignes que nous voulont imprimer
        declencherTexteGO; // Le gameObject du texte de declenchement de texte
    int nb = 0; // Le num?ro de la ligne qui est imprim?e
    int longueurTexte; // La quantit? totale de ligne qui est imprim?e
    bool collisionTexte; // savoir si le personnage est en contact avec un objet contenant du texte
    InputJoueur i_inputJoueur; // le player input du joueur
    public bool texteActivee; // savoir si les dialogues sont activees
    public GameObject uiDash,
        uiTir,
        uiTirLuciolesCount,
        uiStun,
        uiDoubleSaut;

    private void Awake()
    {
        i_inputJoueur = new InputJoueur();
        i_inputJoueur.Dialogues.Enable();
        i_inputJoueur.Dialogues.LireTexte.started += LireTexte;
        i_inputJoueur.Dialogues.SkipperTexte.canceled += SkipperTexte;
    }

    private void Start()
    {
        // Vider la bo?te de dialogue
        textbox.text = "";
    }

    private void Update()
    {
        if(Time.timeScale == 0)
        {
            declencherTexteGO.SetActive(false);
        }
    }
    // La couroutine faisant appara?tre les lettres de la ligne ? imprimer
    IEnumerator texte(string[] test)
    {
        // On met la longueur du array moins 1 dans longueurTexte pour pouvoir comparer sa valeur avec nb dans le futur
        longueurTexte = test.Length - 1;
        // On s'assure que la textbox est vide
        textbox.text = "";

        // Pour faire appara?tre chaque lettre une ? la fois, on utilise un foreach pour chacun des charact?res de la ligne ? imprimer...
        foreach (char i in test[nb].ToCharArray())
        {
            // ... qu'on ajoute une ? la fois dans la zone de texte avec un interval de 0.07 seconde entre chaque lettre
            textbox.text += i;
            yield return new WaitForSeconds(0.07f);
        }
    }

    // Fonction permettant d'enclencher la lecture du texte
    public void LireTexte(InputAction.CallbackContext context)
    {
        // Lorsque la fonction est appel?e...
        if (context.started && collisionTexte && Time.timeScale != 0 && !texteActivee)
        {
            declencherTexteGO.SetActive(false);
            texteActivee = true;
            // D?marrer l'animation pour faire appara?tre la boite de dialogue
            textGameObject.GetComponent<Animator>().SetBool("ouvertureTexte", true);
            // Commencer la coroutine du texte une seconde apr?s le d?but de l'animation
            Invoke("CommencerCoroutine", 1f);
            // Et changer le Action Map du joueur ? "LectureTexte," pour l'emp?cher de bouger lors de la  lecture
            GetComponent<PlayerInput>().SwitchCurrentActionMap("Dialogues");
            //Time.timeScale = 0;
        }
    }

    // Fonction permettant de skipper l'apparission progressive du texte et de changer de ligne
    public void SkipperTexte(InputAction.CallbackContext context)
    {

        // Lorsque la fonction est appel?e...
        if (context.canceled && collisionTexte && Time.timeScale != 0)
        {
            // ... et que le contenu imprim? n'est pas le m?me que celui qui devrait l'?tre...
            if (textbox.text.ToString() != sourceText.GetComponent<ecritureTexte>().texte[nb])
            {
                // Arr?ter la coroutine et faire appara?tre l'enti?ret? du texte
                StopCoroutine(c_textOnGoing);
                textbox.text = sourceText.GetComponent<ecritureTexte>().texte[nb];
            }
            // ... et que le contenu imprim? est le m?me que celui qui devrait l'?tre et que le nombre de la ligne imprim?e est plus petit que la quantit? totale de lignes ? imprimer......
            else if (textbox.text.ToString() == sourceText.GetComponent<ecritureTexte>().texte[nb] && nb < longueurTexte)
            {
                // Changer de ligne ? imprimer et recommencer la coroutine
                nb++;
                CommencerCoroutine();
            }
            // ... et que le contenu imprim? est le m?me que celui qui devrait l'?tre et que le nombre de la ligne imprim?e est plus grand ou ?qual ? la longueur totale de ligne ? imprimer...
            else if (textbox.text.ToString() == sourceText.GetComponent<ecritureTexte>().texte[nb] && nb >= longueurTexte)
            {
                // Changer l'action map de retour au mouvement
                GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
                // R?initialiser le num?ro de la ligne en cours d'impression
                nb = 0;
                // Vider la bo?te de dialogue
                textbox.text = "";
                // Et activer l'animation de fermeture de la bo?te de dialogue
                textGameObject.GetComponent<Animator>().SetBool("ouvertureTexte", false);
                //Time.timeScale = 1;
                texteActivee = false;

                if (sourceText.gameObject.name.Contains("ObtenirDoubleSaut") && !Joueur_Script.doubleSautObtenu)
                {
                    // Changer la valeure de la variable
                    Joueur_Script.doubleSautObtenu = true;
                    // Activer le UI du pouvoir
                    uiDoubleSaut.SetActive(true);
                }
                else if (sourceText.gameObject.name.Contains("ObtenirDash") && !Joueur_Script.dashObtenu)
                {
                    // Changer la valeure de la variable
                    Joueur_Script.dashObtenu = true;
                    // Activer le UI du pouvoir
                    uiDash.SetActive(true);
                }
                else if (sourceText.gameObject.name.Contains("ObtenirAttaqueLuciole") && !Joueur_Script.tirObtenu)
                {
                    Debug.Log("supposer activer le ui lucioles");
                    // Changer la valeure de la variable
                    Joueur_Script.tirObtenu = true;
                    // Activer le UI du pouvoir
                    uiTir.SetActive(true);
                    uiTirLuciolesCount.SetActive(true);
                }
                else if (sourceText.gameObject.name.Contains("ObtenirStun") && !Joueur_Script.stunObtenu)
                {
                    // Changer la valeure de la variable
                    Joueur_Script.stunObtenu = true;
                    // Activer le UI du pouvoir
                    uiStun.SetActive(true);
                }

                if (sourceText.gameObject.name.Contains("ObtenirStun") || sourceText.gameObject.name.Contains("ObtenirDoubleSaut") || sourceText.gameObject.name.Contains("ObtenirDash") || sourceText.gameObject.name.Contains("ObtenirAttaqueLuciole"))
                {
                    Destroy(sourceText);
                }
            }
        }
    }

    // La fonction commen?ant la coroutine
    void CommencerCoroutine()
    {
        // Commencer la coroutine et la mettre dans c_textOnGoing
        c_textOnGoing = StartCoroutine(texte(sourceText.GetComponent<ecritureTexte>().texte));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ecritureTexte>())
        {
            declencherTexteGO.SetActive(true);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ecritureTexte>())
        {
            collisionTexte = true;
            sourceText = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ecritureTexte>())
        {
            collisionTexte = false;
            sourceText = null;
            declencherTexteGO.SetActive(false);
        }
    }
}
