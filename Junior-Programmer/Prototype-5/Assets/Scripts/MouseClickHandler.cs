using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickHandler : MonoBehaviour
{
    TrailRenderer _trail;
    private void Awake()
    {
        _trail = GetComponentInChildren<TrailRenderer>();
    }

    void Update()
    {
        GetMouseClick();
    }

    void GetMouseClick()
    {
        Vector2 _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = _mousePos;

        if (Input.GetMouseButton(0))
        {
            _trail.gameObject.SetActive(true);
        }
        else
        {
            _trail.gameObject.SetActive(false);
        }
    }
}
