using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
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

    }

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Player" && other.tag == "HealingObject")
        {
            _gameManager.Lives += 1;
            Destroy(other.gameObject);
        }

        if (gameObject.tag == "Player" && (other.tag == "FriendlyAnimal" || other.tag == "EnemyAnimal"))
        {
            _gameManager.Lives -= 1;
            Destroy(other.gameObject);
        }

        if (gameObject.tag == "ThrowingObject")
        {
            Destroy(gameObject);

            if (other.tag == "FriendlyAnimal")
            {
                other.GetComponent<AnimalHunger>().FeedAnimal(1);
            }
        }

        if (gameObject.tag == "OneTap")
        {
            Destroy(gameObject);

            if (other.tag == "FriendlyAnimal")
            {
                other.GetComponent<AnimalHunger>().FeedAnimal(100);
            }
        }
    }
}
