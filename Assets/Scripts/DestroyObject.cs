using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DestroyObject : MonoBehaviour
{
    public float aliveTime;
    void Awake()
    {
        Destroy(gameObject, aliveTime);
    }
}
