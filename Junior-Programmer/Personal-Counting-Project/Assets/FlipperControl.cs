using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperControl : MonoBehaviour
{
    [SerializeField] KeyCode _controlKey;
    [SerializeField] float _controlForce = 50f;

    HingeJoint _hingeJoint;

    private void Awake()
    {
        _hingeJoint = GetComponentInChildren<HingeJoint>();
    }

    void Update()
    {
        ControlFlipper();
    }

    void ControlFlipper()
    {
        if (Input.GetKeyDown(_controlKey))
        {
            Debug.Log(_controlKey + " pressed");
            _hingeJoint.useSpring = true;
        }
        else if (Input.GetKeyUp(_controlKey))
        {
            _hingeJoint.useSpring = false;
        }
    }
}
