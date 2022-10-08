using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 1f;
    [SerializeField] bool _isBoss;
    Rigidbody _enemyRb;
    GameObject _player;
    SpawnManager _spawnManager;
    

    void Start()
    {
        _enemyRb = GetComponent<Rigidbody>();
        _player = GameObject.Find("Player");
        _spawnManager = FindObjectOfType<SpawnManager>();

        if(_isBoss)
        {
            _spawnManager.IsBossAlive = true;
        }
    }

    void Update()
    {
        DestroyBelowGround();
    }

    void FixedUpdate()
    {
        MoveTowardPlayer();
    }

    void DestroyBelowGround()
    {
        if (transform.position.y < -10f)
        {
            _spawnManager.EnemyClones.Remove(gameObject);

            if(_isBoss)
            {
                _spawnManager.IsBossAlive = false;
            }
            
            Destroy(gameObject);
        }
    }

    void MoveTowardPlayer()
    {
        Vector3 lookDirection = _player.transform.position - transform.position;
        _enemyRb.AddForce(lookDirection.normalized * _moveSpeed);
    }
}
