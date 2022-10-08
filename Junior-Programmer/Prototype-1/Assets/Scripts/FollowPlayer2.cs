using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer2 : MonoBehaviour
{
    //Getting player object
    [SerializeField] GameObject _player;

    //Offset variable
    private Vector3 _behindCameraOffset;
    private Vector3 _frontCameraOffset;

    private bool _isCameraFront;

    void Start()
    {
        //Gave camera some offset depend on condition
        _behindCameraOffset = new Vector3(0, 5.93f, -9.43f);
        _frontCameraOffset = new Vector3(0, 2.13f, 0.73f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _isCameraFront = !_isCameraFront;
        }
    }
    void LateUpdate()
    {
        //Made camera follow the player
        if (!_isCameraFront)
        {
            transform.position = _player.transform.position + _behindCameraOffset;
        }
        else
        {
            transform.position = _player.transform.position + _frontCameraOffset;
        }
    }
}
