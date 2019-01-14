using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deflector : MonoBehaviour
{
    public TextMesh mirrorText;
    public TextMesh incidentText;
    public Transform mirrorTransform;

    public void Deflect(Vector3 angle)
    {
        mirrorTransform.Rotate(angle, Space.Self);
        mirrorText.text = GetMirrorAngle();
    }

    public void Reflect(float angle)
    {
        incidentText.text = string.Format("Incident: {0}", Mathf.RoundToInt(angle));
    }

    string GetMirrorAngle()
    {
        return string.Format("Mirror: {0}", GetIntAngle());
    }

    int GetIntAngle()
    {
        int angle = Mathf.RoundToInt(mirrorTransform.localEulerAngles.y);
        return angle > 180 ? 360 - angle : angle;
    }
}
