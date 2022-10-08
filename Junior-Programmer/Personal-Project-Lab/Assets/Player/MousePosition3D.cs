using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition3D : MonoBehaviour
{
    [SerializeField] Camera _mainCamera;

    //Made static to not drag every time
    static Transform _transform;
    public static Transform Transform { get { return _transform; } }

    private void Awake()
    {
        _transform = this.transform;
    }

    void Update()
    {
        SetPositionToMousePosition();
    }

    void SetPositionToMousePosition()
    {
        //Convert mouse position to ray continuesly
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        //If that ray hits something teleport visual effect to ray's hit point
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            transform.position = raycastHit.point;
        }
    }
}
