using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{   
    void Start()
    {
        Time.timeScale = 1;
        SoundManager.Instance.SetMusicVolume(0.1f);
        SoundManager.Instance.PlayMusic(SoundManager.Instance.soundMenu); 
    } 
    private void OnDestroy()
    {
        SoundManager.Instance.StopMusic();
    }

    public void PlayButtonClick()
    {
        SceneManager.LoadScene(DataManager.Instance.currentLevel);
    }

    public void QuitButtonClick()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void PlaySoundClick()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.click);
    }

}
