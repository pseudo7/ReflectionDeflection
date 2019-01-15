using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deflector : MonoBehaviour
{
    public TextMesh mirrorText;
    public Transform mirrorTransform;
    public Transform board;

    public void Deflect(Vector3 angle)
    {
        mirrorTransform.Rotate(angle, Space.Self);
        mirrorText.text = GetMirrorAngle();
    }

    string GetMirrorAngle()
    {
        return string.Format("{0}°", GetIntAngle());
    }

    private void LateUpdate()
    {
        board.LookAt(Camera.main.transform);
    }

    int GetIntAngle()
    {
        int angle = Mathf.RoundToInt(mirrorTransform.localEulerAngles.y);
        return angle > 180 ? 360 - angle : angle;
    }
}
