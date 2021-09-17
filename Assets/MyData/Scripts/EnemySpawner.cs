using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    public Transform[] _enemySpawnPoints;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //GameObject go = Instantiate(_enemyPrefab);
            //go.transform.position = other.transform.position + other.transform.forward * 10f;
 
            List<GameObject> goList = new List<GameObject>();
            //List<Vector3> enemyPlacesList = new List<Vector3>
            //{ 
            //    new Vector3(7, 0, 20),
            //    new Vector3(-7, 0, 13), 
            //    new Vector3(-21, 0, 4),
            //    new Vector3(-25, 0, 10)
            //};
            for (int i = 0; i < _enemySpawnPoints.Length; i++)
            {
                GameObject enemy = Instantiate(_enemyPrefab);
                enemy.transform.position = _enemySpawnPoints[i].position;

                //Enemy enemyScriptComponent = enemy.GetComponent<Enemy>();

                //enemyScriptComponent
                enemy.GetComponent<Enemy>().SetEnemyWalkPoint(enemy.transform);


                //goList.Add(enemy);
                //goList[i]
                //enemy.GetComponent<Enemy>().SetEnemyWalkPoint(.transform);
                //goList.Add(Instantiate(_enemyPr));
                //goList[i].transform.position = _enemySpawnPoints[i].position;
            }

            Destroy(gameObject);
        }
    }
}
