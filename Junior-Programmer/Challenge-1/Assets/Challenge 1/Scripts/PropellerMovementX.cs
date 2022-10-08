using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerMovementX : MonoBehaviour
{

    [SerializeField] float _propellerSpeed = 1f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, _propellerSpeed * Time.deltaTime);
    }
}
