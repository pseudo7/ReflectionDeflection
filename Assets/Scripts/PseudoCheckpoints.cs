using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PseudoCheckpoints : MonoBehaviour
{
    public static PseudoCheckpoints Instance;

    public static int CurrentCheckPoint { get { return currentIndex; } set { currentIndex = value; } }

    public Transform[] checkPoints;
    public float moveSpeed = 10f;
    public float rotateSpeed = 50f;

    static int currentIndex;
    static bool moving;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    public void MoveForward(Transform obj)
    {
        if (moving)
            return;
        Debug.Log("Moving Forward");
        if (currentIndex < checkPoints.Length - 1)
            StartCoroutine(MoveTowards(obj, checkPoints[++currentIndex]));
    }

    public void MoveBackward(Transform obj)
    {
        if (moving)
            return;
        Debug.Log("Moving Backward");
        if (currentIndex > 0)
            StartCoroutine(MoveTowards(obj, checkPoints[--currentIndex]));
    }

    public void MoveToFirst(Transform obj)
    {
        if (moving)
            return;
        StartCoroutine(MoveTowards(obj, checkPoints[0]));
    }

    IEnumerator MoveTowards(Transform obj, Transform checkPoint)
    {
        moving = true;
        Debug.LogWarning("Moving");
        while (obj.position != checkPoint.position)
        {
            obj.position = Vector3.MoveTowards(obj.position, checkPoint.position, Time.fixedDeltaTime * moveSpeed);
            yield return new WaitForEndOfFrame();
        }
        while (obj.rotation.eulerAngles.y != checkPoint.rotation.eulerAngles.y)
        {
            obj.rotation = Quaternion.RotateTowards(obj.rotation, checkPoint.rotation, Time.fixedDeltaTime * rotateSpeed);
            yield return new WaitForEndOfFrame();
        }
        Debug.LogWarning("Done");
        moving = false;
    }
}
