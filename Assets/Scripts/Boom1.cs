using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom1 : MonoBehaviour
{
    [SerializeField] private GameObject boom1Collider;
    [SerializeField] private float waitAttack;
    void FixedUpdate()
    {
        StartCoroutine(ShowBoom());
    }

    private IEnumerator ShowBoom()
    {
        yield return new WaitForSeconds(waitAttack);
        boom1Collider.gameObject.SetActive(true);
    }
}
