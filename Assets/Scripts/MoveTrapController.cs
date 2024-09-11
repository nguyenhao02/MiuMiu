using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveTrapController : MonoBehaviour
{
    [SerializeField] private Transform enemyImage;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float waitTime = 0.5f;
    [SerializeField] private List<Transform> points;
    [SerializeField] private bool enemyFlip;
    private Vector3 destination;
    private int nextPoint;
    private bool isWait = false;
    private bool isFacingRight = true;

    void Start()
    {
        transform.position = points[0].position;
        nextPoint = 1;
        destination = points[nextPoint].position;
    }

    void FixedUpdate()
    {
        MoveTrap();
    }

    private void MoveTrap()
    {
        if (isWait) return;
        if(enemyFlip) MoveTrap1();
        else MoveTrap2();
    }

    private void MoveTrap1()
    {
        //Flip
        TowardsDestination();

        //Move
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        if (transform.position == destination)
        {
            if(nextPoint == points.Count - 1) nextPoint = 0; 
            else nextPoint ++;
            
            destination = points[nextPoint].position;
            
            StartCoroutine(DelayTime(waitTime));
        }
    }

    private void MoveTrap2()
    {
        //Move
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        //Rotate
        Vector3 direction = (destination - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        enemyImage.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (transform.position == destination)
        {
            destination = points[nextPoint].position;
            if(nextPoint == points.Count - 1) nextPoint = 0; 
            else nextPoint ++;

            StartCoroutine(DelayTime(waitTime));
        }
    }

    private IEnumerator DelayTime(float delayTime)
    {
        isWait = true;
        yield return new WaitForSeconds(delayTime);
        isWait = false;
    }

    private void TowardsDestination()
    {
        if(isFacingRight && destination.x < transform.position.x)
        {
            Flip();
            return;
        }
        if(!isFacingRight && destination.x > transform.position.x) 
        {
            Flip();
            return;
        }
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
