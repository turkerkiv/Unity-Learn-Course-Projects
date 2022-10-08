using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Building : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _recipeTMP;

    Recipe _recipe = new Recipe();

    private void Awake()
    {
        _recipe = new Recipe();
    }

    private void Start()
    {
        GetNewRecipe();
    }

    public void ModifyRecipe(BuildingItem buildingItem, int amount)
    {
        _recipeTMP.text = "";

        for (int i = 0; i < _recipe.Needs.Count; i++)
        {
            if (_recipe.Needs[i] == buildingItem && _recipe.StockCounts[i] > 0)
            {
                _recipe.StockCounts[i] -= amount;
            }

            UpdateRecipeUI(i);
        }

        CheckDoneStatus();
    }

    void UpdateRecipeUI(int index)
    {
        if (_recipeTMP.text != "")
        {
            _recipeTMP.text += "\n";
        }

        _recipeTMP.text += $"{_recipe.Needs[index]} = {_recipe.StockCounts[index]}";
    }

    void CheckDoneStatus()
    {
        int completedItemCount = 0;

        foreach (int stock in _recipe.StockCounts)
        {
            if (stock == 0)
            {
                completedItemCount++;
            }
        }

        if (completedItemCount == _recipe.StockCounts.Count)
        {
            CompanyBudget.Instance.IncreaseBudget(_recipe.TotalCost);
            GetNewRecipe();
        }
    }

    void GetNewRecipe()
    {
        _recipe = new Recipe();
        _recipe.InitializeRecipe();
        ModifyRecipe(new BuildingItem(), 0);
    }

    class Recipe
    {
        Shop[] _shops;

        int _totalCost;

        List<BuildingItem> _needs = new List<BuildingItem>();
        List<int> _stockCounts = new List<int>();

        public int TotalCost { get { return _totalCost; } }
        public List<BuildingItem> Needs { get { return _needs; } }
        public List<int> StockCounts { get { return _stockCounts; } }

        public void InitializeRecipe()
        {
            _shops = FindObjectsOfType<Shop>();

            BuildingItem[] allItemTypes = (BuildingItem[])Enum.GetValues(typeof(BuildingItem));

            //to randomize how many type we need
            int randomItemTypeCount = UnityEngine.Random.Range(1, allItemTypes.Length + 1);

            for (int i = 0; i < randomItemTypeCount; i++)
            {
                //to randomize which types we need
                int randomItemTypeIndex = UnityEngine.Random.Range(0, allItemTypes.Length);

                if (_needs.Contains(allItemTypes[randomItemTypeIndex])) { return; }

                _needs.Add(allItemTypes[randomItemTypeIndex]);

                //to randomize stock count
                int randomStockCount = UnityEngine.Random.Range(1, 4);
                _stockCounts.Add(randomStockCount);

                CalculateReward(allItemTypes[randomItemTypeIndex], randomStockCount);
            }
        }

        void CalculateReward(BuildingItem buildingItem, int stockCount)
        {
            //mesela 2 tür var 15 e 20 e stokları var türü karşılaştır ve maliyetiyle stoğu çarp ve reward a ekle
            foreach (Shop shop in _shops)
            {
                if (buildingItem == shop.BuildingItem)
                {
                    _totalCost += shop.Cost * stockCount;
                }
            }
            Debug.Log("total cost" + _totalCost);
        }
    }
}
