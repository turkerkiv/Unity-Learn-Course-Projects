using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] PlayerController _playerController;
    [SerializeField] GameObject[] _friendlyAnimals;
    [SerializeField] GameObject _enemyAnimal;
    [SerializeField] GameObject _healingObject;
    [SerializeField] float _frAnimalRepeatRate = 1f;
    [SerializeField] float _enAnimalRepeatRate = 1f;
    [SerializeField] float _startDelay = 1f;
    [SerializeField] float _healingObjectRepeatRate = 1f;
    
    public float FrAnimalRepeatRate
    {
        get { return _frAnimalRepeatRate; }
        set { _frAnimalRepeatRate = value; }
    }
    public float StartDelay
    {
        get { return _startDelay; }
        set { _startDelay = value; }
    }

    void Start()
    {
        InvokeRepeating("SpawnFriendlyAnimal", StartDelay, FrAnimalRepeatRate);
        InvokeRepeating("SpawnEnemyAnimal", StartDelay, _enAnimalRepeatRate);
        InvokeRepeating("SpawnHealingObject", StartDelay, _healingObjectRepeatRate);
    }

    void Update()
    {
    }

    void SpawnFriendlyAnimal()
    {
        int _randomAnimal = Random.Range(0, _friendlyAnimals.Length); //Picking random animals
        Vector3 _randomPosition = new Vector3(Random.Range(-_playerController.XRange, _playerController.XRange), 0, 20f); //Getting random position
        Instantiate(_friendlyAnimals[_randomAnimal], _randomPosition, _friendlyAnimals[_randomAnimal].transform.rotation); //Spawning random friendly animals at random position 
    }

    void SpawnEnemyAnimal()
    {
        int negativePositive = Random.Range(-1, 1); //Getting signs
        Vector3 _randomPosition = new Vector3(Mathf.Sign(negativePositive) * 20f, 0, Random.Range(-1.5f, 3f)); //getting random position
        Instantiate(_enemyAnimal, _randomPosition, Quaternion.AngleAxis(Mathf.Sign(negativePositive) * -90, new Vector3(0, 1f, 0))); //Spawning random enemy animals at random position
    }

    void SpawnHealingObject()
    {
        Vector3 _randomPosition = new Vector3(Random.Range(-_playerController.XRange, _playerController.XRange), 0, Random.Range(-1.5f, 3f));
        Instantiate(_healingObject, _randomPosition, _healingObject.transform.rotation);
    }
}
