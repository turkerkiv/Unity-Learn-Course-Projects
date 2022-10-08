using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _waveTMP;
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] int _firstWaveEnemyCount = 3;
    [SerializeField] int _additionalEnemyPerWave = 1;
    [SerializeField] Vector3 _spawnArea;

    int _currentWave = 1;
    int _currentEnemyCount;
    List<Health> _deadEnemies = new List<Health>();
    public List<Health> DeadEnemies { get { return _deadEnemies; } set { _deadEnemies = value; } }

    private void Awake()
    {
        InstantiateEnemies(_firstWaveEnemyCount);
    }

    private void Start()
    {
        _waveTMP.text = "Current Wave: " + _currentWave;
    }

    private void Update()
    {
        //Wave system and object pooling
        if (_deadEnemies.Count == _currentEnemyCount)
        {
            //instantiating additional enemies to object pool per wave 
            InstantiateEnemies(_additionalEnemyPerWave);

            //Resetting enemies from object pool
            foreach (Health enemy in _deadEnemies)
            {
                enemy.transform.position = GetRandomSpawnPosition();
                enemy.ResetThis();
            }

            _deadEnemies.Clear();
            _currentWave++;
            _waveTMP.text = "Current Wave: " + _currentWave;
        }
    }

    void InstantiateEnemies(int enemyCount)
    {
        //Populating the object pool
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject instance = Instantiate(_enemyPrefab, GetRandomSpawnPosition(), Quaternion.identity, transform);
            _currentEnemyCount++;
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        //Selecting random spawn side which are left, right, bottom
        int side = Random.Range(0, 3);

        Vector3 spawnPoint = Vector3.zero;

        switch (side)
        {
            //Left
            case 0:
                //selecting left side
                spawnPoint.x = -_spawnArea.x;
                //random vertically 
                spawnPoint.z = Random.Range(-_spawnArea.z, _spawnArea.z);
                break;

            //Right
            case 1:
                //selecting right side
                spawnPoint.x = _spawnArea.x;
                //random vertically 
                spawnPoint.z = Random.Range(-_spawnArea.z, _spawnArea.z);
                break;

            //Top
            case 2:
                //random horizontally 
                spawnPoint.x = Random.Range(-_spawnArea.x, _spawnArea.x);
                //selecting top side
                spawnPoint.z = _spawnArea.z;
                break;
        }

        return spawnPoint;
    }
}
