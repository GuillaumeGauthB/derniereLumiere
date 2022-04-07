using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject cible;

    public float decalageX;
    public float decalageY;

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

    private Coroutine transition;

    private Transform t_cam;

    private void Start()
    {
        t_cam = transform;
    }

    private void FixedUpdate()
    {
        suivreJoueur();   
    }

    private void suivreJoueur()
    {
        t_cam.position = new Vector3(
            cible.transform.position.x + decalageX,
            cible.transform.position.y + decalageY,
            t_cam.position.z
        );
        lockCameraInLimits();
    }
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

    public void setNewLimits(float ouest, float est, float nord, float sud)
    {

         if (transition != null ) StopCoroutine(transition);
        Debug.Log("Nouvelles limites!");

        transition = StartCoroutine(transitionZoneCamera(ouest, est, nord, sud));


        
    }
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
