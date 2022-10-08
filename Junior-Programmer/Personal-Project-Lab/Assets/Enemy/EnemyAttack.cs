using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] int _damageAmount = 15;
    Health _playerHealth;

    private void Awake()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        _playerHealth = player.GetComponent<Health>();
    }

    public void Attack()
    {
        _playerHealth.ModifyHealth(-_damageAmount);
    }
}
