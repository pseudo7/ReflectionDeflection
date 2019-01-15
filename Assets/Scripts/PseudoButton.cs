using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PseudoButton : MonoBehaviour
{
    public PseudoButtonType buttonType;
    public Vector3 angle;

    private void OnMouseUpAsButton()
    {
        GetComponentInParent<Deflector>().Deflect(angle);
    }

    public void ButtonPress()
    {
        switch (buttonType)
        {
            case PseudoButtonType.RotationButton:
                GetComponentInParent<Deflector>().Deflect(angle);
                break;
            case PseudoButtonType.MovementButton:
                break;
        }
    }
}
public enum PseudoButtonType
{
    RotationButton, MovementButton
}