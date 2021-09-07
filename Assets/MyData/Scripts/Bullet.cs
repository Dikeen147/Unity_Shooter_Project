using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Transform _target;
    private Vector3 _point;

    public void Initialization(float bulletLifeTime, Transform target) {
        _target = target;
        _point = target.position;

        Destroy(gameObject, bulletLifeTime);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _point, _speed * Time.fixedDeltaTime);
    }
}
