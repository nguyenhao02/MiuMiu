using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectDrop : MonoBehaviour
{
    [SerializeField] private GameObject trapDropped;
    [SerializeField] private GameObject boomEffect;
    private float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        trapDropped.SetActive(false);
        rotateSpeed = Random.Range(10f, 15f);
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
            Instantiate(boomEffect, transform.position, Quaternion.identity);
            DestroyObject();
        }
        if(collider.gameObject.CompareTag("Ground"))
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.enemyHit);
            trapDropped.SetActive(true);
            trapDropped.transform.position = new Vector3(transform.position.x + Random.Range(-0.3f, 0.3f), transform.position.y, 0);
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
