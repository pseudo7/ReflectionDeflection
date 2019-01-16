using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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
        if (Utility.isGameOver)
            return;
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.VALUABLE_TAG))
        {
            Debug.Log("Level Finished");
            ScreenController.Instance.ShowAndHide(Color.white, "You WIN!!", 2);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag(Constants.FINISH_TAG))
        {
            Debug.Log("DEAD");
            StopAllCoroutines();
            ScreenController.Instance.ShowAndHide(Color.red, "You are\nDEAD!!", 2);
        }
    }

    void CheckStrife()
    {
        if (Vector3.Angle(Utility.mainCameraTransform.forward, Utility.mainCameraTransform.forward) < 90)
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
