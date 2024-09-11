using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    [SerializeField] private float aliveTime = 4f;
    [SerializeField] private float speedBullet = 5f;
    [SerializeField] private AudioSource sfxShoot;

    private Vector2 direction;

    void Start()
    {
        Destroy(gameObject, aliveTime);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        direction = (player.transform.position - transform.position).normalized; 
        sfxShoot.PlayOneShot(SoundManager.Instance.firework);
    }

    private void FixedUpdate()
    {
        transform.Translate(direction * speedBullet * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
