using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Range(0,5)]
    public float smoothTime;
    public Vector2 xLimit;
    public Vector2 yLimit;
    private Vector3 offset ;
    //= new Vector3(0f, 0f, -10f)
    private Vector3 velocity = Vector3.zero;

    [SerializeField] Transform target;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.localPosition - target.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        targetPosition = new Vector3(Mathf.Clamp(targetPosition.x, xLimit.x, xLimit.y), Mathf.Clamp(targetPosition.y, yLimit.x, yLimit.y), -10);
        transform.position = Vector3.Lerp(transform.position, targetPosition,  smoothTime* Time.deltaTime );
        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
