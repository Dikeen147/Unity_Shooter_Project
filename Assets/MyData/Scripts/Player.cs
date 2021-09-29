using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private List<AudioClip> _audioClipList;

    public AudioSource _playerShoot;
    public AudioSource _playerWalk;
    private Animator _playerAnimator;
    private bool _isFire;
    private bool _setMine;
    private float _rotateSpeedY = 300f;
    private Rigidbody _rb;
    private float _bulletForce = 10f;
    public float _mineForce = 5f;
    private float shiftMult = 1f;
    private bool reload = true;
    private bool gameOnPause;

    private Canvas[] canvasList;
    private Canvas gameHUD;
    private Canvas gamePause;
    private Canvas gameEnd;
    private Bullet bulletObj;

    public Text scoreText;
    public Text shootText;
    private int shootCount;
    private int scoreCount;

    private void Awake()
    {
        Time.timeScale = 1;
        //_playerWalk = GetComponent<AudioSource>();
        //_playerShoot = GetComponent<AudioSource>();
        //Canvas[] canvasList = FindObjectsOfType<Canvas>();
        gamePause = GameObject.FindGameObjectWithTag("gamePause").GetComponent<Canvas>();
        gameHUD = GameObject.FindGameObjectWithTag("gameHUD").GetComponent<Canvas>();
        gamePause.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        _rb = GetComponent<Rigidbody>();
        _playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        int.TryParse(string.Join("", scoreText.text.Where(c => char.IsDigit(c))), out scoreCount);
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
        
        if (Input.GetKey(KeyCode.Q))
        {
            Time.timeScale = 0.2f;
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            Time.timeScale = 1f;
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (gameOnPause)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = true;
            } 
            else 
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            gameOnPause = gameOnPause ? false : true;
            Time.timeScale = gameOnPause ? 0 : 1;
            gamePause.enabled = gameOnPause;
            gameHUD.enabled = !gameOnPause;
            //gamePause.SetActive(gameOnPause);
        }

        if (bulletObj != null && bulletObj.hitEnemy)
        {
            bulletObj.hitEnemy = false;
            Debug.Log("Есть попадание по врагу");
            scoreCount += 100;
            scoreText.text = $"Score: {scoreCount}";
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

    private void StepSound()
    {
        _playerWalk.clip = _audioClipList[0];
        _playerWalk.Play();
    }
    private void Fire()
    {
        _isFire = false;
        GameObject bullet = Instantiate(_bulletObject, _bulletSpawn.position, _bulletSpawn.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        bulletObj = bullet.GetComponent<Bullet>();
        bulletObj.Initialization(5f);

        rb.AddForce(bullet.transform.forward * _bulletForce, ForceMode.Impulse);
        _playerShoot.clip = _audioClipList[1];
        _playerShoot.Play();
        shootCount++;
        shootText.text = $"Shoot count: {shootCount}";
    }
    private void SetMine()
    {
        GameObject mine = Instantiate(_mine, _mineSpawnPlace.position, _mineSpawnPlace.rotation);
        Rigidbody rbmine = mine.GetComponent<Rigidbody>();
        rbmine.AddForce(mine.transform.forward * _mineForce, ForceMode.Impulse);

        _playerShoot.clip = _audioClipList[3];
        _playerShoot.Play();
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
            var a = other.GetComponent<AudioSource>();
            a.Play();
            Destroy(other.gameObject, 1f);
        }
    }
}
