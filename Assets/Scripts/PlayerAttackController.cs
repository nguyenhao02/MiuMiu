using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private float timeAlive = 0.3f;
    void Start()
    {
        Destroy(gameObject, timeAlive);
    }

}
