using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CompanyBudget : MonoBehaviour
{
    [SerializeField] int _budgetAtStart = 10000;
    [SerializeField] int _profitPercent = 15;

    TextMeshProUGUI _budgetTMP;

    int _currentBudget;
    public int CurrentBudget
    {
        get { return _currentBudget; }
        set { _currentBudget = value; }
    }

    public static CompanyBudget Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += GetCompanyBudgetTMP;

        if (_currentBudget == 0)
        {
            _currentBudget = _budgetAtStart;
            Debug.Log("yeni başlayan");
        }
    }

    public void DecreaseBudget(int amount)
    {
        _currentBudget -= Mathf.Abs(amount);

        _budgetTMP.text = "Budget: " + _currentBudget;

        Debug.Log("here");
    }

    public void IncreaseBudget(int amount)
    {
        amount = Mathf.Abs(amount);
        amount += amount * _profitPercent / 100;
        _currentBudget += amount;

        _budgetTMP.text = "Budget: " + _currentBudget;
    }

    void GetCompanyBudgetTMP(Scene scene, LoadSceneMode mode)
    {
        GameObject budgetObject = GameObject.FindGameObjectWithTag("CompanyBudget");

        if (budgetObject == null) { return; }

        _budgetTMP = budgetObject.GetComponent<TextMeshProUGUI>();
        _budgetTMP.text = "Budget: " + _currentBudget;
    }
}
