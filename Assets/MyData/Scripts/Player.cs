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
    [SerializeField] private GameObject _mine;
    [SerializeField] private Transform _mineSpawnPlace;
    [SerializeField] private Transform _bodyRotateY;

    private Animator _playerAnimator;
    private bool _isFire;
    private bool _setMine;
    private float _rotateSpeedY = 300f;
    private Rigidbody _rb;
    private float _bulletForce = 10f;
    public float _mineForce = 5f;
    private float shiftMult = 1f;
    private bool reload = true;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        _rb = GetComponent<Rigidbody>();
        _playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        _isFire = Input.GetMouseButtonDown(0);
        if (reload) _setMine = Input.GetMouseButtonDown(1);
        _direction.x = Input.GetAxis("Horizontal");
        _direction.z = Input.GetAxis("Vertical");
        _direction.y = Input.GetAxis("Jump");

        if (_direction == Vector3.zero)
        {
            _playerAnimator.SetBool("IsMove", false);
            
            if (_isFire)
            {
                _playerAnimator.SetTrigger("Shoot");
            }
        }
        else
        {
            _playerAnimator.SetBool("IsMove", true);
            
            if (_isFire)
            {
                Fire();
            }
        }
        
        if (_setMine) SetMine();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            shiftMult = 2f;
        }
        else
        {
            shiftMult = 1f;
        }
    }

    private void FixedUpdate()
    {
        //transform.Translate(_direction.normalized * _speed * Time.fixedDeltaTime);
        _direction = transform.TransformDirection(_direction);
        _rb.MovePosition(transform.position + _direction.normalized * _speed * shiftMult * Time.fixedDeltaTime);
        //transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * _rotateSpeed * Time.fixedDeltaTime);
        Vector3 rotate = new Vector3(0, (Input.GetAxis("Mouse X") * _rotateSpeed * Time.fixedDeltaTime), 0);
        
        _rb.MoveRotation(_rb.rotation * Quaternion.Euler(rotate));
        _bodyRotateY.Rotate(Vector3.left, Input.GetAxis("Mouse Y") * _rotateSpeedY * Time.fixedDeltaTime);
    }
    private void Fire()
    {
        _isFire = false;
        GameObject bullet = Instantiate(_bulletObject, _bulletSpawn.position, _bulletSpawn.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        bullet.GetComponent<Bullet>().Initialization(5f);
        rb.AddForce(bullet.transform.forward * _bulletForce, ForceMode.Impulse);
    }
    private void SetMine()
    {
        GameObject mine = Instantiate(_mine, _mineSpawnPlace.position, _mineSpawnPlace.rotation);
        Rigidbody rbmine = mine.GetComponent<Rigidbody>();
        rbmine.AddForce(mine.transform.forward * _mineForce, ForceMode.Impulse);

        reload = false;
        _setMine = false;
        Invoke("ReloadMine", 2f);
    }

    private void ReloadMine()
    {
        reload = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUpItem"))
        {
            Debug.Log("Вы подобрали: " + other.gameObject.name);
            Destroy(other.gameObject);
        }
    }
}
