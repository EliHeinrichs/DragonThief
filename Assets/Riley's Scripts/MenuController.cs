using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.UI;

public class MenuController : MonoBehaviour
{
    public Slider sfxSlider, musicSlider;

    public Canvas deathCanvas;

    public Button firstSelectButton;
    public InputSystemUIInputModule inputModule;
    
    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Awake() {
        GameManager.Instance.playerIsDead = false;
        
        if(sfxSlider != null) {
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
        if(musicSlider != null) {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }

        if(deathCanvas != null) {
            deathCanvas.enabled = false;
        }

        if(inputModule != null) {
            inputModule.enabled = false;
        }
    }

    void Update() {
        if(deathCanvas == null) {
            return;
        }

        if(GameManager.Instance.playerIsDead && inputModule != null && deathCanvas != null) {
            inputModule.enabled = true;
            deathCanvas.enabled = true;
        } else {
            inputModule.enabled = false;
            deathCanvas.enabled = false;
        }
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToShop() {
        SceneManager.LoadScene("Shop");
    }

    public void GoToLevelSelect() {
        SceneManager.LoadScene("LevelSelect");
    }

    public void GoToNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ToggleOptionsMenu() {
        //if the menu is off, turn on, if the menu is on, turn off.
    }

    public void ToggleLeaderboardMenu() {
        //if the menu is off, turn on, if the menu is on, turn off.
    }
    public void ClearData() {
        PlayerPrefs.DeleteAll();
    }

    public void SafetyCheck() {
        // activate a panel to make sure the player wants to do said action.
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void PlaySelectSound() {
        //AudioManager.instance.PlaySFX("MenuSelect", false, 1);
    }

    public void MusicVolume() {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void SFXVolume() {
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }
}
