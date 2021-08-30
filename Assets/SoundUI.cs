using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundUI : MonoBehaviour
{
    public Image soundImage;
    public Image musicImage;
    [Space]
    public Sprite soundOn;
    public Sprite soundOff;
    [Space]
    public Sprite musicOn;
    public Sprite musicOff;
    [Space]
    public AudioMixerGroup soundMixer;
    public AudioMixerGroup musicMixer;

    public bool isSoundOn;
    public bool isMusicOn;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("isSoundOn"))
        {
            PlayerPrefs.SetInt("isSoundOn", 1);
            PlayerPrefs.SetInt("isMusicOn", 1);
        }
        isSoundOn = PlayerPrefs.GetInt("isSoundOn") == 1;
        isMusicOn = PlayerPrefs.GetInt("isMusicOn") == 1;

        isMusicOn = !isMusicOn;
        ToggleMusic();
        isSoundOn = !isSoundOn;
        ToggleSound();
    }

    public void ToggleMusic()
    {
        if (isMusicOn)
        {
            isMusicOn = false;
            musicImage.sprite = musicOff;
            musicMixer.audioMixer.SetFloat("volumeMusic", -80);
            PlayerPrefs.SetInt("isMusicOn", 0);
        }
        else
        {
            isMusicOn = true;
            musicImage.sprite = musicOn;
            musicMixer.audioMixer.SetFloat("volumeMusic", 0);
            PlayerPrefs.SetInt("isMusicOn", 1);
        }
    }

    public void ToggleSound()
    {
        if (isSoundOn)
        {
            isSoundOn = false;
            soundImage.sprite = soundOff;
            soundMixer.audioMixer.SetFloat("volumeSounds", -80);
            PlayerPrefs.SetInt("isSoundOn", 0);

            isMusicOn = true;
            ToggleMusic();
        }
        else
        {
            isSoundOn = true;
            soundImage.sprite = soundOn;
            soundMixer.audioMixer.SetFloat("volumeSounds", 0);
            PlayerPrefs.SetInt("isSoundOn", 1);

            isMusicOn = PlayerPrefs.GetInt("isMusicOn") == 1;
            isMusicOn = !isMusicOn;
            ToggleMusic();
        }
    }
}
