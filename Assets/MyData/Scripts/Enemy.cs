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
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform[] _enemyWalkPoints;

    private Animator _enemyAnim;
    public GameObject player;
    private Transform _enemyNewTargetIsPlayer;
    private int _currentEnemyWalkPoint;
    private Transform lastPlayerPosition;
    private bool _enemySeePlayer;
    private const int raysCount = 7;
    private float rayAngleStep = 3f;
    private bool enemySeePlayer;
    private bool enemySeePlayerLastPosition;
    public int enemyHealth = 500;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _enemyAnim = transform.GetComponent<Animator>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");   
    }

    private void EnemyPatrolling()
    {
        if (_agent == null) return;

        if (_enemyWalkPoints.Length > 0 && _agent.remainingDistance < _agent.stoppingDistance)
        {
            _currentEnemyWalkPoint = (_currentEnemyWalkPoint + 1) % _enemyWalkPoints.Length;
            _agent.SetDestination(_enemyWalkPoints[_currentEnemyWalkPoint].position);
        }
    }

    private void FixedUpdate()
    {
        EnemyPatrolling();

        if (enemyHealth <= 0)
        {
            _agent.SetDestination(transform.position);
            _enemyAnim.SetTrigger("Die");
            Destroy(gameObject, 1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            enemyHealth -= 50;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("В зоне видимости");
            //float distance = Vector3.Distance(transform.position, _player.transform.position);
            Ray ray = new Ray(transform.position, (player.transform.position - transform.position));

            Debug.DrawRay(transform.position, ray.direction * 10f, Color.yellow);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10f) && hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("Враг нас прям видит");
                _agent.speed = 3f;
                transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.color = Color.red;
                //transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.color = Color.red;
                _agent.SetDestination(player.transform.position);

                enemySeePlayer = true;
                enemySeePlayerLastPosition = true;
            }
            else if (enemySeePlayerLastPosition && Physics.Raycast(ray, out hit, 10f) && !hit.collider.gameObject.CompareTag("Player"))
            {
                lastPlayerPosition = player.transform;
                _agent.SetDestination(lastPlayerPosition.position);
                enemySeePlayerLastPosition = false;
            }
            else
            {
                if (enemySeePlayer)
                {
                    if (transform.position.Equals(lastPlayerPosition))
                    {
                        _agent.isStopped = true;
                        Invoke("EnemyBackPatrolling", 2f);
                    }
                }
                else
                {
                    transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.color = Color.gray;
                    //transform.GetChild(0).GetComponent<Renderer>().material.color = Color.gray;
                    //transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.color = Color.gray;
                    EnemyPatrolling();
                }
            }
        }
    }

    private void StepSound()
    {
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _agent.speed = 2f;
            transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.color = Color.gray;
            //transform.GetChild(0).GetComponent<Renderer>().material.color = Color.gray;
            //transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.color = Color.gray;
            EnemyPatrolling();
        }
    }

    public void SetEnemyWalkPoint(Transform point)
    {
        _enemyWalkPoints[0] = point;
    }

    private void EnemyBackPatrolling()
    {
        _agent.isStopped = false;
        enemySeePlayer = false;
        EnemyPatrolling();
    }
}
