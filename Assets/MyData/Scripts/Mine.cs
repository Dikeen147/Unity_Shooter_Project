using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private int _damage;
    public float _radius = 5f;
    public float _force = 100f;

    private Vector3 _explosiveDirection;
    private GameObject _enemy;

    private void OnTriggerEnter(Collider  other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Враг наступил на мину. Взрыв мины");
            _enemy = other.gameObject;
            Explode();
            // var enemy = other.GetComponent<Enemy>();
            // enemy.Hurt(_damage);            
            // Destroy(gameObject);
        }
        if (other.CompareTag("Player"))
        {
            Debug.Log("Игрок наступил на мину");
            //Explode(other);
            // var enemy = other.GetComponent<Enemy>();
            // enemy.Hurt(_damage);            
            // Destroy(gameObject);
        }
    }

    public void Explode()
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius);

        foreach (var item in overlappedColliders)
        {
            Rigidbody rb = item.attachedRigidbody;

            if (rb && !rb.gameObject.CompareTag("Player"))
            {
                _explosiveDirection = rb.position - transform.position;
                _explosiveDirection.y = transform.position.y;

                rb.AddForce(_explosiveDirection * _force);

                Debug.Log("Execute Force");
            }
        }
        Destroy(_enemy);
        Destroy(gameObject);
    }
}
