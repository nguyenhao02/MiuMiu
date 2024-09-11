using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerTrap : MonoBehaviour
{
    [SerializeField] private Transform enemyImage;
    [SerializeField] private float waitTimeToFollow;
    [SerializeField] private float moveSpeed;
    private bool canMove = true;
    private bool isFacingRight = true;
    private Transform player;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }


    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.CompareTag("Player") && canMove)
        {
            canMove = false;
            player = collider.transform;
            FlipTowardsPlayer();
            StartCoroutine(FollowPlayer());
        }
    }


    private IEnumerator FollowPlayer()
    {
        //Thêm 1 lực cho tParent để tiến gần về phía Player theo chiều ngang
        Vector2 direction = (player.position - transform.parent.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        yield return new WaitForSeconds(waitTimeToFollow);
        canMove = true;
    }

     private void FlipTowardsPlayer()
    {
        if (player.position.x < transform.parent.position.x && isFacingRight)
        {
            Flip();
        }
        else    if (player.position.x > transform.parent.position.x && !isFacingRight)
                {
                    Flip();
                }
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 localScale = enemyImage.localScale;
        localScale.x *= -1;
        enemyImage.localScale = localScale;
    }
}
