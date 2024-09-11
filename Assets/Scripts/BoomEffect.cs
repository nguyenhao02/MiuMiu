using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEffect : MonoBehaviour
{
    [SerializeField] private GameObject boomEffect;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            Instantiate(boomEffect, transform.position, Quaternion.identity);
        }
    }
}
