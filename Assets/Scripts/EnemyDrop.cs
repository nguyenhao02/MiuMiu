using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private GameObject boomEffect;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;
    private GameObject player;
    private bool goLelf;
    void Awake()
    {
        Destroy(gameObject, 6f);
    }
    void Start()
    {
        player = GameObject.Find("Player");
        goLelf =  player.transform.position.x < transform.position.x ? true : false ;
        if(!goLelf) Flip(); 
    }

    void FixedUpdate()
    {
        MoveEnemy();
    }
    private void MoveEnemy()
    {
        if( goLelf) rb.velocity = new Vector2(-1f * speed, rb.velocity.y);
        else  rb.velocity = new Vector2(1f * speed, rb.velocity.y);
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            Instantiate(boomEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
