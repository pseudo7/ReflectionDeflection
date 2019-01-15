using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PseudoReticle : MonoBehaviour
{
    [SerializeField] Image reticleImg;

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
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit))
        {
            if (hit.collider.CompareTag(Constants.MIRROR_TAG) || hit.collider.CompareTag(Constants.CLICKABLE_TAG))
            {
                if (countdown < 1)
                    FillReticle(countdown += Time.deltaTime);
                else
                {
                    PseudoButtonPress(hit.collider.GetComponent<PseudoButton>());
                }
            }
        }
        else ResetReticle();
    }

    void PseudoButtonPress(PseudoButton pseudoButton)
    {

    }

    void FillReticle(float val)
    {
        reticleImg.fillAmount = val;
    }

    void ResetReticle()
    {
        reticleImg.fillAmount = 0;
        countdown = 0;
    }
}
