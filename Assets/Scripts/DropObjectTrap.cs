using System.Collections;
using UnityEngine;

public class DropObjectTrap : MonoBehaviour
{
    [SerializeField] Transform dropPosition;
    [SerializeField] GameObject dropObject;
    [SerializeField] GameObject dropObject2;
    [SerializeField] float dropCooldown = 2f; // Thời gian chờ giữa các lần drop
    private bool canDrop = true; // Biến kiểm soát việc drop

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

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
        dropObject2.SetActive(true);
        canDrop = true;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(!canDrop) return;
        if (collider.gameObject.CompareTag("Player") )
        {
            dropObject2.SetActive(false);
            SoundManager.Instance.PlaySFX(SoundManager.Instance.enemyDrop);
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
