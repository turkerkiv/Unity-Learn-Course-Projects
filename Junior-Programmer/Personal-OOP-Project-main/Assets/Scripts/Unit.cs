using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Unit : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] TextMeshPro _capacityTMP;
    
    [SerializeField] int _carryCapacity = 1;

    NavMeshAgent _navMesh;

    bool _isSelected;
    List<BuildingItem> _currentCarry = new List<BuildingItem>();

    bool _isMoving;

    void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
    }

    void Update()
    {
        _capacityTMP.gameObject.transform.rotation = Quaternion.identity;

        SelectUnit();

        PlayAnimation("");

        if (!_isSelected) return;
        SetDestinationToMove();
    }

    void OnCollisionEnter(Collision other)
    {
        Shop shop = other.gameObject.GetComponent<Shop>();

        if (shop != null && _currentCarry.Count < _carryCapacity)
        {
            // ABSTRACTION
            BuyItem(shop);

            return;
        }

        Building building = other.gameObject.GetComponent<Building>();
        if (building != null)
        {
            if (_currentCarry.Count == 0) { return; }

            // ABSTRACTION
            DropItemToBuilding(building);
        }
    }

    void BuyItem(Shop shop)
    {
        _currentCarry.Add(shop.BuildingItem);

        CompanyBudget.Instance.DecreaseBudget(shop.Cost);

        _capacityTMP.text = _currentCarry.Count + " / " + _carryCapacity;
    }

    void DropItemToBuilding(Building building)
    {
        foreach (BuildingItem buildingItem in _currentCarry)
        {
            building.ModifyRecipe(buildingItem, _currentCarry.Count);
        }

        _currentCarry.Clear();
        _capacityTMP.text = _currentCarry.Count + " / " + _carryCapacity;
    }

    void SelectUnit()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isSelected = false;
            _capacityTMP.gameObject.SetActive(false);

            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(mouseRay, out RaycastHit hit);

            Unit unit = hit.transform.GetComponent<Unit>();

            if (unit != null && unit == this)
            {
                _isSelected = true;
                _capacityTMP.gameObject.SetActive(true);
            }
        }
    }

    void SetDestinationToMove()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(mouseRay, out RaycastHit hit))
            {
                Vector3 rightClickPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);

                _navMesh.SetDestination(rightClickPoint);
            }
        }
    }

    protected virtual void PlayAnimation(string movingBoolName)
    {
        //Moving animation
        if (_navMesh.velocity.magnitude < 5f)
        {
            _isMoving = false;
        }
        else
        {
            _isMoving = true;
        }
        _animator.SetBool(movingBoolName, _isMoving);
    }
}
