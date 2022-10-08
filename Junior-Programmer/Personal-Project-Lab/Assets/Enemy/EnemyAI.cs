using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    PlayerController _target;
    NavMeshAgent _navMeshAgent;
    Animator _animator;
    Health _health;

    private void Awake()
    {
        _target = FindObjectOfType<PlayerController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _health = GetComponentInParent<Health>();
    }

    void Update()
    {
        if (_health.IsDead == true)
        {
            //resetting things
            _navMeshAgent.enabled = false;

            _animator.SetBool("CanWalk", !_health.IsDead);
            _animator.SetBool("CanAttack", !_health.IsDead);
            _animator.ResetTrigger("GetHit");
            return;
        }

        EngageTarget();
    }

    void EngageTarget()
    {
        //Defensive code
        if (_target == null || _navMeshAgent == null) { return; }

        //Setting destination
        _navMeshAgent.SetDestination(_target.transform.position);

        _animator.SetBool("CanWalk", true);
        _animator.SetBool("CanAttack", false);

        //Wolf attacks if it is close enough
        float distanceToTarget = Vector3.Distance(_target.transform.position, transform.position);
        if (distanceToTarget <= _navMeshAgent.stoppingDistance)
        {
            //For performance dont get component constantly
            Health targetHealth = null;
            if (targetHealth == null)
            {
                targetHealth = _target.GetComponent<Health>();
            }

            _animator.SetBool("CanWalk", false);
            _animator.SetBool("CanAttack", true);
        }
    }

    void FaceToTarget()
    {
        transform.LookAt(_target.transform);
    }
}
