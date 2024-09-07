using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SummaryLevelController : MonoBehaviour
{
    
    [SerializeField] private int fishCollect, pateCollect;

    public void AddFishCollect(int fish)
    {
        fishCollect += fish;
    }

    public void AddPateCollect(int pate)
    {
        pateCollect += pate;
    }

    public int GetFishCollect()
    {
        return fishCollect;
    }

    public int GetPateCollect()
    {
        return pateCollect;
    }
}
