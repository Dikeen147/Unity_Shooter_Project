using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanRaysOrigin
{
    public Vector3 _rayOrigin;
    public Vector3 _rayDirection;
    public Color _rayColor = Color.yellow;
    public RaycastHit _rayHit;
    public float _rayDistance = 3f;
    public float _rayAngle;
    public Ray _ray;
}

public class TestSceneScript : MonoBehaviour
{
    public float _radius = 2f;
    public float _force = 100f;
    private Vector3 pointDir;

    #region RAY FIELDS

    private const int raysCount = 3;
    private float rayAngleStep = 10f;

    #endregion

    private Rigidbody _rb;
    public float JumpForce = 500f;
    private bool onGround = true;
    private bool _jump;
    private float _speed = 5f;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();

    }
    private void Update()
    {
        //Rays();

        if (onGround)
        {
            if (Input.GetButton("Jump"))
            {
                Debug.Log("jump ");
                _rb.AddForce(transform.up * JumpForce);
            }
        }

        PlayerMove();
    }

    private void PlayerMove()
    {
        float ad = Input.GetAxis("Horizontal");
        float ws = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(0, 0, ws);

        _rb.MovePosition(transform.position + transform.TransformDirection(dir.normalized) * _speed * Time.fixedDeltaTime);
        Vector3 rotate = new Vector3(0, ad, 0);
        _rb.MoveRotation(_rb.rotation * Quaternion.Euler(rotate));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            Debug.Log("На земле? " + onGround);
            onGround = true;
        }   
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            Debug.Log("вышел ");
            onGround = false;
        }
    }

    private void Rays()
    {
        List<ScanRaysOrigin> _scanRays = new List<ScanRaysOrigin>();
        float rayAngle = -10f;

        for (int i = 0; i < raysCount; i++)
        {
            _scanRays.Add(new ScanRaysOrigin
            {
                _rayOrigin = transform.position,
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
                Debug.Log(_scanRay._rayHit.collider.gameObject.name);
                _scanRay._rayColor = Color.green;
            }
            else
            {
                _scanRay._rayColor = Color.yellow;
            }

            Debug.DrawRay(
                _scanRay._rayOrigin,
                _scanRay._rayDirection * _scanRay._rayDistance,
                _scanRay._rayColor);
        }

    }
    private void Explode()
    {
        Collider[] overlapColliders = Physics.OverlapSphere(transform.position, _radius);

        foreach (var item in overlapColliders)
        {
            Rigidbody rb = item.attachedRigidbody;
            if (rb)
            {
                pointDir = rb.position - transform.position;
                pointDir.y = transform.position.y;

                rb.AddForce(pointDir * _force);

                Debug.Log("Execute Force");
            }
        }
    }
}
