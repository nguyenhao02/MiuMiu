using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom1 : MonoBehaviour
{
    [SerializeField] GameObject boom1Collider;
   void Start()
   {
        Destroy(gameObject, 1.95f);
   }
   
    void Update()
    {
        ShowBoom();
    }

    private void ShowBoom()
    {
        StartCoroutine(WaitAndShow());
    }

    private IEnumerator WaitAndShow()
    {
        yield return new WaitForSeconds(1.8f);
        boom1Collider.gameObject.SetActive(true);
    }
}
