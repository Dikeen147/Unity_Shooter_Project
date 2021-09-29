using Assets.MyData.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstLevelSound : MonoBehaviour
{
    public Slider menuSoundSlider;
    public AudioSource menuAudio;
    public Text menuSounudPercent;

    private void Awake()
    {
        menuSounudPercent.text = string.Format("{0:0}", GameSettings.soundPercent);
        menuSoundSlider.value = GameSettings.audioVolume;
        menuAudio.volume = GameSettings.audioVolume;
    }

    void Update()
    {
        menuSounudPercent.text = string.Format("{0:0}%", menuSoundSlider.value * 100);
        menuAudio.volume = menuSoundSlider.value;
    }
}
