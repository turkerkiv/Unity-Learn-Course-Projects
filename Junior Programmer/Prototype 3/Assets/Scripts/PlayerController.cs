using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _jumpForce = 1f;
    [SerializeField] float _gravityModifier = 1f;
    [SerializeField] ParticleSystem _particleExplosion;
    [SerializeField] ParticleSystem _particleDirt;
    [SerializeField] AudioClip _jumpSound;
    [SerializeField] AudioClip _crashSound;
    [SerializeField] GameObject _endScreen;
    [SerializeField] TextMeshProUGUI _endScore;
    [SerializeField] SpawnManager _spawnManager;
    private AudioSource _playerAudioSource;
    private Rigidbody _playerRigidbody;
    private Animator _playerAnimator;
    private bool _isOnGround;
    private bool _isGameOver;
    private bool _isDoubleUsed;
    private float _animatorSpeedAtStart;
    float _boostedAnimatorSpeed = 1f;

    public bool IsGameOver
    {
        get { return _isGameOver; }
        set { _isGameOver = value; }
    }

    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerAnimator = GetComponent<Animator>();
        _playerAudioSource = GetComponent<AudioSource>();
        Physics.gravity *= _gravityModifier;
        _animatorSpeedAtStart = _playerAnimator.speed;
        _boostedAnimatorSpeed = _animatorSpeedAtStart * 1.25f;
    }

    void Update()
    {
        if (transform.position.x < 2f)
        {
            _playerAnimator.SetFloat("Speed_f", 0.49f);
            transform.Translate(Vector3.forward * Time.deltaTime * 2f);
            _particleDirt.Stop();
        }
        else
        {
            _playerAnimator.SetFloat("Speed_f", 1f);
        }
        if (Input.GetKeyDown(KeyCode.Space) && _isOnGround && !_isGameOver)
        {
            _isOnGround = false;
            _particleDirt.Stop();
            _playerAudioSource.PlayOneShot(_jumpSound);
            _playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _playerAnimator.SetTrigger("Jump_trig");
            _isDoubleUsed = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !_isOnGround && !_isDoubleUsed)
        {
            _isDoubleUsed = true;
            _particleDirt.Stop();
            _playerAudioSource.PlayOneShot(_jumpSound);
            _playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _playerAnimator.Play("Running_Jump", 3, 0f);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _playerAnimator.speed = _boostedAnimatorSpeed;
        }
        else
        {
            _playerAnimator.speed = _animatorSpeedAtStart;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") && !_isGameOver)
        {
            _isOnGround = true;
            _particleDirt.Play();
        }
        if (other.gameObject.CompareTag("Obstacle") && !_isGameOver)
        {
            _isGameOver = true;
            _particleExplosion.Play();
            _particleDirt.Stop();
            _playerAudioSource.PlayOneShot(_crashSound);
            _playerAnimator.SetBool("Death_b", true);
            _playerAnimator.SetInteger("DeathType_int", 1);
        }
    }
}
