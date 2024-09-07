using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource sfxLongSource;
    public AudioClip soundMenu;
    public AudioClip earnMoney, error, click;
    public AudioClip jump, hurt;
    public AudioClip napDan, attack1;
    public List<AudioClip> shoots;
    public AudioClip item, itemDrop;
    public AudioClip enemyDrop, enemyHit;
    public AudioClip notice, nextLevel;

    private void Awake()
    {
        // Đảm bảo chỉ có một instance của SoundManager tồn tại
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Phát nhạc nền
    public void PlayMusic(AudioClip musicClip)
    {
        musicSource.clip = musicClip;
        musicSource.loop = true;
        musicSource.Play();
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFXLong(AudioClip sfxLongClip)
    {
        sfxLongSource.clip = sfxLongClip;
        sfxLongSource.loop = true;
        sfxLongSource.Play();
    }
    public void StopSFXLong()
    {
        sfxLongSource.Stop();
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp(volume, 0f, 1f); 
    }

    // Phát hiệu ứng âm thanh
    public void PlaySFX(AudioClip sfxClip)
    {
        sfxSource.PlayOneShot(sfxClip);
    }



}
