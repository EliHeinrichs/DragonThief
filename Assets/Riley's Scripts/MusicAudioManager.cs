using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MusicAudioManager : MonoBehaviour
{
    public AudioClip[] musicSounds;
    public AudioSource[] musicAudioSources;
    public string musicTitle;

    void Update() {
                for(int i = 0; i < musicAudioSources.Length; i++) {
            musicAudioSources[i].volume = PlayerPrefs.GetFloat("MusicVolume");
        }
    }

    void Awake() {
        Playmusic(musicTitle);
    }

    public void Playmusic(string name) {
        AudioClip s = Array.Find(musicSounds, x => x.name == name);

        if (s == null) {
            Debug.Log("Sound not found!");
            return;
        }

        // Find the first available audio source
        AudioSource availableSource = Array.Find(musicAudioSources, source => !source.isPlaying);

        if (availableSource == null) {
            Debug.Log("All audio sources are busy!");
            return;
        }

        availableSource.clip = s;
        availableSource.pitch = UnityEngine.Random.Range(1f, 1.1f);
        availableSource.Play();
    }
}