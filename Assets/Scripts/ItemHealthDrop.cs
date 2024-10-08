using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemHealthDrop : MonoBehaviour
{
    private RectTransform healthImageTras; // Hình ảnh trên Canvas UIGame
    private Vector3 zoomSize = new Vector3(2f, 2f, 1f); // Kích thước phóng to
    private Vector3 normalSize ;
    private Rigidbody2D rb;
    private Vector3 imageCanvasPosition;
    private bool isMoving = false;
    private bool canMove = false;

    void Start()
    {
        GameObject healthImage = GameObject.Find("HealthImage");
        healthImageTras = healthImage.GetComponent<RectTransform>();

        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(Random.Range(-4f, 4f), Random.Range(5f, 10f));

        normalSize = healthImageTras.localScale ; 

        StartCoroutine(WaitAndMove());
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, imageCanvasPosition, 15 * Time.deltaTime);
            // Thay đổi kích thước hình ảnh con cá khi đối tượng di chuyển đến gần
            float distance = Vector3.Distance(transform.position, imageCanvasPosition);
            float maxDistance = 5f; // Tùy chỉnh khoảng cách tối đa để phóng to

            if (distance < maxDistance)
            {
                float scaleFactor = Mathf.Lerp(1f, zoomSize.x, 1 - (distance / maxDistance));
                healthImageTras.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
            }

            if (transform.position == imageCanvasPosition)
            {
                DataManager.Instance.AddPlayerHealth(1);
                healthImageTras.localScale = normalSize;
                Destroy(gameObject);
            }
        }

        imageCanvasPosition = GetHealthPosition();
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") && canMove)
        {
            rb.gravityScale = 0;
            isMoving = true;

            SoundManager.Instance.PlaySFX(SoundManager.Instance.itemDrop);
        }
    }

    private Vector3 GetHealthPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(healthImageTras.position);
        Plane plane = new Plane(Vector3.forward, 0);

        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 worldPosition = ray.GetPoint(distance);
            return worldPosition;
        }

        return Vector3.zero;   
    }

     private IEnumerator WaitAndMove()
    {
        yield return new WaitForSeconds(1f);
        canMove = true;
    }
}
