using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.MyData.Scripts;

public class Menu : MonoBehaviour
{
    public Slider menuSoundSlider;
    public AudioSource menuAudio;
    public Text menuSounudPercent;
    private Button playGameButton;

    private void Awake()
    {
        menuSounudPercent.text = string.Format("{0:0}", menuSoundSlider.value * 100);
        playGameButton = GameObject.FindGameObjectWithTag("PlayGameButton").GetComponent<Button>();
    }
    private void Update()
    {
        menuSounudPercent.text = string.Format("{0:0}%", menuSoundSlider.value * 100);
        menuAudio.volume = menuSoundSlider.value;

        GameSettings.audioVolume = menuAudio.volume;
        GameSettings.soundPercent = menuSoundSlider.value * 100;
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
