using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SFXAudioManager : MonoBehaviour
{
    public AudioClip[] playerSounds;
    public AudioSource[] playerAudioSources;

    void Update() {
        for(int i = 0; i < playerAudioSources.Length; i++) {
            playerAudioSources[i].volume = PlayerPrefs.GetFloat("SFXVolume");
        }
    }

    public void PlaySound(string name) {
        AudioClip s = Array.Find(playerSounds, x => x.name == name);

        if (s == null) {
            Debug.Log("Sound not found!");
            return;
        }

        // Find the first available audio source
        AudioSource availableSource = Array.Find(playerAudioSources, source => !source.isPlaying);

        if (availableSource == null) {
            Debug.Log("All audio sources are busy!");
            return;
        }

        availableSource.clip = s;
        availableSource.pitch = UnityEngine.Random.Range(1f, 1.1f);
        availableSource.Play();
    }
}