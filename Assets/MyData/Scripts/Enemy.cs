using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform[] _enemyWalkPoints;

    int _currentEnemyWalkPoint;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        //_agent.SetDestination(_target.position);    
    }

    private void FixedUpdate()
    {
        if (_agent.remainingDistance < _agent.stoppingDistance)
        {
            _currentEnemyWalkPoint = (_currentEnemyWalkPoint + 1) % _enemyWalkPoints.Length;
            _agent.SetDestination(_enemyWalkPoints[_currentEnemyWalkPoint].position);
        }
    }

    public void Hurt(int damage)
    {
        print("Ouch: " + damage);

        _health -= damage;;

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject, 1f);
    }

}
