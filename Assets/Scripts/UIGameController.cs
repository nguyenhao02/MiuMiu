using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class UIGameController : MonoBehaviour
{
    [SerializeField] private GameObject uiMenu;
    private GameObject player;
    private PlayerController playerController;

    void Start()
    {
        player  = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if(DataManager.Instance.playerHealth == 0)
        {
           OpenMenu();
        }
    }

    public void OpenMenu()
    {
        uiMenu.SetActive(true);
        PauseGame();
    }

    public void MenuButtonClick()
    {
        SceneManager.LoadScene("Menu");
    }

    public void PauseGame()
    {
        playerController.SetPause(true);
        Time.timeScale = 0;
    }
    public void GoGame()
    {
        //if(DataManager.Instance.playerHealth == 0) return;
        playerController.SetPause(false);
        Time.timeScale = 1;
    }

     public void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadThisScene()
    {
        if(DataManager.Instance.playerHealth < 1) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void WatchAds()
    {
        DataManager.Instance.AddPlayerHealth(1);
    }
}
