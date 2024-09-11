using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int attack = 1;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            Animator playerAnim = collider.GetComponentInParent<Animator>();
            playerAnim.SetTrigger("isHurt");
            DataManager.Instance.MinusPlayerHealth(attack);
            SoundManager.Instance.PlaySFX(SoundManager.Instance.hurt);
        }
    }
}
