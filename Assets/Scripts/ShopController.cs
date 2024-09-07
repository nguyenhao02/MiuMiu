using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [SerializeField] private List<GameObject> shops;
    [SerializeField] private GameObject notice;
    [SerializeField] private TextMeshProUGUI textNotice;
    [SerializeField] private TextMeshProUGUI textUpgradeHealthByFish;
    [SerializeField] private TextMeshProUGUI textUpgradeHealthByPate;
    [SerializeField] private TextMeshProUGUI textUpgradeWeaponByFish;
    [SerializeField] private TextMeshProUGUI textUpgradeWeaponByPate;

    private void Start()
    {
        ResetShops();
        shops[0].SetActive(true);

        ShowUpgradeHealth();
        ShowUpgradeWeapon();
    }

    public void OpenShop1() { SoundManager.Instance.PlaySFX(SoundManager.Instance.click); ResetShops(); shops[0].SetActive(true); }
    
    public void OpenShop2() { SoundManager.Instance.PlaySFX(SoundManager.Instance.click); ResetShops(); shops[1].SetActive(true); }
    
    public void OpenShop3() { SoundManager.Instance.PlaySFX(SoundManager.Instance.click); ResetShops(); shops[2].SetActive(true); }
    
    public void OpenShop4() { SoundManager.Instance.PlaySFX(SoundManager.Instance.click); ResetShops(); shops[3].SetActive(true); }
   
    private void ResetShops()
    {
        foreach (var shop in shops)
        {
            shop.SetActive(false);
        }
    }

    public void BuyHealthByFish(int fish)
    {
        notice.SetActive(true);
        int currentFish = DataManager.Instance.playerFish;
        if(currentFish >= fish) 
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.earnMoney);
            DataManager.Instance.MinusPlayerFish(fish);
            DataManager.Instance.AddPlayerHealth(fish);
            textNotice.text = "Mua thành công " + fish + " Tym";
        }
        else 
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.error);
            textNotice.text = "Bé còn thiếu " + (fish - currentFish) + " Cá";
        }

    }

    public void BuyWeaponthByFish(int fish)
    {
        notice.SetActive(true);
        int currentFish = DataManager.Instance.playerFish;
        if(currentFish >= fish) 
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.earnMoney);
            DataManager.Instance.MinusPlayerFish(fish);
            DataManager.Instance.AddPlayerWeapon(fish);
            textNotice.text = "Mua thành công " + fish + " Vũ Khí";
        }
        else 
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.error);
            textNotice.text = "Bé còn thiếu " + (fish - currentFish) + " Cá";
        }

    }

    public void BuyWeaponthByPate(int pate)
    {
        notice.SetActive(true);
        if(DataManager.Instance.playerPate > 0) 
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.earnMoney);
            DataManager.Instance.MinusPlayerPate(pate);
            DataManager.Instance.AddPlayerWeapon(10);
            textNotice.text = "Mua thành công " + 10 + " Vũ Khí";
        }
        else 
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.error);
            textNotice.text = "Pate của bé không đủ";
        }

    }

    public void BuyHealthhByPate(int pate)
    {
        notice.SetActive(true);
        if(DataManager.Instance.playerPate > 0) 
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.earnMoney);
            DataManager.Instance.MinusPlayerPate(pate);
            DataManager.Instance.AddPlayerHealth(10);
            textNotice.text = "Mua thành công " + 10 + " Tym";
        }
        else 
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.error);
            textNotice.text = "Pate của bé không đủ";
        }

    }

    public void UpgradeHealthByFish()
    {
        notice.SetActive(true);
        int fish = Mathf.RoundToInt(50 * Mathf.Pow(1.15f, DataManager.Instance.playerHealthMax - 10));
        int currentFish = DataManager.Instance.playerFish;

        if(fish <= currentFish) 
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.earnMoney);
            DataManager.Instance.MinusPlayerFish(fish);
            DataManager.Instance.AddPlayerHealthMax(1);
            textNotice.text = "Nâng cấp thành công giới hạn Tym +1";
            ShowUpgradeHealth();
        } 
        else 
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.error);
            textNotice.text = "Bé còn thiếu " + (fish - currentFish) + " Cá";
        }
    }

    public void UpgradeWeaponByFish()
    {
        notice.SetActive(true);
        int fish = Mathf.RoundToInt(50 * Mathf.Pow(1.15f, DataManager.Instance.playerWeaponMax - 10));
        int currentFish = DataManager.Instance.playerFish;

        if(fish <= currentFish) 
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.earnMoney);
            DataManager.Instance.MinusPlayerFish(fish);
            DataManager.Instance.AddPlayerWeaponMax(1);
            textNotice.text = "Nâng cấp thành công giới hạn Vũ Khí +1";
            ShowUpgradeWeapon();
        } 
        else 
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.error);
            textNotice.text = "Bé còn thiếu " + (fish - currentFish) + " Cá";
        }
    }

    public void UpgradeHealthByPate()
    {
        notice.SetActive(true);
        int fish = Mathf.RoundToInt(50 * Mathf.Pow(1.15f, DataManager.Instance.playerHealthMax - 10));
        int pate = Mathf.RoundToInt(fish/10);
        int currentPate = DataManager.Instance.playerPate;

        if(pate <= currentPate) 
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.earnMoney);
            DataManager.Instance.MinusPlayerPate(pate);
            DataManager.Instance.AddPlayerHealthMax(1);
            textNotice.text = "Nâng cấp thành công giới hạn Tym +1";
            ShowUpgradeHealth();
        } 
        else 
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.error);
            textNotice.text = "Bé còn thiếu " + (pate - currentPate) + " Pate";
        }
    }

    public void UpgradeWeaponByPate()
    {
        notice.SetActive(true);
        int fish = Mathf.RoundToInt(50 * Mathf.Pow(1.15f, DataManager.Instance.playerWeaponMax - 10));
        int pate = Mathf.RoundToInt(fish/10);
        int currentPate = DataManager.Instance.playerPate;

        if(pate <= currentPate) 
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.earnMoney);
            DataManager.Instance.MinusPlayerPate(pate);
            DataManager.Instance.AddPlayerWeaponMax(1);
            textNotice.text = "Nâng cấp thành công giới hạn Vũ Khí +1";
            ShowUpgradeWeapon();
        } 
        else 
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.error);
            textNotice.text = "Bé còn thiếu " + (pate - currentPate) + " Pate";
        }
    }

    private void ShowUpgradeHealth()
    {
        textUpgradeHealthByFish.text = Mathf.RoundToInt(50 * Mathf.Pow(1.15f, DataManager.Instance.playerHealthMax - 10)).ToString();
        textUpgradeHealthByPate.text = Mathf.RoundToInt(5 * Mathf.Pow(1.15f, DataManager.Instance.playerHealthMax - 10)).ToString();
    }

    private void ShowUpgradeWeapon()
    {
        textUpgradeWeaponByFish.text = Mathf.RoundToInt(50 * Mathf.Pow(1.15f, DataManager.Instance.playerWeaponMax - 10)).ToString();
        textUpgradeWeaponByPate.text = Mathf.RoundToInt(5 * Mathf.Pow(1.15f, DataManager.Instance.playerWeaponMax - 10)).ToString();
    }

}
