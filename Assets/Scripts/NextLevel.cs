using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class NextLevel : MonoBehaviour
{
    [SerializeField] GameObject summaryLevel;
    [SerializeField] GameObject CanvasSummaryLevel;
    [SerializeField] private int indexMap;
    [SerializeField] private Image imageStarLevel;
    [SerializeField] private Slider sliderFish, sliderPate;
    [SerializeField] private TextMeshProUGUI textFish, textPate;
    [SerializeField] private int fishMax, pateMax;
    [SerializeField] private int fishCollect, pateCollect;
    [SerializeField] private List<Sprite> starSprite;
    private string nextLevelName;
    private int numStar = 0;
    private string starLevel;
    
    void Start()
    {
        nextLevelName = "level" + (indexMap + 1);
    }

    public void UnlockLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex > DataManager.Instance.maxLevel)
        {
            DataManager.Instance.SetMaxLevel(SceneManager.GetActiveScene().buildIndex);
        }
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.nextLevel);
            DataManager.Instance.SetCurrentLevel(nextLevelName);
            UnlockLevel();

            CanvasSummaryLevel.SetActive(true);
            ShowSummary();

            StartCoroutine(PauseGameAfterDelay(0.02f));
        }
    }

    private IEnumerator PauseGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Time.timeScale = 0;
    }

    private void ShowSummary()
    {
        SummaryLevelController summaryLevelController = summaryLevel.GetComponent<SummaryLevelController>();
        fishCollect = summaryLevelController.GetFishCollect();
        pateCollect = summaryLevelController.GetPateCollect();
        
        // ShowStarLevel
        numStar += 1;
        if(fishCollect == fishMax) numStar += 1;
        if(pateCollect == pateMax) numStar += 1;
        imageStarLevel.sprite = starSprite[numStar - 1];

        //Slider
        sliderFish.value = fishCollect / fishMax;
        sliderPate.value = pateCollect / pateMax;
        
        //Text
        textFish.text = fishCollect + " / " + fishMax;
        textPate.text = pateCollect + " / " + pateMax;

        SetStarLevel();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }

    public void SetStarLevel()
    {   
        starLevel = DataManager.Instance.starLevel;
        
        if((int)char.GetNumericValue(starLevel[indexMap]) >= numStar) return;

        char[] charArray = starLevel.ToCharArray();
        charArray[indexMap] = (char)(numStar + '0');
        starLevel = new string(charArray);
        DataManager.Instance.SetStarLevel(starLevel);
    }
}
