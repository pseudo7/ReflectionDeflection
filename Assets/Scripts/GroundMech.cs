using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMech : MonoBehaviour
{
    [SerializeField] Transform healthPivot;
    [SerializeField] Transform healthBar;
    [SerializeField] Transform startBound, endBound;
    [SerializeField] Transform leftBarrel, rightBarrel;
    [SerializeField] GameObject missile;
    [SerializeField] int health = 3;
    [SerializeField] float movingSpeed = 10;
    [SerializeField] float shootingSpeed = 10;
    [SerializeField] float rotateSpeed = 50;
    [SerializeField] float fireRate = 5;

    Coroutine oscillate;
    Transform mainCamTransform;
    float countdown;
    int origHealth;
    bool moving;
    bool switchDir = true;
    bool switchBarrel;

    void Start()
    {
        origHealth = health;
        oscillate = StartCoroutine(Oscillate());
        mainCamTransform = Camera.main.transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Utility.isGameOver)
            return;
        if (collision.collider.CompareTag(Constants.FINISH_TAG) && health > 0)
        {
            if (--health <= 0)
            {
                StopCoroutine(oscillate);
                transform.GetChild(0).gameObject.SetActive(true);
                Destroy(gameObject, 2);
            }
            UpdateHealth(health);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Utility.isGameOver)
            return;
        if (collision.collider.CompareTag(Constants.PLAYER_TAG))
            FireAtRate();
    }

    void FireAtRate()
    {
        if (PseudoCheckpoints.CurrentCheckPoint > 6)
        {
            if (countdown > 1 / fireRate)
                Shoot();
            else countdown += Time.deltaTime;
        }
    }
    void Shoot()
    {
        var spawnedMissile = Instantiate(missile, (switchBarrel = !switchBarrel) ? leftBarrel.position : rightBarrel.position, missile.transform.rotation, transform);
        spawnedMissile.GetComponent<Rigidbody>().AddForce((Utility.mainCameraTransform.position - transform.position + Vector3.down).normalized * shootingSpeed, ForceMode.VelocityChange);
        Destroy(spawnedMissile, 2);
        countdown = 0;
    }

    private void LateUpdate()
    {
        if (Utility.isGameOver)
        {
            StopCoroutine(oscillate);
            return;
        }
        healthBar.rotation = Quaternion.LookRotation(Utility.mainCameraTransform.position - healthBar.position);
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
