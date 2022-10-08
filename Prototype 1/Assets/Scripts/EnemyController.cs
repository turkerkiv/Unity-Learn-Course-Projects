using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float _busSpeed = 1f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.forward * _busSpeed * Time.deltaTime);
    }
}
