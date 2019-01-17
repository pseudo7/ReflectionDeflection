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

#if UNITY_EDITOR
    [UnityEditor.MenuItem("Pseudo/Capture")]
    public static void Capture()
    {
        ScreenCapture.CaptureScreenshot(string.Format("{0}.png", System.DateTime.Now.Ticks.ToString()));
    }
#endif
}
