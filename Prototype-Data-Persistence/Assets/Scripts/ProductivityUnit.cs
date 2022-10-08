using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductivityUnit : Unit
{
    [SerializeField] float _productivityMultiplier = 2f;

    ResourcePile m_CurrentPile;

    protected override void BuildingInRange()
    {
        if (m_CurrentPile == null)
        {
            ResourcePile pile = m_Target as ResourcePile;

            if(pile != null)
            {
                m_CurrentPile = pile;
                m_CurrentPile.ProductionSpeed = _productivityMultiplier;
            }
        }
    }

    public override void GoTo(Building target)
    {
        ResetProductivity();
        base.GoTo(target);
    }

    public override void GoTo(Vector3 position)
    {
        ResetProductivity();
        base.GoTo(position);
    }

    void ResetProductivity()
    {
        if(m_CurrentPile != null)
        {
            m_CurrentPile.ProductionSpeed /= _productivityMultiplier;
            m_CurrentPile = null;
        }
    }
}
