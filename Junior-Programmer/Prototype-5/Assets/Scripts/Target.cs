using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float _minSpeed = 12f;
    [SerializeField] float _maxSpeed = 15f;
    [SerializeField] float _torque = 10f;
    [SerializeField] float _xRange = 4f;
    [SerializeField] float _ySpawnPos = -2f;
    [SerializeField] int _point = 1;
    [SerializeField] ParticleSystem _particleSystem;

    private Rigidbody targetRb;
    private GameManager _gameManager;

    void Awake()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Start()
    {
        targetRb = GetComponent<Rigidbody>();

        transform.position = RandomPos();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), ForceMode.Impulse);
    }

    void Update()
    {

    }

    void OnMouseDown()
    {
        if (_gameManager.IsGameActive)
        {
            Destroy(gameObject);
            _gameManager.UpdateScore(_point);
            Instantiate(_particleSystem, transform.position, _particleSystem.transform.rotation);
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Trail") && _gameManager.IsGameActive)
        {
            Destroy(gameObject);
            _gameManager.UpdateScore(_point);
            Instantiate(_particleSystem, transform.position, _particleSystem.transform.rotation);
            return;
        }

        Destroy(gameObject);

        if (gameObject.CompareTag("Good"))
        {
            _gameManager.DecreaseHealth();
        }
    }

    Vector3 RandomPos()
    {
        return new Vector2(Random.Range(-_xRange, _xRange), _ySpawnPos);
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(_minSpeed, _maxSpeed);
    }

    Vector3 RandomTorque()
    {
        return new Vector3(Random.Range(-_torque, _torque), Random.Range(-_torque, _torque), Random.Range(-_torque, _torque));
    }

}
