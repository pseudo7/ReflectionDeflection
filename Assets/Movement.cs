using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private void Start()
    {
        PseudoCheckpoints.Instance.MoveToFirst(transform);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            PseudoCheckpoints.Instance.MoveForward(transform);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            PseudoCheckpoints.Instance.MoveBackward(transform);
    }
}
