using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBossRing : MonoBehaviour
{
    //not used
    [SerializeField] float _rotateSpeedY = 1f;
    private float _yAngle;

    void Start()
    {

    }

    void Update()
    {
        _yAngle += _rotateSpeedY * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, _yAngle, 0);
    }
}
