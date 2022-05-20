using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limiteurCamera : MonoBehaviour
{
    /**
     * Classe qui permet de gerer les limiteurs de la camera
     * Codeurs : Jerome
     * Derniere modification : 20/05/2022
    */
    private Collider2D c_collider; // Le collider du limiteur

    private float f_limiteOuestLocale; // Limite ouest de limiteur
    private float f_limiteEstLocale; // Limite est du limiteur
    private float f_limiteNordLocale; // Limite nord du limiteur
    private float f_limiteSudLocale; // Limite sud du limiteur

    [Header("Zoom accorde a la camera")]
    public float zoomCamera = 5;

    private GameObject mainCamera;


    private void Start()
    {
        // Lors du chargement de la scene, on associe a chacun des limiteurs quel sont ses limites
        c_collider = GetComponent<Collider2D>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        // Limites dans les axes des X
        f_limiteOuestLocale = c_collider.bounds.center.x - c_collider.bounds.extents.x;
        f_limiteEstLocale = c_collider.bounds.center.x + c_collider.bounds.extents.x;

        // Limites dans les axes des Y
        f_limiteNordLocale = c_collider.bounds.center.y + c_collider.bounds.extents.y;
        f_limiteSudLocale = c_collider.bounds.center.y - c_collider.bounds.extents.y;
    }

    // Methode appele quand on touche un nouveau limiteur qui change les limites de la camera
    public void setNouvellesLimitesGlobales()
    {
        mainCamera.GetComponent<CameraController>().setNewZoom(zoomCamera);
        mainCamera.GetComponent<CameraController>().setNewLimits(
            f_limiteOuestLocale,
            f_limiteEstLocale,
            f_limiteNordLocale,
            f_limiteSudLocale
        );
    }
}
