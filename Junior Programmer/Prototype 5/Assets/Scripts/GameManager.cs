using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _targets;
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _gameOverText;
    [SerializeField] TextMeshProUGUI _livesText;
    [SerializeField] Button _restartButton;
    [SerializeField] GameObject _titleScreen;
    [SerializeField] GameObject _lightBackground;
    [SerializeField] GameObject _darkBackground;

    [SerializeField] Canvas _pauseCanvas;
    [SerializeField] KeyCode _pauseKey = KeyCode.Escape;

    [SerializeField] int _lives = 3;

    public bool IsGameActive { get; set; }


    private int _score;
    private float _spawnRate = 1f;

    private void Update()
    {
        PauseGame();
    }

    IEnumerator SpawnTarget()
    {
        while (IsGameActive)
        {
            yield return new WaitForSeconds(_spawnRate);
            int index = Random.Range(0, _targets.Count);
            Instantiate(_targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        _scoreText.text = "Health: " + _score;

        if (_score < 0)
        {
            _gameOverText.gameObject.SetActive(true);
            _restartButton.gameObject.SetActive(true);
            IsGameActive = false;
        }
    }

    public void DecreaseHealth()
    {
        _lives--;
        _livesText.text = $"{_lives} :Lives";

        if (_lives <= 0)
        {
            _gameOverText.gameObject.SetActive(true);
            _restartButton.gameObject.SetActive(true);
            IsGameActive = false;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(float difficulty)
    {
        IsGameActive = true;
        _score = 0;
        _spawnRate /= difficulty;

        StartCoroutine(SpawnTarget());
        UpdateScore(0);

        _livesText.text = $"{_lives} :Lives";

        _titleScreen.SetActive(false);
    }

    public void SelectLightBackground(bool isTrue)
    {
        if (isTrue)
        {
            _lightBackground.SetActive(true);
            _darkBackground.SetActive(false);
        }
        else
        {
            _lightBackground.SetActive(false);
            _darkBackground.SetActive(true);
        }
    }

    void PauseGame()
    {
        if (Input.GetKeyDown(_pauseKey) && IsGameActive)
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                _pauseCanvas.enabled = true;
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                _pauseCanvas.enabled = false;
            }
        }
    }
}
