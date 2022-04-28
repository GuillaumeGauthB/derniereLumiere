using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBossRoom : MonoBehaviour
{
    public GameObject Boss;
    public GameObject panelBossUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Boss.SetActive(true);
            panelBossUI.SetActive(true);
            Destroy(gameObject, 2f);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            panelBossUI.SetActive(false);
        }
    }
}
