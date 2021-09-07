using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private int _damage;

    private void OnTriggerEnter(Collider  other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Враг наступил на мину. Взрыв мины");
            // var enemy = other.GetComponent<Enemy>();
            // enemy.Hurt(_damage);            
            // Destroy(gameObject);
        }
        if (other.CompareTag("Player"))
        {
            Debug.Log("Игрок наступил на мину");
            // var enemy = other.GetComponent<Enemy>();
            // enemy.Hurt(_damage);            
            // Destroy(gameObject);
        }
    }
}
