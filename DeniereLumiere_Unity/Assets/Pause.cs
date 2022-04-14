using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    public GameObject particules;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauserJeu();
        }
    }
    public void PauserJeu()
    {
        GetComponent<Animator>().SetBool("EnPause", true);
        Time.timeScale = 0;

    }
    public void DepauserJeu()
    {
        GetComponent<Animator>().SetBool("EnPause", false);
        Time.timeScale = 1;
    }
    public void activerParticules()
    {
        particules.SetActive(true);
    }
    public void desactiverParticules()
    {
        particules.SetActive(false);
    }
}
