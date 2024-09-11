using System.Collections;
using UnityEngine;

public class DropObjectTrap : MonoBehaviour
{
    [SerializeField] Transform dropPosition;
    [SerializeField] GameObject dropObject;
    [SerializeField] GameObject imageObject;
    [SerializeField] float dropCooldown = 2f; // Thời gian chờ giữa các lần drop
    private bool canDrop = true; // Biến kiểm soát việc drop


    private void DropObject()
    {
        GameObject dropObjectTemp = Instantiate(dropObject, dropPosition.position, Quaternion.identity);
        canDrop = false;
        Destroy(dropObjectTemp, 4f);
        StartCoroutine(DropCooldown());
    }

    private IEnumerator DropCooldown()
    {
        yield return new WaitForSeconds(dropCooldown);
        imageObject.SetActive(true);
        canDrop = true;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(!canDrop) return;
        if (collider.gameObject.CompareTag("Player") )
        {
            imageObject.SetActive(false);
            DropObject();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") )
        {
           
        }
    }
}
