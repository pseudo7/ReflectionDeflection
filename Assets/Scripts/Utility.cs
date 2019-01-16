using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
    public static bool isGameOver;
    public static Transform mainCameraTransform;

    private void Start()
    {
        PseudoCheckpoints.CurrentCheckPoint = 0;
        mainCameraTransform = Camera.main.transform;
        isGameOver = false;
    }
}
