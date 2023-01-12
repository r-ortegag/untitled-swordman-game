using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource musicSource;

    void Start()
    {
        musicSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        musicSource.volume = PlayerPrefs.GetFloat("Music");
    }

    public void SetVolumeMusic(float volume)
    {
        PlayerPrefs.SetFloat("Music", volume);
    }
}
