using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotate : MonoBehaviour
{
    [SerializeField] private GameObject _bulletObject;
    [SerializeField] private Transform _bulletSpawn;
    public float _rotateSpeed = 20f;
    public float _atackRotateSpeed = 1f;
    private Transform _target;
    private float count;
    private float maxCount;
    private Vector3 _turnSide;

    private const float _updateTimeConst = 0.2f;
    private float _updateTime = _updateTimeConst;

    void Start()
    {
        maxCount = 50;
        _turnSide = Vector3.up;
    }

    void FixedUpdate()
    {
        if (_target == null)
        {
            if (count <= maxCount)
            {
                transform.Rotate(_turnSide, _rotateSpeed * Time.fixedDeltaTime);
                count += _rotateSpeed * Time.fixedDeltaTime;
            }
            else
            {
                count = 0;
                maxCount = 100;
                _turnSide *= -1;
            }
        }
        else
        {
            Vector3 direction =  _target.position - transform.position;
            Vector3 stepDir = Vector3.RotateTowards(transform.forward, direction, _atackRotateSpeed * Time.fixedDeltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(stepDir);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _target = other.transform;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _target = other.transform;

            if (_updateTime <= 0)
            {
                GameObject bullet = Instantiate(_bulletObject, _bulletSpawn.position, Quaternion.identity);
                bullet.GetComponent<TurretBullet>().Initialization(2f, _target);
                Debug.Log("Вы под обстрелом!");
                _updateTime = _updateTimeConst;
            }
            else
            {
                _updateTime -= Time.fixedDeltaTime;
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _target = null;
            Debug.Log("А сейчас норм");
        }
    }

}
