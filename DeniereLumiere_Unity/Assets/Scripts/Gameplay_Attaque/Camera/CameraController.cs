using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /**
     * Classe qui permet de gerer les limites de la camera et de comment elle agie
     * Codeurs : Jerome
     * Derniere modification : 20/05/2022
    */

    public GameObject cible; // Cible que la camera doit suivre

    // Decalage au besoin de la camera
    public float decalageX; 
    public float decalageY;

    // Temps entre les changement de zone de la camera
    public float tempsChangementZoneCamera;

    [Header("Limites du collider sur l'axe des X")]
    public float limiteOuestCollider;
    public float limiteEstCollider;

    [Header("Limites du collider sur l'axe des Y")]
    public float limiteNordCollider;
    public float limiteSudCollider;


    [Header("Limites de la camera sur l'axe des X")]
    private float f_limiteOuest;
    private float f_limiteEst;

    [Header("Limites de la camera sur l'axe des Y")]
    private float f_limiteNord;
    private float f_limiteSud;

    private Coroutine transitionZone; // Coroutine de la transition de la camera

    private Transform t_cam; // Position de la camera

    private void Awake()
    {
        // au reveil, on met la camera sur la cible
        t_cam = transform;
        t_cam.position = new Vector3(
           cible.transform.position.x,
           cible.transform.position.y,
           t_cam.position.z
       );
    }

    // La camera suit constamment le joueur
    private void FixedUpdate()
    {
        suivreJoueur();   
    }

    private void suivreJoueur()
    {
        // Pour suivre le joueur on associe sa position avec celle de la cible
        t_cam.position = new Vector3(
            cible.transform.position.x + decalageX,
            cible.transform.position.y + decalageY,
            t_cam.position.z
        );
        // Ensuite, on ajuste la position de la camera avec les limites accordees
        lockCameraInLimits();
    }
    
    // Methode qui ajuste la position de la camera selon les limites accordes par les limiteurCamera
    private void lockCameraInLimits()
    {
        //Limiter la camera dans les axes des X
        if (t_cam.position.x < f_limiteOuest)
        {
            t_cam.position = new Vector3(f_limiteOuest, t_cam.position.y, t_cam.position.z);
        }
        else if (t_cam.position.x > f_limiteEst)
        {
            t_cam.position = new Vector3(f_limiteEst, t_cam.position.y, t_cam.position.z);
        }
        //Limiter la camera dans les axes des Y

        if (t_cam.position.y < f_limiteSud)
        {
            t_cam.position = new Vector3(t_cam.position.x, f_limiteSud, t_cam.position.z);
        }
        else if (t_cam.position.y > f_limiteNord)
        {
            t_cam.position = new Vector3(t_cam.position.x, f_limiteNord, t_cam.position.z);
        }
    }

    // Methode qui permet d'ajuste le zoom de la camera
    public void setNewZoom(float zoom)
    {
        gameObject.GetComponent<Camera>().orthographicSize = zoom;
    }

    // Methode qui permet de mettre de nouvelles limites a la camera quand elle change de zone
    public void setNewLimits(float ouest, float est, float nord, float sud)
    {
        if (transitionZone != null ) StopCoroutine(transitionZone);
        transitionZone = StartCoroutine(transitionZoneCamera(ouest, est, nord, sud));
    }

    // Transition entre les differentes zone de camera
    public IEnumerator transitionZoneCamera(float ouest, float est, float nord, float sud)
    {
        float timeToStart = Time.time;
        float height = 2f * GetComponent<Camera>().orthographicSize;
        float width = height * GetComponent<Camera>().aspect;
        while (limiteOuestCollider != ouest && limiteEstCollider != est && limiteNordCollider != nord && limiteSudCollider != sud)
        {
            limiteOuestCollider = Mathf.Lerp(limiteOuestCollider, ouest, (Time.time - timeToStart) / tempsChangementZoneCamera);
            f_limiteOuest = limiteOuestCollider + width / 2;
            limiteEstCollider = Mathf.Lerp(limiteEstCollider, est, (Time.time - timeToStart) / tempsChangementZoneCamera);
            f_limiteEst = limiteEstCollider - width / 2;
            limiteNordCollider = Mathf.Lerp(limiteNordCollider, nord, (Time.time - timeToStart) / tempsChangementZoneCamera);
            f_limiteNord = limiteNordCollider;
            limiteSudCollider = Mathf.Lerp(limiteSudCollider, sud, (Time.time - timeToStart) / tempsChangementZoneCamera);
            f_limiteSud = limiteSudCollider + height / 2;
            yield return null;
        }
        limiteOuestCollider = ouest;
        limiteEstCollider = est;
        limiteNordCollider = nord;
        limiteSudCollider = sud;

        f_limiteOuest = limiteOuestCollider + width / 2;
        f_limiteEst = limiteEstCollider - width / 2;
        f_limiteNord = limiteNordCollider;
        f_limiteSud = limiteSudCollider + height / 2;
        yield return new WaitForSeconds(0f);
    }
}
