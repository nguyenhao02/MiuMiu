using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class DropEnemyTrap : MonoBehaviour
{
    [SerializeField] private Transform dropPosition;
    [SerializeField] private GameObject enemy;
    [SerializeField] private float minTimeDrop = 1f, maxTimeDrop = 10f;
    [SerializeField] private float effectTime;
    [SerializeField] private bool isFollowPlayer;
    [SerializeField] private List<GameObject> enemyDrops;
    private Vector3 playerPosition;
    private Animator anim;
    private bool canDrop = true;
    private bool isStay = false;

    void Start()
    {
        anim = enemy.GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        DropEnemys();
        EnemyFollowPlayer();
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            isStay = true;
            playerPosition = collider.transform.position;
            return;
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            isStay = false;
        }
    }

    private void DropEnemys()
    {
        if(!isStay) return;
        if(!canDrop) return;
        
        StartCoroutine(DropEnemyWithDelay());
    }

    private IEnumerator DropEnemyWithDelay()
    {
        canDrop = false;
        anim.SetTrigger("isDrop");
        // Đợi cho đến khi clip "isDrop" kết thúc
        yield return new WaitForSeconds(effectTime); 
        // DropEnemy
        Instantiate(enemyDrops[Random.Range(0, enemyDrops.Count)], dropPosition.position, Quaternion.identity);
        //Wait
        yield return new WaitForSeconds(Random.Range(minTimeDrop, maxTimeDrop));
        canDrop = true;
    }

    private void EnemyFollowPlayer()
    {
        if(!isFollowPlayer) return;

        Vector2 direction = (playerPosition - dropPosition.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        enemy.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

}
