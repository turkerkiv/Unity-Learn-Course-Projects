using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = 1f;
    float _horizontalInput;
    float _verticalInput;
    void Start()
    {

    }

    void Update()
    {
        //Getting input
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        Rotate();
    }

    void Rotate()
    {
        transform.Rotate(Vector3.down, _horizontalInput * _rotationSpeed * Time.deltaTime);
    }
}
