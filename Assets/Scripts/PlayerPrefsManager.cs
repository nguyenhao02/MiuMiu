using UnityEngine;

public static class PlayerPrefsManager
{
    private const string PlayerFishKey = "PlayerFish";
    private const string PlayerPateKey = "PlayerPate";
    private const string PlayerHealthKey = "PlayerHealth";
    private const string PlayerHealthMaxKey = "PlayerHealthMax";
    private const string PlayerWeaponKey = "PlayerWeapon";
    private const string PlayerWeaponMaxKey = "PlayerWeaponMax";
    private const string CurrentLevelKey = "CurrentLevel";
    private const string MaxLevelKey = "MaxLevel";
    private const string StarLevelKey = "StarLevel";


    // public static void Save()
    // {
    //     PlayerPrefs.SetInt();
    //     PlayerPrefs.Save();
    // }

    // public static int Load()
    // {
    //     return PlayerPrefs.GetInt(); 
    // }
    public static void SaveStarLevel(string star)
    {
        PlayerPrefs.SetString(StarLevelKey, star);
        PlayerPrefs.Save();
    }

    public static string LoadStarLevel()
    {
        return PlayerPrefs.GetString(StarLevelKey, new string('0', 100)); 
    }

    public static void SaveMaxLevel(int level)
    {
        PlayerPrefs.SetInt(MaxLevelKey, level);
        PlayerPrefs.Save();
    }
    public static int LoadMaxLevel()
    {
        return PlayerPrefs.GetInt(MaxLevelKey, 0);
    }

    public static void SavePlayerPate(int pate)
    {
        PlayerPrefs.SetInt(PlayerPateKey, pate);
        PlayerPrefs.Save();
    }
    public static int LoadPlayerPate()
    {
        return PlayerPrefs.GetInt(PlayerPateKey, 0); 
    }

    public static void SavePlayerWeaponMax(int weaponMax)
    {
        PlayerPrefs.SetInt(PlayerWeaponMaxKey, weaponMax);
        PlayerPrefs.Save();
    }

    public static int LoadPlayerWeaponMax()
    {
        return PlayerPrefs.GetInt(PlayerWeaponMaxKey, 10); 
    }

    public static void SavePlayerWeapon(int weapon)
    {
        PlayerPrefs.SetInt(PlayerWeaponKey, weapon);
        PlayerPrefs.Save();
    }

    public static int LoadPlayerWeapon()
    {
        return PlayerPrefs.GetInt(PlayerWeaponKey, 10); 
    }

    public static void SavePlayerFish(int money)
    {
        PlayerPrefs.SetInt(PlayerFishKey, money);
        PlayerPrefs.Save();
    }

    public static int LoadPlayerFish()
    {
        return PlayerPrefs.GetInt(PlayerFishKey, 0); 
    }

    public static void SavePlayerHealth(int health)
    {
        PlayerPrefs.SetInt(PlayerHealthKey, health);
        PlayerPrefs.Save();
    }

    public static int LoadPlayerHealth()
    {
        return PlayerPrefs.GetInt(PlayerHealthKey, 10); 
    }

    public static void SavePlayerHealthMax(int healthMax)
    {
        PlayerPrefs.SetInt(PlayerHealthMaxKey, healthMax);
        PlayerPrefs.Save();
    }

    public static int LoadPlayerHealthMax()
    {
        return PlayerPrefs.GetInt(PlayerHealthMaxKey, 10); 
    }

    public static void SaveCurrentLevel(string level)
    {
        PlayerPrefs.SetString(CurrentLevelKey, level);
        PlayerPrefs.Save();
    }

    public static string LoadCurrentLevel()
    {
        return PlayerPrefs.GetString(CurrentLevelKey, "Level0"); 
    }
    
    public static void ClearData()
    {
        PlayerPrefs.DeleteKey(PlayerFishKey);
        PlayerPrefs.DeleteKey(PlayerPateKey);
        PlayerPrefs.DeleteKey(PlayerHealthKey);
        PlayerPrefs.DeleteKey(PlayerHealthMaxKey);
        PlayerPrefs.DeleteKey(PlayerWeaponKey);
        PlayerPrefs.DeleteKey(PlayerWeaponMaxKey);
        PlayerPrefs.DeleteKey(CurrentLevelKey);
        PlayerPrefs.DeleteKey(MaxLevelKey);
        PlayerPrefs.DeleteKey(StarLevelKey);
        PlayerPrefs.Save();
    }
}
