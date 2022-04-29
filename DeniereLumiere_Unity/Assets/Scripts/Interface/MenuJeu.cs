using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuJeu : MonoBehaviour
{
    private Animator a_animator;
    public GameObject cadre;
    public GameObject particules;
    public GameObject texteTitre;
    private void Start()
    {
        a_animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.anyKey)
        {
            ouvrirMenu();
        }
    }
    private void ouvrirMenu()
    {
        a_animator.SetBool("afficherMenu", true);
    }
    public void activerCadre()
    {
        cadre.SetActive(true);
    }
    public void activerParticules()
    {
        particules.SetActive(true);
    }
    public void desactiverTexteTitre()
    {
        texteTitre.SetActive(false);
    }
}
