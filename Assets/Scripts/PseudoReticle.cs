using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PseudoReticle : MonoBehaviour
{
    [SerializeField] Image reticleImg;
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
                if (countdown < 1)
                    FillReticle(countdown += Time.deltaTime);
                else
                {
                    buttonPressed = true;
                    var pseudoButton = hit.collider.GetComponent<PseudoButton>();
                    var mirror = hit.collider.GetComponent<Mirror>();
                    if (pseudoButton)
                        PseudoButtonPress(pseudoButton);
                    if (mirror)
                        Shooter.Instance.Shoot();
                }
            }
        }
        else ResetReticle();
    }

    void PseudoButtonPress(PseudoButton pseudoButton)
    {
        pseudoButton.ButtonPress();
    }

    void FillReticle(float val)
    {
        reticleImg.fillAmount = val;
    }

    void ResetReticle()
    {
        reticleImg.fillAmount = 0;
        countdown = 0;
        buttonPressed = false;
    }
}
