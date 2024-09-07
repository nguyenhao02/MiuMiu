using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    [SerializeField] private List<GameObject> maps;
    [SerializeField] private List<Button> btnMap;
    [SerializeField] private List<Sprite> starLevelSprite;
    [SerializeField] private string starLevelString;
 
    private void Start()
    {
        starLevelString = DataManager.Instance.starLevel;

        ResetMaps();
        maps[0].SetActive(true);
        
        SetBtnMap();
        
    }

    public void OpenMap1() { SoundManager.Instance.PlaySFX(SoundManager.Instance.click); ResetMaps(); maps[0].SetActive(true); }
    public void OpenMap2() { SoundManager.Instance.PlaySFX(SoundManager.Instance.click); ResetMaps(); maps[1].SetActive(true); }
    public void OpenMap3() { SoundManager.Instance.PlaySFX(SoundManager.Instance.click); ResetMaps(); maps[2].SetActive(true); }
    public void OpenMap4() { SoundManager.Instance.PlaySFX(SoundManager.Instance.click); ResetMaps(); maps[3].SetActive(true); }
    public void OpenMap5() { SoundManager.Instance.PlaySFX(SoundManager.Instance.click); ResetMaps(); maps[4].SetActive(true); }

    private void ResetMaps()
    {
        foreach(var map in maps)
        {
            map.SetActive(false);
        }
    }

    public void LoadSceneMap(int x)
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.click);
        string mapLoad = "Level" + x;
        DataManager.Instance.SetCurrentLevel(mapLoad);
        SceneManager.LoadScene(mapLoad);
    }

    private void SetBtnMap()
    {
        
        for (int i = 1; i < btnMap.Count; i++)
        {
            btnMap[i].interactable = false;
            Transform  imageStar = btnMap[i].transform.Find("ImageStar");
            Image image  = imageStar.GetComponent<Image>();
            int numStar = (int)char.GetNumericValue(starLevelString[i]);
            image.sprite = starLevelSprite[numStar]; 
        }

        for (int i = 1; i <= DataManager.Instance.maxLevel && i < btnMap.Count; i++)
        {
            btnMap[i].interactable = true;
        }
    }
}
