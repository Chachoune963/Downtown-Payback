using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public static float musicVolume = 0.3f;
    public Slider sliderMusicVolume;
    public AudioSource music;

    public static float soundEffectsVolume = 0.4f;
    public Slider sliderSoundEffectsVolume;
    public AudioSource soundEffectsExemple;
    // Start is called before the first frame update
    void Start()
    {
        music.volume = musicVolume;
        music.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void VolumeMusicUpdate()
    {
        float newvolume = sliderMusicVolume.value;
        musicVolume = newvolume;
        music.volume = newvolume;
    }

    public void VolumeSoundEffectsUpdate()
    {
        float newvolume = sliderSoundEffectsVolume.value;
        soundEffectsVolume = newvolume;
        soundEffectsExemple.volume = newvolume;
        soundEffectsExemple.Play();
    }
}
/*
You are a young man who took a credit from the mafia. The mafia wants its money back but you have nothing. To defend your self from killers send by the mafia, you need to buy credit weapons in shops belonging to the mafia. Therefore your DEBT INCREASE and the mafia send MORE killers.
Try to stay alive.

CONTROLS : 
Z Q S D or Arrows to move
Right click to fire
F to open shop (need to be next to a shop)
X to use Jetpack */