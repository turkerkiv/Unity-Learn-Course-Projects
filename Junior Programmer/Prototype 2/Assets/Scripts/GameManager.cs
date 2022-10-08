using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] SpawnManager _spawnManager;
    [SerializeField] int _lives;
    [SerializeField] TextMeshProUGUI _livesTMP;
    [SerializeField] TextMeshProUGUI _scoreTMP;
    [SerializeField] TextMeshProUGUI _endingTMP;
    [SerializeField] TextMeshProUGUI _timeTMP;
    [SerializeField] TextMeshProUGUI _levelTMP;

    private float _realTime;
    private int _levelUP;

    public int Lives
    {
        get { return _lives; }
        set { _lives = value; }
    }
    public int Score { get; set; }

    void Start()
    {
        Lives = 2;
    }

    void Update()
    {
        _realTime += Time.deltaTime;
        _levelUP = (int)_realTime;


        if (_levelUP == 10)
        {
            _spawnManager.FrAnimalRepeatRate = 1.5f;
            _spawnManager.CancelInvoke("SpawnFriendlyAnimal");
            _spawnManager.InvokeRepeating("SpawnFriendlyAnimal", _spawnManager.StartDelay, _spawnManager.FrAnimalRepeatRate);
            _levelUP += 1;
            _levelTMP.text = "Level 1";
        }
        else if (_levelUP == 30)
        {
            _spawnManager.FrAnimalRepeatRate = 1.3f;
            _spawnManager.CancelInvoke("SpawnFriendlyAnimal");
            _spawnManager.InvokeRepeating("SpawnFriendlyAnimal", _spawnManager.StartDelay, _spawnManager.FrAnimalRepeatRate);
            _levelUP += 1;
            _levelTMP.text = "Level 2";
        }
        else if (_levelUP == 60)
        {
            _spawnManager.FrAnimalRepeatRate = 1f;
            _spawnManager.CancelInvoke("SpawnFriendlyAnimal");
            _spawnManager.InvokeRepeating("SpawnFriendlyAnimal", _spawnManager.StartDelay, _spawnManager.FrAnimalRepeatRate);
            _levelUP += 1;
            _levelTMP.text = "Level 3";
        }
        else if (_levelUP == 90)
        {
            _spawnManager.FrAnimalRepeatRate = 0.8f;
            _spawnManager.CancelInvoke("SpawnFriendlyAnimal");
            _spawnManager.InvokeRepeating("SpawnFriendlyAnimal", _spawnManager.StartDelay, _spawnManager.FrAnimalRepeatRate);
            _spawnManager.CancelInvoke("SpawnHealingObject");
            _levelUP += 1;
            _levelTMP.text = "Level 4";
        }

        _livesTMP.text = "Lives:" + _lives;
        _scoreTMP.text = Score + ":Score";

        if (Lives == 0)
        {
            Destroy(_player);
            _endingTMP.gameObject.SetActive(true);
            _timeTMP.text = "Time lived\n" + _levelUP + " sec";
            _timeTMP.gameObject.SetActive(true);
            Lives -= 1;
        }
    }
}
