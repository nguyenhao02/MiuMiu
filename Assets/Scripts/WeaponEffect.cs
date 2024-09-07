using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, Random.Range(-180f, 180f));
        Destroy(gameObject, 0.21f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
