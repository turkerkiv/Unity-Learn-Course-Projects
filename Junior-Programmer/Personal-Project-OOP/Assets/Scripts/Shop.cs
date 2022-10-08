using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] BuildingItem _buildingItem;
    [SerializeField] TextMeshPro _BuildingItemTMP;
    [SerializeField] int _cost = 1000;

    public BuildingItem BuildingItem { get { return _buildingItem; } }
    public int Cost { get { return -Mathf.Abs(_cost); } }

    private void Start()
    {
        _BuildingItemTMP.text = _buildingItem + " " + Mathf.Abs(_cost);
    }
}