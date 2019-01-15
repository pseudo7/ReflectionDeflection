using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public static Shooter Instance;

    [SerializeField] Transform player;
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

#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Shoot();

    }
#endif

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(player.position, Camera.main.transform.forward, out hit))
            if (hit.collider.CompareTag(Constants.MIRROR_TAG))
            {
                var ball = Instantiate(ballPrefab, mainCamTransform.position, Quaternion.identity);
                ball.GetComponent<Ball>().initialVelocity = (hit.transform.position - mainCamTransform.position).normalized * shootingSpeed;
                ball.SetActive(true);
                Destroy(ball, destroyDelay);
            }
            else Debug.LogWarning("TAG: " + hit.collider.tag);
        else Debug.Log("Nothing to Shoot");
    }
}
