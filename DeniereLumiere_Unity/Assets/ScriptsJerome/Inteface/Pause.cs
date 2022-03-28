using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject menuPause;


    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.P))
        {
            PauserJeu();
        }*/
    }
    public void PauserJeu()
    {
        Time.timeScale = 0;
        menuPause.SetActive(true);

    }
    public void DepauserJeu()
    {
        Time.timeScale = 1;
    }
}
