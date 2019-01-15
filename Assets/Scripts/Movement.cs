using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Transform mainCamTransform;
    bool allowStrife = true;

    private void Start()
    {
        mainCamTransform = Camera.main.transform;
        PseudoCheckpoints.Instance.MoveToFirst(transform);
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.UpArrow))
            PseudoCheckpoints.Instance.MoveForward(transform);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            PseudoCheckpoints.Instance.MoveBackward(transform);
#else
        CheckStrife();
        ResetAcc();
#endif
    }


    void CheckStrife()
    {
        if (Vector3.Angle(mainCamTransform.forward, mainCamTransform.parent.forward) < 90)
        {
            if (Input.acceleration.x < -.4f)
                MovePlayerLeft();
            else if (Input.acceleration.x > .4f)
                MovePlayerRight();
        }
        else
        {
            if (Input.acceleration.x < -.4f)
                MovePlayerRight();
            else if (Input.acceleration.x > .4f)
                MovePlayerLeft();
        }
    }

    void MovePlayerLeft()
    {
        if (!allowStrife)
            return;
        PseudoCheckpoints.Instance.MoveForward(transform);
        allowStrife = false;
    }

    void MovePlayerRight()
    {
        if (!allowStrife)
            return;
        PseudoCheckpoints.Instance.MoveBackward(transform);
        allowStrife = false;
    }

    void ResetAcc()
    {
        if (Input.acceleration.x < .4f && Input.acceleration.x > -.4f) allowStrife = true;
    }

}
