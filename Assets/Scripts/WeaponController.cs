using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour
{                
    [SerializeField] private GameObject boomEffect;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Enemy"))
        {
            Quaternion randomRotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(-180f, 180f));
            Instantiate(boomEffect, collider.transform.position, randomRotation);
        }
    }
}
