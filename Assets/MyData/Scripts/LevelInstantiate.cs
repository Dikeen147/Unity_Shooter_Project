using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInstantiate : MonoBehaviour
{
    private GameObject enemy;
    private GameObject player;
    public GameObject playerPrefab;
    public Transform playerSpawn;
    public Text scoreText;
    public Text shootCount;
    public Canvas endGameCanvas;
    private bool enemyOnScene;
    private AudioSource levelAudio;

    private void Awake()
    {
        enemyOnScene = true;
        levelAudio = GetComponent<AudioSource>();
        //GameObject player = Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation);
        //player.GetComponent<Player>().scoreText = scoreText;
        //player.GetComponent<Player>().shootText = shootCount;

        enemy = GameObject.FindGameObjectWithTag("Enemy");
        var a = endGameCanvas;

    }

    private void FixedUpdate()
    {
        if (enemy == null && enemyOnScene)
        {
            enemyOnScene = false;
            //endGameCanvas.enabled = true;
            endGameCanvas.gameObject.active = true;
            Time.timeScale = 0;
            levelAudio.Stop();

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

        }
    }
}
