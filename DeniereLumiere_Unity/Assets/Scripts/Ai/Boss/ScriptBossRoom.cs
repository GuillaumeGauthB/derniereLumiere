using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBossRoom : MonoBehaviour
{
    [Header("Le Boss")]
    public GameObject Boss;
    [Header("UI")]
    public GameObject panelBossUI;
    [Header("Musique")]
    public GameObject musiqueController;
    public AudioClip musiqueBoss;
    public AudioClip musiqueIdle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            musiqueController.GetComponent<MusiqueController>().SetNouvelleMusique(musiqueBoss);
            Boss.SetActive(true);
            panelBossUI.SetActive(true);
            Destroy(gameObject, 2f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            musiqueController.GetComponent<MusiqueController>().SetNouvelleMusique(musiqueIdle);
            panelBossUI.SetActive(false);
        }
    }
}
