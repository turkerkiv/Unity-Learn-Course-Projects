using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _verticalSpeed = 1f;
    [SerializeField] float _horizontalSpeed = 1f;
    [SerializeField] float _xRange = 1f;
    [SerializeField] GameObject _throwingObject;
    [SerializeField] GameObject _oneTapObject;
    [SerializeField] float _shootingDelay = 0.30f;
    private float _verticalInput;
    private float _horizontalInput;
    private float _shootingTimeDelta;
    [SerializeField] int _ammo = 1;
    [SerializeField] TextMeshProUGUI _ammoTMP;

    public float XRange
    {
        get { return _xRange; }
        set { _xRange = value; }
    }

    void Start()
    {

    }

    void Update()
    {
        //setting y range
        if (transform.position.z < -1.5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -1.5f);
        }
        if (transform.position.z > 3f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 3f);
        }
        _verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * _verticalInput * _verticalSpeed * Time.deltaTime);

        //setting x range
        if (transform.position.x < -_xRange || transform.position.x > _xRange)
        {
            transform.position = new Vector3(Mathf.Sign(transform.position.x) * _xRange, transform.position.y, transform.position.z);
        }
        _horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * _horizontalInput * _horizontalSpeed * Time.deltaTime);

        _ammoTMP.text = _ammo + ":Special Ammo ";
        ThrowObject();
    }

    void ThrowObject()
    {
        _shootingTimeDelta += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && _shootingTimeDelta > _shootingDelay)
        {
            Instantiate(_throwingObject, transform.position, _throwingObject.transform.rotation);
            _shootingTimeDelta = 0;
        }
        if (_ammo > 0 && Input.GetKeyDown(KeyCode.M))
        {
            Instantiate(_oneTapObject, transform.position, _oneTapObject.transform.rotation);
            _ammo -= 1;
        }
    }
}
