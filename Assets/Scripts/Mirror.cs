using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    static Transform camTransform;

    private void Start()
    {
        if (!camTransform)
            camTransform = Camera.main.transform;
    }

    private void OnMouseUpAsButton()
    {
        GetComponentInParent<Deflector>().Reflect(Vector3.Angle(camTransform.position - transform.position, -transform.forward));
    }
}