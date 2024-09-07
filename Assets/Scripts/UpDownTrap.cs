using System.Collections;
using UnityEngine;

public class UpDownTrap : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float waitTime = 0.5f;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    private Vector3 destination;
    private bool isWait = false;

    void Start()
    {
        destination = startPoint.position;
    }

    void Update()
    {
        MoveTrap();
    }

    private void MoveTrap()
    {
        if (isWait) return;

        //Move
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        //Rotate
        Vector3 direction = (destination - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (transform.position == startPoint.position)
        {
            destination = endPoint.position;
            StartCoroutine(DelayTime(waitTime));
        }
        if (transform.position == endPoint.position)
        {
            destination = startPoint.position;
            StartCoroutine(DelayTime(waitTime));
        }
    }

    private IEnumerator DelayTime(float delayTime)
    {
        isWait = true;
        yield return new WaitForSeconds(delayTime);
        isWait = false;
    }
}
