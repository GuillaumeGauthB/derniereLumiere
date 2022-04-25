using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauserJeu();
        }
    }
    public void PauserJeu()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);

    }
    public void DepauserJeu()
    {
        Time.timeScale = 1;
    }
}
