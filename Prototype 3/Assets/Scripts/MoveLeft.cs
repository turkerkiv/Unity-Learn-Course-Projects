using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 1f;
    float _boostedMoveSpeed = 1f;
    float _moveSpeedAtStart;
    PlayerController _playerController;

    void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }
    void Start()
    {
        _moveSpeedAtStart = _moveSpeed;
        _boostedMoveSpeed = _moveSpeedAtStart * 1.25f;
    }

    void Update()
    {
        if (!_playerController.IsGameOver && _playerController.transform.position.x >= 2f)
        {
            transform.Translate(Vector3.left * _moveSpeed * Time.deltaTime);
        }
        if (transform.position.x < -10f && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _moveSpeed = _boostedMoveSpeed;
        }
        else
        {
            _moveSpeed = _moveSpeedAtStart;
        }
    }
}
