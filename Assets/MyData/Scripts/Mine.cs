using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Mine : MonoBehaviour
{
    [SerializeField] private int _damage;
    public float _radius = 5f;
    public float _force = 100f;
    public AudioSource mineAudio;
    private Text gameScoreText;
    private int gameScoreCount;

    private Vector3 _explosiveDirection;
    private GameObject _enemy;

    public GameObject explosionPrefab;

    private void Awake()
    {
        gameScoreText = GameObject.FindGameObjectWithTag("GameScore").GetComponent<Text>();
        int.TryParse(string.Join("", gameScoreText.text.Where(c => char.IsDigit(c))), out gameScoreCount);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !other.isTrigger)
        {
            Debug.Log("Враг наступил на мину. Взрыв мины");
            _enemy = other.gameObject;
            if (mineAudio != null) mineAudio.Play();
            gameScoreText.text = $"Score: {gameScoreCount + 500}";
            _enemy.GetComponent<Enemy>().enemyHealth -= 500;
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
        //Destroy(_enemy, 0.1f);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject, 2f);
    }
}
