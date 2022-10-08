using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _bulletLifetime = 2f;
    [SerializeField] int _damage = 15;

    Rigidbody _rb;
    bool _isBlunt;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        //Destroy after given time
        Destroy(gameObject, _bulletLifetime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_isBlunt) { return; }

        if (other.gameObject.GetComponentInParent<Health>() != null)
        {
            //if anything has health reduce and destroy this
            other.gameObject.GetComponentInParent<Health>().ModifyHealth(-_damage);

            Destroy(gameObject);
        }

        //Freeze after hits something
        _rb.constraints = RigidbodyConstraints.FreezeAll;
        _isBlunt = true;
    }
}
