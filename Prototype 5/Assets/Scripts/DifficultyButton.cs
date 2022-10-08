using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    [SerializeField] float _difficulty;

    private Button _button;
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SetDifficulty);
    }

    void Update()
    {
        
    }

    void SetDifficulty()
    {
        _gameManager.StartGame(_difficulty);
    }
}
