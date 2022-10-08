using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    public GameObject plane;
    private Vector3 offset;

    void Start()
    {
        offset = new Vector3(26.06f, 5.03f, 5.08f);
    }

    void Update()
    {
        transform.position = plane.transform.position + offset;
    }
}
