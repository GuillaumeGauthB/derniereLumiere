using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class dialogues : MonoBehaviour
{
    public Text textbox; // La boite de texte du jeu
    public GameObject textGameObject; // Le gameObject contenant l'entièretée de la boite de dialogue
    private Coroutine c_textOnGoing; // La couroutine d'imprimation de texte
    public GameObject sourceText; // Le gameObject contenant les lignes que nous voulont imprimer
    int nb=0; // Le numéro de la ligne qui est imprimée
    int longueurTexte; // La quantité totale de ligne qui est imprimée

    private void Start()
    {
        // Vider la boîte de dialogue
        textbox.text = "";
    }
    // La couroutine faisant apparaître les lettres de la ligne à imprimer
    IEnumerator texte(string[] test)
    {
        // On met la longueur du array moins 1 dans longueurTexte pour pouvoir comparer sa valeur avec nb dans le futur
        longueurTexte = test.Length - 1;
        // On s'assure que la textbox est vide
        textbox.text = "";
        
        // Pour faire apparaître chaque lettre une à la fois, on utilise un foreach pour chacun des charactères de la ligne à imprimer...
        foreach(char i in test[nb].ToCharArray())
        {
            // ... qu'on ajoute une à la fois dans la zone de texte avec un interval de 0.07 seconde entre chaque lettre
            textbox.text += i;
            yield return new WaitForSeconds(0.07f);
        }
    }
   
    // Fonction permettant d'enclencher la lecture du texte
    public void LireTexte(InputAction.CallbackContext context)
    {
        // Lorsque la fonction est appelée...
        if (context.performed)
        {
            // Démarrer l'animation pour faire apparaître la boite de dialogue
            textGameObject.GetComponent<Animator>().SetBool("ouvertureTexte", true);
            // Commencer la coroutine du texte une seconde après le début de l'animation
            Invoke("CommencerCoroutine", 1f);
            // Et changer le Action Map du joueur à "LectureTexte," pour l'empêcher de bouger lors de la  lecture
            GetComponent<PlayerInput>().SwitchCurrentActionMap("LectureTexte");
        }
    }

    // Fonction permettant de skipper l'apparission progressive du texte et de changer de ligne
    public void SkipperTexte(InputAction.CallbackContext context)
    {
        // Lorsque la fonction est appelée...
        if (context.performed)
        {
            // ... et que le contenu imprimé n'est pas le même que celui qui devrait l'être...
            if (textbox.text.ToString() != sourceText.GetComponent<ecritureTexte>().texte[nb])
            {
                // Arrêter la coroutine et faire apparaître l'entièreté du texte
                StopCoroutine(c_textOnGoing);
                textbox.text = sourceText.GetComponent<ecritureTexte>().texte[nb];
            }
            // ... et que le contenu imprimé est le même que celui qui devrait l'être et que le nombre de la ligne imprimée est plus petit que la quantité totale de lignes à imprimer......
            else if (textbox.text.ToString() == sourceText.GetComponent<ecritureTexte>().texte[nb] && nb < longueurTexte)
            {
                // Changer de ligne à imprimer et recommencer la coroutine
                nb++;
                CommencerCoroutine();
            }
            // ... et que le contenu imprimé est le même que celui qui devrait l'être et que le nombre de la ligne imprimée est plus grand ou équal à la longueur totale de ligne à imprimer...
            else if (textbox.text.ToString() == sourceText.GetComponent<ecritureTexte>().texte[nb] && nb >= longueurTexte)
            {
                // Changer l'action map de retour au mouvement
                GetComponent<PlayerInput>().SwitchCurrentActionMap("Texte");
                // Réinitialiser le numéro de la ligne en cours d'impression
                nb = 0;
                // Vider la boîte de dialogue
                textbox.text = "";
                // Et activer l'animation de fermeture de la boîte de dialogue
                textGameObject.GetComponent<Animator>().SetBool("ouvertureTexte", false);
            }
        }
    }

    // La fonction commençant la coroutine
    void CommencerCoroutine()
    {
        // Commencer la coroutine et la mettre dans c_textOnGoing
        c_textOnGoing = StartCoroutine(texte(sourceText.GetComponent<ecritureTexte>().texte));
    }
}
