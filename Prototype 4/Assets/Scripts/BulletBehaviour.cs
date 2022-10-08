using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 1f;
    [SerializeField] float _bulletPower = 1f;
    [SerializeField] ParticleSystem _pff;

    public Transform EnemyClone { get; set; }

    private float _timer;
    private GameObject _player;

    void Awake()
    {
        _player = GameObject.Find("Player");
    }

    void Start()
    {
    }

    void Update()
    {
        if (EnemyClone != null)
        {
            Vector3 gap = EnemyClone.position - transform.position;
            transform.position += gap.normalized * _moveSpeed * Time.deltaTime;
            transform.LookAt(EnemyClone);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Instantiate(_pff, transform.position, _pff.transform.rotation);

        //oklar bazen kendine doğru çekiyor o saçma oluyor
        if (other.gameObject.CompareTag("Enemy"))
        {
            Vector3 gap = other.transform.position - _player.transform.position;
            other.rigidbody.AddForce(gap.normalized * _bulletPower, ForceMode.Impulse);

            Destroy(gameObject);
        }

        Destroy(gameObject);
    }
}
