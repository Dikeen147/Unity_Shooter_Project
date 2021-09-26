using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInstantiate : MonoBehaviour
{
    private GameObject player;
    public GameObject playerPrefab;
    public Transform playerSpawn;
    public Text scoreText;
    public Text shootCount;
    public TextMesh enemyHP;
    public Transform e;

    private void Awake()
    {
        GameObject player = Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation);
        player.GetComponent<Player>().scoreText = scoreText;
        player.GetComponent<Player>().shootText = shootCount;

        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
    }
}
