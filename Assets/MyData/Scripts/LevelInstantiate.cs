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
    public AudioClip winClipSound;
    public AudioSource winClipAudio;

    private void Awake()
    {
        enemyOnScene = true;
        levelAudio = GetComponent<AudioSource>();
        //GameObject player = Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation);
        //player.GetComponent<Player>().scoreText = scoreText;
        //player.GetComponent<Player>().shootText = shootCount;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        enemy = GameObject.FindGameObjectWithTag("Enemy");

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
            winClipAudio.clip = winClipSound;
            winClipAudio.Play();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
    }
}
