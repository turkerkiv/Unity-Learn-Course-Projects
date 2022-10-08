using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        Destroy(gameObject, 0.1f);
    }
}
