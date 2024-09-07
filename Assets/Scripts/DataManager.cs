using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    public int playerHealth , playerHealthMax;
    public int playerWeapon , playerWeaponMax;
    public int playerFish;
    public int playerPate;
    public string currentLevel;
    public int maxLevel;
    public string starLevel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGame();
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void ClearData()
    {
        
        PlayerPrefsManager.ClearData();     
        Debug.Log("ClearData");
    }

    public void LoadGame()
    {
        playerHealth = PlayerPrefsManager.LoadPlayerHealth();
        playerHealthMax = PlayerPrefsManager.LoadPlayerHealthMax();
        playerWeapon = PlayerPrefsManager.LoadPlayerWeapon();
        playerWeaponMax = PlayerPrefsManager.LoadPlayerWeaponMax();
        playerFish = PlayerPrefsManager.LoadPlayerFish();
        playerPate = PlayerPrefsManager.LoadPlayerPate();
        currentLevel = PlayerPrefsManager.LoadCurrentLevel();
        maxLevel = PlayerPrefsManager.LoadMaxLevel();
        starLevel = PlayerPrefsManager.LoadStarLevel();
    }

    public void SaveGame()
    {
        PlayerPrefsManager.SavePlayerHealth(playerHealth);
        PlayerPrefsManager.SavePlayerHealthMax(playerHealthMax);
        PlayerPrefsManager.SavePlayerWeapon(playerWeapon);
        PlayerPrefsManager.SavePlayerWeaponMax(playerWeaponMax);
        PlayerPrefsManager.SavePlayerFish(playerFish);
        PlayerPrefsManager.SavePlayerPate(playerPate);
        PlayerPrefsManager.SaveCurrentLevel(currentLevel);
        PlayerPrefsManager.SaveMaxLevel(maxLevel);
        PlayerPrefsManager.SaveStarLevel(starLevel);
    }

    public void AddPlayerWeapon(int weapon)
    {
        if(playerWeapon + weapon >= playerWeaponMax) 
        {
            playerWeapon = playerWeaponMax;
        }
        else
        {
            playerWeapon += weapon;
        }
        
    }
    public void MinusPlayerWeapon(int weapon)
    {
        if(playerWeapon - weapon <= 0) 
        {
            playerWeapon = 0;
        }
        else
        {
            playerWeapon -= weapon;
        }
        
    }

    public void AddPlayerHealth(int health)
    {
        if(playerHealth + health >= playerHealthMax) 
        {
            playerHealth = playerHealthMax;
        }
        else
        {
            playerHealth += health;
        }
        
    }

    public void MinusPlayerHealth(int health)
    {
        if(playerHealth - health <= 0)
        {
            playerHealth = 0;
        } 
        else 
        {
            playerHealth -= health;
        }
        SoundManager.Instance.PlaySFX(SoundManager.Instance.hurt);
    }

    public void AddPlayerFish(int fish)
    {
        playerFish += fish;
    }
    public void MinusPlayerFish(int fish)
    {
        if(playerFish - fish <= 0) 
        {
            playerFish = 0;
        }
        else
        {
            playerFish -= fish;
        }
        
    }

    public void AddPlayerPate(int pate)
    {
        playerPate += pate;
    }
    public void MinusPlayerPate(int pate)
    {
        playerPate -= pate;
    }

    public void AddPlayerHealthMax(int upgrade)
    {
        playerHealthMax += upgrade;
    }
    public void AddPlayerWeaponMax(int upgrade)
    {
        playerWeaponMax += upgrade;
    }

    public void SetCurrentLevel(string level)
    {
        currentLevel = level;
    }
    
    public void SetMaxLevel(int level)
    {
        maxLevel = level;
    }

    public void SetStarLevel(string star)
    {
        starLevel = star;
    }
}
