using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public static Shooter Instance;

    [SerializeField] GameObject ballPrefab;
    [SerializeField] int shootingSpeed = 2;
    [SerializeField] int destroyDelay = 10;
    Transform mainCamTransform;
    Camera mainCam;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    private void Start()
    {
        mainCam = Camera.main;
        mainCamTransform = mainCam.transform;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Shoot();
    }

    public void Shoot()
    {
        RaycastHit hit;
        Vector3 mouseFar = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.farClipPlane));
        Vector3 mouseNear = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.nearClipPlane));
        Debug.DrawRay(mouseNear, mouseFar - mouseNear, Color.green);
        if (Physics.Raycast(mouseNear, mouseFar - mouseNear, out hit))
            if (hit.collider.CompareTag("Mirror"))
            {
                var ball = Instantiate(ballPrefab, mainCamTransform.position, Quaternion.identity);
                ball.GetComponent<Ball>().initialVelocity = (hit.transform.position - mainCamTransform.position).normalized * shootingSpeed;
                ball.SetActive(true);
                Destroy(ball, destroyDelay);
            }
    }
}
