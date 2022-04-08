using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticuleSystemeMenu : MonoBehaviour
{
    public float decalage;
    public float tempsAnimation;

    private Coroutine animationParticule;
    public void setNouvellePosition(float posY)
    {
        if (animationParticule != null) StopCoroutine(animationParticule);
        animationParticule = StartCoroutine(transitionPosition(posY));
    }
    private IEnumerator transitionPosition(float nouvellePos)
    {
        float timeToStart = Time.time;
        Vector3 posDepart = transform.position;
        Vector3 posCible = new Vector3(transform.position.x, nouvellePos + decalage, transform.position.z);
        while (posDepart != posCible)
        {
            posDepart = Vector3.Lerp(posDepart, posCible, (Time.time - timeToStart)/tempsAnimation);
            transform.position = posDepart;
            yield return null;
        }
        yield return new WaitForSeconds(0f);
    }

}
