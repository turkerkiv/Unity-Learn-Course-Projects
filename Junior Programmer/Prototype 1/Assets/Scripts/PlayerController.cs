using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Changeable variables
    [SerializeField] float _moveSpeed = 1f;
    [SerializeField] float _turnSpeed = 1f;
    [SerializeField] string _inputID;

    //Constant variables
    private float _horizontalSpeed;
    private float _verticalSpeed;


    void Start()
    {

    }

    void Update()
    {
        //Getting player inputs
        _horizontalSpeed = Input.GetAxis("Horizontal" + _inputID);
        _verticalSpeed = Input.GetAxis("Vertical" + _inputID);

        //Moving vehicle
        transform.Translate(Vector3.forward * Time.deltaTime * _moveSpeed * _verticalSpeed);
        //Turning vehicle
        transform.Rotate(Vector3.up, _turnSpeed * Time.deltaTime * _horizontalSpeed);
    }
}
