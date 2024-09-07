
using UnityEngine.UI;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class ShowNoticeGame : MonoBehaviour
{
    [SerializeField] private GameObject noticeUI;
    [SerializeField] private UnityEngine.UI.Image imageNotice;
    [SerializeField] private TextMeshProUGUI textNotice;
    [SerializeField] private Sprite spriteNotice;
    [SerializeField] public string stringNotice;

    void OnTriggerEnter2D (Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            Time.timeScale = 0;
            noticeUI.SetActive(true);
            textNotice.text = stringNotice;
            imageNotice.sprite = spriteNotice;
            SoundManager.Instance.PlaySFX(SoundManager.Instance.notice);
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
