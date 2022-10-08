using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _playerHealthTMP;
    [SerializeField] TextMeshPro _enemyHealthTMP;
    [SerializeField] bool _isPlayer;
    [SerializeField] int _health = 50;
    Collider _collider;

    bool _isDead;
    public bool IsDead { get { return _isDead; } }
    int _healthAtStart;

    Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _collider = GetComponentInChildren<Collider>();
    }

    private void Start()
    {
        _healthAtStart = _health;

        if (_playerHealthTMP != null && _isPlayer)
        {
            _playerHealthTMP.text = "Health: " + _health;
        }
        if (_enemyHealthTMP != null && !_isPlayer)
        {
            _enemyHealthTMP.text = _health.ToString();
        }

    }

    public void ModifyHealth(int amount)
    {
        if (_isDead) { return; }

        //Modifying health
        _health += amount;

        //Setting UI
        if (_isPlayer)
        {
            _playerHealthTMP.text = "Health: " + _health;
        }
        else
        {
            _enemyHealthTMP.text = _health.ToString();
        }

        _animator.SetTrigger("GetHit");

        if (_health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        _isDead = true;
        _animator.SetTrigger("Die");
        _collider.isTrigger = true;

        if (!_isPlayer)
        {
            //if this is enemy then add it to dead enemies for object pooling system
            EnemySpawner enemySpawner = transform.parent.GetComponent<EnemySpawner>();

            if (enemySpawner == null) { return; }
            enemySpawner.DeadEnemies.Add(this);

        }
        else
        {
            //if this is player then restart game after couple of seconds
            Invoke(nameof(Restart), 2f);
        }
    }

    public void ResetThis()
    {
        //Resetting everything
        _isDead = false;
        _animator.ResetTrigger("Die");
        _collider.isTrigger = false;

        if (!_isPlayer)
        {
            _health = _healthAtStart;
            _enemyHealthTMP.text = _healthAtStart.ToString();
            GetComponent<NavMeshAgent>().enabled = true;
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
