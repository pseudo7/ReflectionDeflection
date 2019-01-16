using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMech : MonoBehaviour
{
    [SerializeField] int health = 3;
    [SerializeField] float movingSpeed = 10;
    [SerializeField] float rotateSpeed = 50;
    [SerializeField] Transform healthPivot;
    [SerializeField] Transform healthBar;
    [SerializeField] Transform startBound, endBound;

    Coroutine oscillate;
    Transform mainCamTransform;
    int origHealth;
    bool moving;
    bool switchDir = true;

    void Start()
    {
        origHealth = health;
        oscillate = StartCoroutine(Oscillate());
        mainCamTransform = Camera.main.transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (health <= 0)
            return;
        if (--health <= 0)
        {
            StopCoroutine(oscillate);
            transform.GetChild(0).gameObject.SetActive(true);
            Destroy(gameObject, 2);
        }
        UpdateHealth(health);
    }

    private void LateUpdate()
    {
        healthBar.rotation = Quaternion.LookRotation(mainCamTransform.position - healthBar.position);
    }

    void UpdateHealth(int h)
    {
        healthPivot.localScale = new Vector3(origHealth - h / (float)(origHealth), 2f, 1f);
    }

    IEnumerator Oscillate()
    {
        while (gameObject.activeInHierarchy)
        {
            yield return StartCoroutine(MoveTowards(transform, switchDir ? startBound : endBound));
            switchDir = !switchDir;
        }
    }

    IEnumerator MoveTowards(Transform obj, Transform checkPoint)
    {
        moving = true;
        while (obj.position != checkPoint.position)
        {
            obj.position = Vector3.MoveTowards(obj.position, checkPoint.position, Time.fixedDeltaTime * movingSpeed);
            yield return new WaitForEndOfFrame();
        }
        while (obj.rotation.eulerAngles.y != checkPoint.rotation.eulerAngles.y)
        {
            obj.rotation = Quaternion.RotateTowards(obj.rotation, checkPoint.rotation, Time.fixedDeltaTime * rotateSpeed);
            yield return new WaitForEndOfFrame();
        }
        moving = false;
    }
}
