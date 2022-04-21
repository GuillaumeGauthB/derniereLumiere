using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticuleSystemeUnscaledTime : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale < 0.01f)
        {
            gameObject.GetComponent<ParticleSystem>().Simulate(Time.unscaledDeltaTime, true, false);
        }
    }
}
