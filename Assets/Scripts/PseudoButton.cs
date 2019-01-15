using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PseudoButton : MonoBehaviour
{
    public Vector3 angle;

    private void OnMouseUpAsButton()
    {
        GetComponentInParent<Deflector>().Deflect(angle);
    }

    public void ButtonPress()
    {
        Debug.Log("Command Entered");
        GetComponentInParent<Deflector>().Deflect(angle);
    }
}
