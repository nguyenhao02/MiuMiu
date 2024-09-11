
using UnityEngine.UI;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class ShowNoticeGame : MonoBehaviour
{
    [SerializeField] private GameObject noticeUI;
    [SerializeField] private UnityEngine.UI.Image imageNotice;
    [SerializeField] private TextMeshProUGUI textNotice;
    [SerializeField] private Sprite spriteNotice;
    [SerializeField] public List<string> stringNotice;

    void OnTriggerEnter2D (Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.notice);
            Time.timeScale = 0;
            noticeUI.SetActive(true);
            string textInfo = "";
            imageNotice.sprite = spriteNotice;
            for(int i = 0; i < stringNotice.Count - 1; i++)
            {
                textInfo += stringNotice[i] + "\n";
            }
            textInfo += stringNotice[stringNotice.Count - 1];
            textNotice.SetText(textInfo);
        }
    }

    void OnTriggerExit2D (Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {  
            Destroy(gameObject);
        }
    }
}
