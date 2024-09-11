using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectDrop : MonoBehaviour
{
    [SerializeField] private GameObject trapDropped;
    private AudioSource sfxSound;
    private float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        trapDropped.SetActive(false);
        rotateSpeed = Random.Range(10f, 20f);
        sfxSound = gameObject.GetComponentInParent<AudioSource>();
        sfxSound.PlayOneShot(SoundManager.Instance.enemyDrop);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotateObject();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            DestroyObject();
        }
        if(collider.gameObject.CompareTag("Ground"))
        {
            trapDropped.SetActive(true);
            trapDropped.transform.position = new Vector3(transform.position.x + Random.Range(-0.4f, 0.4f), transform.position.y, 0);
            //Play sound
            sfxSound.PlayOneShot(SoundManager.Instance.enemyHit);
            DestroyObject();
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
    
    private void RotateObject()
    {
        transform.Rotate(0, 0, rotateSpeed);
    }
}
