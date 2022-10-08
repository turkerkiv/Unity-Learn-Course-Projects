using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShot : MonoBehaviour
{
    [SerializeField] float _throwSpeed = 15f;
    [SerializeField] float _throwPreparationTime = 2f;

    GameObject _substance;
    bool _hasSubstance;

    private void OnCollisionEnter(Collision other)
    {
        _hasSubstance = true;

        _substance = other.gameObject;

        Invoke(nameof(ThrowSubstance), 0);
    }

    void ThrowSubstance()
    {
        Rigidbody substanceRb = _substance.GetComponent<Rigidbody>();

        substanceRb.AddForce(Vector3.up * _throwSpeed, ForceMode.Impulse);
    }
}
