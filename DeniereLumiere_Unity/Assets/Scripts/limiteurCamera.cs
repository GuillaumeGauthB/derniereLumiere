using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limiteurCamera : MonoBehaviour
{
    private Collider2D c_collider;

    private float f_limiteOuestLocale;
    private float f_limiteEstLocale;
    private float f_limiteNordLocale;
    private float f_limiteSudLocale;

    private GameObject mainCamera;


    private void Start()
    {
        c_collider = GetComponent<Collider2D>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        f_limiteOuestLocale = c_collider.bounds.center.x - c_collider.bounds.extents.x;
        f_limiteEstLocale = c_collider.bounds.center.x + c_collider.bounds.extents.x;

        f_limiteNordLocale = c_collider.bounds.center.y + c_collider.bounds.extents.y;
        f_limiteSudLocale = c_collider.bounds.center.y - c_collider.bounds.extents.y;
    }

    public void setNouvellesLimitesGlobales()
    {
        mainCamera.GetComponent<CameraController>().setNewLimits(
            f_limiteOuestLocale,
            f_limiteEstLocale,
            f_limiteNordLocale,
            f_limiteSudLocale
        );
    }
}
