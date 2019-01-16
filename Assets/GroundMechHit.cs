using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMechHit : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(Constants.PLAYER_TAG))
            GetComponentInParent<GroundMech>().fire = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag(Constants.PLAYER_TAG))
            GetComponentInParent<GroundMech>().fire = false;
    }
}
