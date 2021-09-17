using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScanRays
{
    public Vector3 _rayOrigin;
    public Vector3 _rayDirection;
    public Color _rayColor = Color.yellow;
    public RaycastHit _rayHit;
    public float _rayDistance = 10f;
    public float _rayAngle;
    public Ray _ray;
}

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform[] _enemyWalkPoints;

    private Transform _enemyNewTargetIsPlayer;
    private int _currentEnemyWalkPoint;
    private bool _enemySeePlayer;
    private const int raysCount = 7;
    private float rayAngleStep = 3f;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        //_agent.SetDestination(_target.position);
    }

    private void Update()
    {
        Rays();
    }

    private void Rays()
    {
        List<ScanRays> _scanRays = new List<ScanRays>();
        float rayAngle = (raysCount / 2) * rayAngleStep * -1f;

        for (int i = 0; i < raysCount; i++)
        {
            _scanRays.Add(new ScanRays
            {
                _rayOrigin = transform.position + new Vector3(0f, 1.4f, 0f),
                _rayDirection = Quaternion.AngleAxis(rayAngle, Vector3.up) * transform.forward,
                _rayColor = Color.yellow,
                _rayAngle = rayAngle,
                _ray = new Ray(transform.position, Quaternion.AngleAxis(rayAngle, Vector3.up) * transform.forward)
            });

            rayAngle += rayAngleStep;
        }

        foreach (var _scanRay in _scanRays)
        {
            bool isHit = Physics.Raycast(_scanRay._ray, out _scanRay._rayHit, _scanRay._rayDistance);

            if (isHit)
            {
                if (_scanRay._rayHit.collider.gameObject.CompareTag("Player"))
                {
                    Debug.Log("Enemy see Player");
                    _enemyNewTargetIsPlayer = _scanRay._rayHit.collider.gameObject.transform;
                }

                //Debug.Log(_scanRay._rayHit.collider.gameObject.name);
                _scanRay._rayColor = Color.green;
            }
            else
            {
                _enemyNewTargetIsPlayer = null;
                _scanRay._rayColor = Color.yellow;
            }

            Debug.DrawRay(
                _scanRay._rayOrigin,
                _scanRay._rayDirection * _scanRay._rayDistance,
                _scanRay._rayColor);
        }

    }

    private void EnemyWalk()
    {
        if (_enemyWalkPoints == null) return;

        if (_agent.remainingDistance < _agent.stoppingDistance)
        {
            _currentEnemyWalkPoint = (_currentEnemyWalkPoint + 1) % _enemyWalkPoints.Length;
            _agent.SetDestination(_enemyWalkPoints[_currentEnemyWalkPoint].position);
        }
    }

    private void FixedUpdate()
    {
        if (_enemyNewTargetIsPlayer == null)
        {
            if (_enemySeePlayer)
            {
                _agent.SetDestination(transform.position);
                Invoke("GoBackToPatrolling", 2f);
            }
            else
            {
                EnemyWalk();
            }
        }
        else
        {
            _enemySeePlayer = true;
            _agent.SetDestination(_enemyNewTargetIsPlayer.position);
        }
    }

    private void GoBackToPatrolling()
    {
        _enemySeePlayer = false;
    }

    public void SetEnemyWalkPoint(Transform point)
    {
        _enemyWalkPoints[0] = point;
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
