using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] _obstaclePrefab;
    [SerializeField] float _repeatRate = 2f;
    [SerializeField] TextMeshProUGUI _scoreTMP;
    float _repeatRateAtStart;
    float _boostedRepeatRate = 1f;
    Vector3 _spawnPosition = new Vector3(25f, 1.08f, 0);
    PlayerController _playerController;
    float _timer;
    public int Score { get; set; }
    GameObject _clone;
    bool _isSpawned;
    int _multiplyNum = 1;

    void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }
    void Start()
    {
        _repeatRateAtStart = _repeatRate;
        _boostedRepeatRate = _repeatRateAtStart / 1.25f;
    }

    void Update()
    {
        _timer += Time.deltaTime;

        SpawnObstacle();
        GiveScore();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _repeatRate = _boostedRepeatRate;
            _multiplyNum = 2;
        }
        else
        {
            _repeatRate = _repeatRateAtStart;
            _multiplyNum = 1;
        }
    }

    void SpawnObstacle()
    {
        if (!_playerController.IsGameOver && _timer >= _repeatRate && _playerController.transform.position.x >= 2f)
        {
            int _randomObstacle = Random.Range(0, _obstaclePrefab.Length);
            _clone = Instantiate(_obstaclePrefab[_randomObstacle], _spawnPosition, _obstaclePrefab[_randomObstacle].transform.rotation);
            _timer = 0f;
            _isSpawned = true;
        }
    }

    void GiveScore()
    {
        if (_isSpawned && _clone.transform.position.x < _playerController.transform.position.x)
        {
            Score += 1 * _multiplyNum;
            _scoreTMP.text = "Score: " + Score;
            _isSpawned = false;
        }
    }
}
