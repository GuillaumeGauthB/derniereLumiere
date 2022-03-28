using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class dialogues : MonoBehaviour
{
    public Text textbox; // La boite de texte du jeu
    public GameObject textGameObject; // Le gameObject contenant l'enti�ret�e de la boite de dialogue
    private Coroutine c_textOnGoing; // La couroutine d'imprimation de texte
    public GameObject sourceText; // Le gameObject contenant les lignes que nous voulont imprimer
    int nb=0; // Le num�ro de la ligne qui est imprim�e
    int longueurTexte; // La quantit� totale de ligne qui est imprim�e

    private void Start()
    {
        // Vider la bo�te de dialogue
        textbox.text = "";
    }
    // La couroutine faisant appara�tre les lettres de la ligne � imprimer
    IEnumerator texte(string[] test)
    {
        // On met la longueur du array moins 1 dans longueurTexte pour pouvoir comparer sa valeur avec nb dans le futur
        longueurTexte = test.Length - 1;
        // On s'assure que la textbox est vide
        textbox.text = "";
        
        // Pour faire appara�tre chaque lettre une � la fois, on utilise un foreach pour chacun des charact�res de la ligne � imprimer...
        foreach(char i in test[nb].ToCharArray())
        {
            // ... qu'on ajoute une � la fois dans la zone de texte avec un interval de 0.07 seconde entre chaque lettre
            textbox.text += i;
            yield return new WaitForSeconds(0.07f);
        }
    }
   
    // Fonction permettant d'enclencher la lecture du texte
    public void LireTexte(InputAction.CallbackContext context)
    {
        // Lorsque la fonction est appel�e...
        if (context.performed)
        {
            // D�marrer l'animation pour faire appara�tre la boite de dialogue
            textGameObject.GetComponent<Animator>().SetBool("ouvertureTexte", true);
            // Commencer la coroutine du texte une seconde apr�s le d�but de l'animation
            Invoke("CommencerCoroutine", 1f);
            // Et changer le Action Map du joueur � "LectureTexte," pour l'emp�cher de bouger lors de la  lecture
            GetComponent<PlayerInput>().SwitchCurrentActionMap("LectureTexte");
        }
    }

    // Fonction permettant de skipper l'apparission progressive du texte et de changer de ligne
    public void SkipperTexte(InputAction.CallbackContext context)
    {
        // Lorsque la fonction est appel�e...
        if (context.performed)
        {
            // ... et que le contenu imprim� n'est pas le m�me que celui qui devrait l'�tre...
            if (textbox.text.ToString() != sourceText.GetComponent<ecritureTexte>().texte[nb])
            {
                // Arr�ter la coroutine et faire appara�tre l'enti�ret� du texte
                StopCoroutine(c_textOnGoing);
                textbox.text = sourceText.GetComponent<ecritureTexte>().texte[nb];
            }
            // ... et que le contenu imprim� est le m�me que celui qui devrait l'�tre et que le nombre de la ligne imprim�e est plus petit que la quantit� totale de lignes � imprimer......
            else if (textbox.text.ToString() == sourceText.GetComponent<ecritureTexte>().texte[nb] && nb < longueurTexte)
            {
                // Changer de ligne � imprimer et recommencer la coroutine
                nb++;
                CommencerCoroutine();
            }
            // ... et que le contenu imprim� est le m�me que celui qui devrait l'�tre et que le nombre de la ligne imprim�e est plus grand ou �qual � la longueur totale de ligne � imprimer...
            else if (textbox.text.ToString() == sourceText.GetComponent<ecritureTexte>().texte[nb] && nb >= longueurTexte)
            {
                // Changer l'action map de retour au mouvement
                GetComponent<PlayerInput>().SwitchCurrentActionMap("Texte");
                // R�initialiser le num�ro de la ligne en cours d'impression
                nb = 0;
                // Vider la bo�te de dialogue
                textbox.text = "";
                // Et activer l'animation de fermeture de la bo�te de dialogue
                textGameObject.GetComponent<Animator>().SetBool("ouvertureTexte", false);
            }
        }
    }

    // La fonction commen�ant la coroutine
    void CommencerCoroutine()
    {
        // Commencer la coroutine et la mettre dans c_textOnGoing
        c_textOnGoing = StartCoroutine(texte(sourceText.GetComponent<ecritureTexte>().texte));
    }
}
