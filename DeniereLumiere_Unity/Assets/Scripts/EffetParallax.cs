using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetParallax : MonoBehaviour
{
    [Header("Cible à suivre par le backgound")]
    public GameObject cible;
    [Header("Position originale du background")]
    private Vector3 v_posOrigin;
    [Header("Modificateurs de decalage pour accelerer ou ralentir la poursuite")]
    public float decalageXModifier;
    public float decalageYModifier;
    [Header("Decalage réel entre la position originale du background et sa cible")]
    private float f_decalageX;
    private float f_decalageY;

    private void Start()
    {
        // Associer la variable posOrigin à la position du background au chargement de la scène
        v_posOrigin = transform.position;
    }

    private void Update()
    {
        //Avoir le decalage entre la posistion orignale et la position de la cible en X et Y
        f_decalageX = getDistanceEntre2PointsSurUnAxe(v_posOrigin.x, cible.transform.position.x);
        f_decalageY = getDistanceEntre2PointsSurUnAxe(v_posOrigin.y, cible.transform.position.y);

        // Associer la nouvelle position au background en fonction sa position originale,
        // la position de la cible, le decalage ainsi que le modificateur de decalage
        transform.position = new Vector3(
            v_posOrigin.x + cible.transform.position.x + f_decalageX * decalageXModifier,
            v_posOrigin.y + cible.transform.position.y + f_decalageY * decalageYModifier,
            transform.position.z
        );
    }
    // Fonction qui retourne la distance entre 2 points sur un seul axe (X ou Y)
    private float getDistanceEntre2PointsSurUnAxe(float origin, float target)
    {
        return target - origin;
    }
}
