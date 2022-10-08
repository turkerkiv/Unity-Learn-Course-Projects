using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalHunger : MonoBehaviour
{
    [SerializeField] Slider _hungerSlider;
    [SerializeField] int _amountToBeFed;
    GameManager _gameManager;
    private int _currentFedAmount = 0;

    void Start()
    {
        _hungerSlider.maxValue = _amountToBeFed;
        _hungerSlider.fillRect.gameObject.SetActive(false);
        _hungerSlider.value = 0;

        _gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {

    }

    public void FeedAnimal(int amount)
    {
        _currentFedAmount += amount;
        _hungerSlider.fillRect.gameObject.SetActive(true);
        _hungerSlider.value = _currentFedAmount;

        if (_currentFedAmount >= _amountToBeFed)
        {
            _gameManager.Score += 1;
            Destroy(gameObject, 0.05f);
        }
    }
}
