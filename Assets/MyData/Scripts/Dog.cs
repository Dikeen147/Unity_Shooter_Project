using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dog : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _dog;
    [SerializeField] private Transform[] _dogWalkPoints;

    int _currentDogWalkPoint;

    private void Awake()
    {
        _dog = GetComponent<NavMeshAgent>();
    }
    
    private void FixedUpdate()
    {
        if (_dog.remainingDistance < _dog.stoppingDistance)
        {
            _currentDogWalkPoint = (_currentDogWalkPoint + 1) % _dogWalkPoints.Length;
            _dog.SetDestination(_dogWalkPoints[_currentDogWalkPoint].position);
        }
    }
}
