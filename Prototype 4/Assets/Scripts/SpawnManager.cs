using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] _enemyPrefab;
    [SerializeField] GameObject[] _powerupPrefab;
    [SerializeField] GameObject _boss;
    [SerializeField] GameObject[] _babiesOfBoss;
    [SerializeField] PlayerController _playerController;
    [SerializeField] float _rndRange = 1f;
    [SerializeField] int _bossRound = 1;
    [SerializeField] float _babySpawnInterval = 1f;
    [SerializeField] TextMeshProUGUI _waveTmp;

    public List<GameObject> EnemyClones
    {
        get { return _enemyClones; }
        set { _enemyClones = value; }
    }
    public int WaveNumber
    {
        get { return _waveNumber; }
    }
    public bool IsBossAlive { get; set; }

    private List<GameObject> _enemyClones;
    private int _waveNumber = 1;
    private int _enemyCount;
    private float nextSpawn;

    void Start()
    {
        _enemyClones = new List<GameObject>();
        SpawnEnemyWaves(_waveNumber);
        SpawnPowerup();
    }

    void Update()
    {
        _waveTmp.text = "Wave: " + _waveNumber;
        _enemyCount = _enemyClones.Count;
        SpawnBabiesOfBoss();

        if (_enemyCount == 0 && !_playerController.IsGameOver)
        {
            _waveNumber++;
            if (_waveNumber % _bossRound == 0)
            {
                SpawnBoss();
            }
            else
            {
                SpawnEnemyWaves(_waveNumber);
            }
            SpawnPowerup();
        }
    }

    void SpawnEnemyWaves(int enemiesToSpawn)
    {
        for (int i = 1; i <= enemiesToSpawn; i++)
        {
            int randomEnemy = Random.Range(0, _enemyPrefab.Length);
            _enemyClones.Add(Instantiate(_enemyPrefab[randomEnemy], GenerateRandomPosition(2), _enemyPrefab[randomEnemy].transform.rotation));
        }
    }

    void SpawnPowerup()
    {
        int randomPowerUp = Random.Range(0, _powerupPrefab.Length);
        Instantiate(_powerupPrefab[randomPowerUp], GenerateRandomPosition(0.3f), _powerupPrefab[randomPowerUp].transform.rotation);
    }

    void SpawnBoss()
    {
        _enemyClones.Add(Instantiate(_boss, GenerateRandomPosition(2), _boss.transform.rotation));
    }

    void SpawnBabiesOfBoss()
    {
        if (Time.time > nextSpawn && IsBossAlive)
        {
            SpawnPowerup();

            nextSpawn = Time.time + _babySpawnInterval;
            int babyCount = _waveNumber / _bossRound;
            for (int i = 0; i < babyCount; i++)
            {
                int randomBaby = Random.Range(0, _babiesOfBoss.Length);
                _enemyClones.Add(Instantiate(_babiesOfBoss[randomBaby], GenerateRandomPosition(2), _babiesOfBoss[randomBaby].transform.rotation));
            }
        }
    }

    Vector3 GenerateRandomPosition(float posY)
    {
        float randomPosX = Random.Range(-_rndRange, _rndRange);
        float randomPosZ = Random.Range(-_rndRange, _rndRange);
        Vector3 randomPos = new Vector3(randomPosX, posY, randomPosZ);
        return randomPos;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Prototype 4");
    }
}
