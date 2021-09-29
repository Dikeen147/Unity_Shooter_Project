using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemy : MonoBehaviour
{
    public NavMeshAgent _agent;
    private GameObject _player;
    public Transform[] _enemyWalkPoints;
    private Vector3 _lastPlayerPosition;
    private int _currentEnemyWalkPoint;
    private bool _enemySeePlayer;


    void Start()
    {
        //_agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Point");
    }

    
    void FixedUpdate()
    {
        EnemyWalk();
        //float distance = Vector3.Distance(transform.position, _player.transform.position);
        //Ray ray = new Ray(transform.position, (_player.transform.position - transform.position));

        //Debug.DrawRay(transform.position, ray.direction * 5f, Color.yellow);

        //if (distance <= 5f)
        //{
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit, 10f) && hit.collider.gameObject.CompareTag("Point"))
        //    {
        //        _enemySeePlayer = true;
        //        _agent.SetDestination(_player.transform.position);
        //        Debug.Log("Enemy see Player");
        //    }
        //    else
        //    {
        //        if (_enemySeePlayer)
        //        {
        //            Debug.Log("Мы потеряли игрока из виду");
        //            //Debug.Log("Перестали видеть игрока inner if. last position: " + _lastPlayerPosition.position);

        //            _agent.SetDestination(transform.position);
        //            Invoke("SetEnemySeePlayer", 2f);
        //            //if (_agent.remainingDistance < _agent.stoppingDistance)
        //            //{
        //            //    Invoke("SetEnemySeePlayer", 2f);
        //            //}
        //        }
        //        else
        //        {
        //            EnemyWalk();
        //        }
        //    }
        //}
        //else
        //{
        //    if (_enemySeePlayer)
        //    {
        //        Debug.Log("Мы потеряли игрока из виду");
        //        //Debug.Log("Перестали видеть игрока. last position: " + _lastPlayerPosition.position);

        //        _agent.SetDestination(transform.position);
        //        Invoke("SetEnemySeePlayer", 2f);
        //        //if (_agent.remainingDistance < _agent.stoppingDistance)
        //        //{
        //        //    Invoke("SetEnemySeePlayer", 2f);
        //        //}
        //    }
        //    else
        //    {
        //        EnemyWalk();
        //    }
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Point"))
        {

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Point"))
        {
            //float distance = Vector3.Distance(transform.position, _player.transform.position);
            Ray ray = new Ray(transform.position, (_player.transform.position - transform.position));

            Debug.DrawRay(transform.position, ray.direction * 5f, Color.yellow);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10f) && hit.collider.gameObject.CompareTag("Point"))
            {
                transform.GetComponent<Renderer>().material.color = Color.red;
                _agent.SetDestination(_player.transform.position);
            }
            else
            {
                transform.GetComponent<Renderer>().material.color = Color.gray;
                EnemyWalk();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Point"))
        {
            transform.GetComponent<Renderer>().material.color = Color.gray;
            EnemyWalk();
        }   
    }

    private void SetEnemySeePlayer()
    {
        _enemySeePlayer = false;
    }


    private void EnemyWalk()
    {
        Debug.Log("На патруле");
        if (_agent.remainingDistance < _agent.stoppingDistance && _enemyWalkPoints.Length > 0)
        {
            _currentEnemyWalkPoint = (_currentEnemyWalkPoint + 1) % _enemyWalkPoints.Length;
            _agent.SetDestination(_enemyWalkPoints[_currentEnemyWalkPoint].position);
        }
    }
}
