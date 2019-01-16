using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public static Shooter Instance;

    [SerializeField] Transform player;
    [SerializeField] Transform gun;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] float gunRecoilSpeed = 2f;
    [SerializeField] int shootingSpeed = 2;
    [SerializeField] int destroyDelay = 10;

    readonly Vector3 gunShootPos = new Vector3(0f, -.15f, 0f);
    readonly Vector3 gunRecoilPos = new Vector3(0f, -.3f, -.01f);
    Transform mainCamTransform;
    Vector3 gunOrigPos;
    Camera mainCam;
    bool recoiling;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    private void Start()
    {
        mainCam = Camera.main;
        mainCamTransform = mainCam.transform;
        gunOrigPos = gun.localPosition;
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
                if (recoiling)
                    return;
                StartCoroutine(Recoiling());
                var ball = Instantiate(ballPrefab, Utility.mainCameraTransform.position, Quaternion.identity);
                ball.GetComponent<Ball>().initialVelocity = (hit.transform.position - Utility.mainCameraTransform.position).normalized * shootingSpeed;
                ball.SetActive(true);
                Destroy(ball, destroyDelay);
            }
            else Debug.LogWarning("TAG: " + hit.collider.tag);
        else Debug.Log("Nothing to Shoot");
    }

    IEnumerator Recoiling()
    {
        recoiling = true;

        while (gun.localPosition != gunShootPos)
        {
            gun.localPosition = Vector3.MoveTowards(gun.localPosition, gunShootPos, Time.deltaTime * gunRecoilSpeed * 5);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        while (gun.localPosition != gunRecoilPos)
        {
            gun.localPosition = Vector3.MoveTowards(gun.localPosition, gunRecoilPos, Time.deltaTime * gunRecoilSpeed * 2);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        while (gun.localPosition != gunOrigPos)
        {
            gun.localPosition = Vector3.MoveTowards(gun.localPosition, gunOrigPos, Time.deltaTime * gunRecoilSpeed);
            yield return new WaitForEndOfFrame();
        }

        recoiling = false;
    }
}
