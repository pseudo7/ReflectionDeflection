using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenController : MonoBehaviour
{
    public static ScreenController Instance;

    [SerializeField] Transform screen;
    [SerializeField] TextMesh messageText;
    [SerializeField] float speed = 3;

    bool scaling;

    void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    public void ShowScreen(Color color, string message)
    {
        if (scaling)
            return;
        messageText.text = message;
        messageText.color = color;
        StartCoroutine(ChangeScale(new Vector3(3, 1.5f, 1)));
    }

    public void HideScreen()
    {
        if (scaling)
            return;
        StartCoroutine(ChangeScale(new Vector3(0, 0, 1)));
    }

    public void ShowAndHide(Color color, string message, float delay)
    {
        if (scaling)
            return;
        messageText.text = message;
        messageText.color = color;
        StartCoroutine(ShowingAndHiding(delay));
    }

    IEnumerator ShowingAndHiding(float delay)
    {
        yield return StartCoroutine(ChangeScale(new Vector3(3, 1.5f, 1)));
        yield return new WaitForSecondsRealtime(delay);
        yield return StartCoroutine(ChangeScale(new Vector3(0, 0, 1)));
        RestartScene();
    }

    IEnumerator ChangeScale(Vector3 scale)
    {
        scaling = true;
        while (screen.localScale != scale)
        {
            screen.localScale = Vector3.MoveTowards(screen.localScale, scale, Time.fixedDeltaTime);
            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime * 1 / speed);
        }
        scaling = false;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
