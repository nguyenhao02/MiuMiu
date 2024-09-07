using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropEnemyTrap : MonoBehaviour
{
    [SerializeField] private Transform dropPosition;
    [SerializeField] private GameObject enemyDrop;
    [SerializeField] private float minTimeDrop = 1f, maxTimeDrop = 10f;
    private Animator anim;
    private bool canDrop = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DropEnemys();
    }

    private void DropEnemys()
    {
        if(canDrop)
        {
            StartCoroutine(DropEnemyWithDelay());
            canDrop = false;
        }
    }

    private IEnumerator DropEnemyWithDelay()
    {
        anim.SetTrigger("isDrop");
        yield return new WaitForSeconds(1f);
        // DropEnemy
        Instantiate(enemyDrop, dropPosition.position, Quaternion.identity);
        canDrop = false;
        //Wait
        yield return StartCoroutine(DropCooldown());
    }

    private IEnumerator DropCooldown()
    {
        yield return new WaitForSeconds(Random.Range(minTimeDrop, maxTimeDrop));
        canDrop = true;
    }

}
