using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Bullet _bulletPrefab;
    [SerializeField] GameObject _hands;
    [SerializeField] Animator _bowScalingAnimator;
    [SerializeField] float _bulletSpeed = 35f;
    [SerializeField] float _shootDelay = 0.5f;

    float _shootTimer;
    bool _canShoot;
    bool _isShooting;
    public bool IsShooting { get { return _isShooting; } }

    Animator _animator;
    Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_health.IsDead) { return; }
        Shoot();
    }

    void Shoot()
    {
        RunShootTimer();

        if (Input.GetMouseButtonDown(0) && _canShoot)
        {
            //Make player wait for delay
            _canShoot = false;
            _shootTimer = _shootDelay;
            _isShooting = true;

            _animator.SetTrigger("Attack");
            _bowScalingAnimator.SetTrigger("Fire");
        }
    }

    public void FireBullet()
    {
        if (_health.IsDead) { return; }

        //Get direction
        Vector3 shootDirection = (MousePosition3D.Transform.position - _hands.transform.position).normalized;

        //Make bullet look to target
        Quaternion lookRotation = Quaternion.LookRotation(shootDirection);

        //Take instance to get rb
        Bullet instance = Instantiate(_bulletPrefab, _hands.transform.position, lookRotation);
        Rigidbody instanceRb = instance.GetComponent<Rigidbody>();

        if (instanceRb == null) { return; }

        //Fire it toward given direction
        instanceRb.AddForce(shootDirection * _bulletSpeed, ForceMode.Impulse);

        _isShooting = false;
    }

    void RunShootTimer()
    {
        //Run timer and make player can shoot after given period of time passes
        _shootTimer -= Time.deltaTime;

        if (_shootTimer < 0)
        {
            _canShoot = true;
        }
    }
}
