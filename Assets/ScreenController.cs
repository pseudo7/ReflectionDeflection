using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    public static ScreenController Instance;

    [SerializeField] Transform screen;
    [SerializeField] TextMesh messageText;

    bool scaling;

    void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    public void ShowScreen(string message)
    {
        messageText.text = message;
        StartCoroutine(ChangeScale(new Vector3(3, 1.5f, 1)));
    }

    public void HideScreen()
    {
        StartCoroutine(ChangeScale(new Vector3(0, 0, 1)));
    }

    public void ShowAndHide(string message, float delay)
    {
        messageText.text = message;
        StartCoroutine(ShowingAndHiding(delay));
    }

    IEnumerator ShowingAndHiding(float delay)
    {
        yield return StartCoroutine(ChangeScale(new Vector3(3, 1.5f, 1)));
        yield return new WaitForSecondsRealtime(delay);
        StartCoroutine(ChangeScale(new Vector3(0, 0, 1)));
    }

    IEnumerator ChangeScale(Vector3 scale)
    {
        scaling = true;
        while (screen.localScale != scale)
        {
            screen.localScale = Vector3.MoveTowards(screen.localScale, scale, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        scaling = false;
    }
}
