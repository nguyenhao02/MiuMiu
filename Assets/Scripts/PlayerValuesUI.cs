using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PLayerValuesUI : MonoBehaviour
{
        GameObject healthText;
        GameObject fishText;
        GameObject weaponText;
        GameObject pateText;

        TextMeshProUGUI healthTextUI;
        TextMeshProUGUI fishTextUI;
        TextMeshProUGUI weaponTextUI;
        TextMeshProUGUI pateTextUI;

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdatePlayerValuesUI();
    }

    public void UpdatePlayerValuesUI()
    {
        healthText = GameObject.Find("HealthText");
        fishText = GameObject.Find("FishText");
        weaponText = GameObject.Find("WeaponText");
        pateText = GameObject.Find("PateText");

        healthTextUI = healthText.GetComponent<TextMeshProUGUI>();
        fishTextUI = fishText.GetComponent<TextMeshProUGUI>();
        weaponTextUI = weaponText.GetComponent<TextMeshProUGUI>();
        pateTextUI = pateText.GetComponent<TextMeshProUGUI>();
        
        healthTextUI.text =  DataManager.Instance.playerHealth.ToString() + " / " + DataManager.Instance.playerHealthMax.ToString();
        weaponTextUI.text = DataManager.Instance.playerWeapon.ToString() + " / " + DataManager.Instance.playerWeaponMax.ToString();
        fishTextUI.text = DataManager.Instance.playerFish.ToString();
        pateTextUI.text = DataManager.Instance.playerPate.ToString();
    }
}
