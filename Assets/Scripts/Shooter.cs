using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject ballPrefab;
    public int shootingSpeed = 2;

    Transform mainCamTransform;
    Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
        mainCamTransform = mainCam.transform;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Vector3 mouseFar = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.farClipPlane));
            Vector3 mouseNear = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.nearClipPlane));
            Debug.DrawRay(mouseNear, mouseFar - mouseNear, Color.green);
            if (Physics.Raycast(mouseNear, mouseFar - mouseNear, out hit))
                if (hit.collider.CompareTag("Mirror"))
                    Instantiate(ballPrefab, mainCamTransform.position, Quaternion.identity);
            //{
            //Debug.Log(hit.collider.tag);
            //ball.GetComponent<Rigidbody>().AddForce(mainCamTransform.forward * shootingSpeed, ForceMode.VelocityChange);
            //    return;
            //}
        }
    }
}
