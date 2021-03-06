using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBossRoom : MonoBehaviour
{
    /** Script principal du premier boss du jeu
     * Cree par Jonathan Mores et Jerome Trottier
     * Derniere modification: 20/05/22
     */

    [Header("Le Boss")]
    public GameObject Boss; // le boss
    [Header("UI")]
    public GameObject panelBossUI; // la vie du boss
    [Header("Musique")]
    public GameObject musiqueController; // le controller de la musique du boss
    public AudioClip musiqueBoss; // la musique du boss
    public AudioClip musiqueIdle; // la musique autre que ccelle du boss

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Quand le joueur entre en contact avec une salle de boss
        if(collision.tag == "Player")
        {
            // Le musique de boss demarre
            musiqueController.GetComponent<MusiqueController>().SetNouvelleMusique(musiqueBoss);
            // Le boss s'active
            Boss.SetActive(true);
            // La barre de vie du boss s'affiche
            panelBossUI.SetActive(true);
            // On detruie l'objet dans 2 secondes
            Destroy(gameObject, 2f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // Quand le joueur quitte la salle
            // On remet la musique de base
            musiqueController.GetComponent<MusiqueController>().SetNouvelleMusique(musiqueIdle);
            // On desactive le panel boss du UI
            panelBossUI.SetActive(false);
        }
    }
}
