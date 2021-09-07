using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter trigger zone");
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Its player");
            //GameObject go = Instantiate(_enemyPrefab);
            //go.transform.position = other.transform.position + other.transform.forward * 10f;
 
            List<GameObject> goList = new List<GameObject>();
            List<Vector3> enemyPlacesList = new List<Vector3>
            { 
                new Vector3(7, 0, 20),
                new Vector3(-7, 0, 13), 
                new Vector3(-21, 0, 4),
                new Vector3(-25, 0, 10)
            };
            for (int i = 0; i < enemyPlacesList.Count; i++)
            {
                goList.Add(Instantiate(_enemyPrefab));
                goList[i].transform.position = enemyPlacesList[i];
            }

            Destroy(gameObject);
        }
    }
}
