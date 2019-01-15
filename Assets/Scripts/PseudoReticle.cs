using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PseudoReticle : MonoBehaviour
{
    [SerializeField] Image reticleImg;
    [SerializeField] float reticleSpeed = 2f;
    [SerializeField] float maxDistance = 3f;
    Camera mainCam;
    RaycastHit hit;
    float countdown;
    bool buttonPressed;

    void Start()
    {
        FillReticle(0);
        mainCam = Camera.main;
    }

    void Update()
    {
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, maxDistance))
        {
            if (hit.collider.CompareTag(Constants.MIRROR_TAG) || hit.collider.CompareTag(Constants.CLICKABLE_TAG))
            {
                if (buttonPressed)
                    return;
                if (countdown < reticleSpeed)
                    FillReticle(countdown += Time.deltaTime);
                else
                {
                    var pseudoButton = hit.collider.GetComponent<PseudoButton>();
                    if (pseudoButton)
                        PseudoButtonPress(pseudoButton);
                    else Debug.LogWarning("No Pseudo Button");
                    if (hit.collider.CompareTag(Constants.MIRROR_TAG))
                        Shooter.Instance.Shoot();
                    else Debug.LogWarning("No Mirror Component");
                    buttonPressed = true;
                }
            }
            else ResetReticle();
        }
        else ResetReticle();
    }

    void PseudoButtonPress(PseudoButton pseudoButton)
    {
        pseudoButton.ButtonPress();
    }

    void FillReticle(float val)
    {
        reticleImg.fillAmount = val / reticleSpeed;
    }

    void ResetReticle()
    {
        reticleImg.fillAmount = 0;
        countdown = 0;
        buttonPressed = false;
    }
}
