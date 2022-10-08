using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject _focalPoint;
    [SerializeField] float _moveSpeed = 1f;
    [SerializeField] int _powerupLast = 1;
    [SerializeField] float _jumpSpeed = 1f;
    [SerializeField] float _fallSpeed = 1f;
    [SerializeField] float _pBPowerupStrength = 1f;
    [SerializeField] TextMeshProUGUI _endScreen;
    [SerializeField] Button _endButton;

    [SerializeField] GameObject _pBPowerupIndicator;
    [SerializeField] GameObject _rPowerupIndicator;
    [SerializeField] GameObject _sPowerupIndicator;

    [SerializeField] float _hardEnemyStrength = 1f;
    [SerializeField] SpawnManager _spawnManager;
    [SerializeField] GameObject _bullet;
    [SerializeField] float _explosionForce = 1f;
    [SerializeField] float _explosionRadius = 1f;

    public bool IsGameOver { get; set; }

    Rigidbody _playerRb;
    float _verticalInput;
    bool hasPushBackPU;
    bool hasSmashPU;
    bool hasRocketPU;

    private bool _isOnGround;
    private bool _isUsingSmash;

    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_isOnGround)
        {
            _verticalInput = Input.GetAxis("Vertical");
            SpawnBullet();
            Jump();
        }
        Smash();

        _pBPowerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        _sPowerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        _rPowerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        if (transform.position.y < -10f)
        {
            IsGameOver = true;
            _endScreen.gameObject.SetActive(true);
            _endButton.gameObject.SetActive(true);
            _endScreen.text = "You survived " + (_spawnManager.WaveNumber - 1) + " waves";
        }

    }

    void FixedUpdate()
    {
        MoveForward();
    }
    void MoveForward()
    {
        _playerRb.AddForce(_focalPoint.transform.forward * _verticalInput * _moveSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PushBackPU"))
        {
            Destroy(other.gameObject);
            hasPushBackPU = true;
            _pBPowerupIndicator.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine());
        }
        if (other.CompareTag("RocketPU"))
        {
            Destroy(other.gameObject);
            hasRocketPU = true;
            _rPowerupIndicator.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine());
        }
        if (other.CompareTag("SmashPU"))
        {
            Destroy(other.gameObject);
            hasSmashPU = true;
            _sPowerupIndicator.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(_powerupLast);
        hasPushBackPU = false;
        hasRocketPU = false;
        hasSmashPU = false;
        _pBPowerupIndicator.SetActive(false);
        _sPowerupIndicator.SetActive(false);
        _rPowerupIndicator.SetActive(false);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isOnGround = true;

            if (_isUsingSmash)
            {
                _isUsingSmash = false;
                for (int i = 0; i < _spawnManager.EnemyClones.Count; i++)
                {
                    Vector3 gap = _spawnManager.EnemyClones[i].transform.position - transform.position;
                    _spawnManager.EnemyClones[i].GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, transform.position, _explosionRadius, 0f, ForceMode.Impulse);
                }
            }
        }
        if (other.gameObject.CompareTag("Enemy") && hasPushBackPU)
        {
            Rigidbody enemyRb = other.rigidbody;
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position;

            enemyRb.AddForce(awayFromPlayer * _pBPowerupStrength, ForceMode.Impulse);
        }
        if (other.gameObject.CompareTag("HardEnemy"))
        {
            Vector3 awayFromPlayer = transform.position - other.gameObject.transform.position;
            _playerRb.AddForce(awayFromPlayer * _hardEnemyStrength, ForceMode.Impulse);
        }
    }
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isOnGround = false;
        }
    }

    void SpawnBullet()
    {
        if (Input.GetKeyDown(KeyCode.F) && hasRocketPU)
        {
            for (int i = 0; i < _spawnManager.EnemyClones.Count; i++)
            {
                BulletBehaviour bulletClone = Instantiate(_bullet, transform.position, _bullet.transform.rotation).GetComponent<BulletBehaviour>();
                bulletClone.EnemyClone = _spawnManager.EnemyClones[i].transform;
            }
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && hasSmashPU)
        {
            _playerRb.AddForce(Vector3.up * _jumpSpeed, ForceMode.Impulse);
            _isUsingSmash = true;
        }
    }
    void Smash()
    {
        if (transform.position.y > 10f)
        {
            _playerRb.AddForce(Vector3.down * _fallSpeed, ForceMode.Impulse);
        }
    }
}
