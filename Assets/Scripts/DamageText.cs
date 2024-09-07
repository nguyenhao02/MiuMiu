using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    public float moveSpeed = 1.0f; 
    public float destroyTime = 1.0f; 

    void Start()
    {
        Destroy(transform.parent.gameObject, destroyTime); 
    }

    void Update()
    {
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
    }

    public void SetDamageText(int damage)
    {
        GetComponent<TextMeshProUGUI>().text = " - " + damage.ToString();
    }
}
