using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    public bool hitEnemy;

    public void Initialization(float bulletLifeTime) {

        Destroy(gameObject, bulletLifeTime);
    }

    private void Update()
    {
        
        //transform.position += transform.forward * _speed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        //transform.position = Vector3.MoveTowards(transform.position, _point, _speed * Time.fixedDeltaTime);
        //transform.position += new Vector3(Time.deltaTime *_speed , 0, 0); 
        //transform.position += transform.forward * _speed * Time.fixedDeltaTime;
    }
   

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hitEnemy = true;
        }
    }
}
