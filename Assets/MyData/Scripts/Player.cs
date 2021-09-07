using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed = 500f;
    [SerializeField] private Vector3 _direction;
    [SerializeField] private GameObject _bulletObject;
    [SerializeField] private Transform _bulletSpawn;
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _mine;
    [SerializeField] private Transform _mineSpawnPlace;
    private bool _isFire;
    private bool _setMine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        _isFire = Input.GetMouseButtonDown(0);
        _setMine = Input.GetMouseButtonDown(1);
        _direction.x = Input.GetAxis("Horizontal");
        _direction.z = Input.GetAxis("Vertical");    
    }

    private void FixedUpdate()
    {
        transform.Translate(_direction.normalized * _speed * Time.fixedDeltaTime);
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * _rotateSpeed * Time.fixedDeltaTime);

        if (_isFire) Fire();
        if (_setMine) SetMine();
    }
    private void Fire()
    {
        _isFire = false;
        GameObject bullet = Instantiate(_bulletObject, _bulletSpawn.position, Quaternion.identity);

        bullet.GetComponent<Bullet>().Initialization(5f, _target);
    }
    private void SetMine()
    {
        GameObject mine = Instantiate(_mine, _mineSpawnPlace.position, _mineSpawnPlace.rotation);
        
    }

}
