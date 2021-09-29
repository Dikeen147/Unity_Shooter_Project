using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelItems : MonoBehaviour
{
    private float _turnSpeed = 50f;
    private AudioSource _itemAudio;

    void Start()
    {
        _itemAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Vector3.up, _turnSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Вы подобрали предмет");
            if (_itemAudio != null) _itemAudio.Play();
            Destroy(gameObject, 0.5f);
            
        }
    }

}
