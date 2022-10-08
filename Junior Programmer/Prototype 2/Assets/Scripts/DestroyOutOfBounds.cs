using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    [SerializeField] float _topBound = 20f;
    [SerializeField] float _bottomBound = -5f;
    [SerializeField] float _xRange = 20f;
    GameManager _gameManager;

    void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }
    void Start()
    {
    }

    void Update()
    {
        if (!(transform.position.z < _topBound && transform.position.z > _bottomBound && transform.position.x < _xRange && transform.position.x > -_xRange))
        {
            Destroy(gameObject);

            if (gameObject.tag == "FriendlyAnimal")
            {
                _gameManager.Lives -= 1;
            }
        }
    }
}
