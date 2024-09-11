using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class BoomTrap : MonoBehaviour
{
     [SerializeField] private GameObject boomPrefabs;
     [SerializeField] private float minTime = 5f, maxTime = 8f;
     private GameObject boom;
     private bool canBoom = true;

     private void OnTriggerStay2D(Collider2D collider)
     {
          if(collider.CompareTag("Player") && canBoom)
          {
               AttackBoom();
               UpdateBoomPosition();
          }
     }
          
     private void AttackBoom()
     { 
          canBoom = false;
          boom = Instantiate(boomPrefabs, transform.position, Quaternion.identity);
          StartCoroutine(Waiting());
     }

     private IEnumerator Waiting()
     {
          yield return new WaitForSeconds(Random.Range(minTime, maxTime));
          canBoom = true;
     }

     private void UpdateBoomPosition()
     {
          if(boom != null)
          boom.transform.position = transform.position;
     }

     private void OnDestroy() // khi gameobject bị destroy
     {
          if (boom != null)
          {
               Destroy(boom);
          }
     }
}
