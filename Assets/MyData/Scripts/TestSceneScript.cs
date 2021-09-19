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

    void Start()
    {
    }
    private void Update()
    {
        Rays();
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
