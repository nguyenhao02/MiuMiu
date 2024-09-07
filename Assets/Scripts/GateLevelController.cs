using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateLevelController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private bool isOpen = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Animation();
    }

    public void Animation()
    {
        anim.SetBool("isOpen",isOpen);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            isOpen = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            isOpen = false;
        }
    }

}
